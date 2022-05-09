/*using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;
using System;
using System.Collections;
using System.Collections.Generic;
using Avrahamy;
using Avrahamy.Collections;
using Avrahamy.Math;
using Avrahamy.Utils;
using Product.Combat;
using BitStrap;
using Pathfinding;


namespace Product {
    public class CombatRoomModel : MonoBehaviour {
        public const int PLATFORM_NODE_TAG = 10;
        public const int STANDABLE_NODE_TAG = 11;
        public const int WALL_NODE_TAG = 12;
        public const int CEILING_NODE_TAG = 13;
        public const int DANGER_NODE_TAG = 20;
        public const int CLOSE_TO_GROUND_1_NODE_TAG = 21;
        public const int CLOSE_TO_GROUND_2_NODE_TAG = 22;
        public const int CLOSE_TO_GROUND_3_NODE_TAG = 23;

        [Header("Navigation")]
        [SerializeField] float platformingGraphNodeDistance = 2f;
        [SerializeField] float platformingGraphEdgeOffset = 0.5f;
        [SerializeField] Vector2 center;
        [SerializeField] Vector2Int size;
        [SerializeField] float flyingGraphNodeSize = 0.5f;

        public GridGraph FlyingGraph {
            get {
                return flyingGraph;
            }
        }
        public PointGraph PlatformingGraph {
            get {
                return platformingGraph;
            }
        }

        public float FlyingGraphNodeSize {
            get {
                return flyingGraphNodeSize;
            }
        }

        private GridGraph flyingGraph;
        private PointGraph platformingGraph;

        protected void Start() {
            flyingGraph = AstarData.active.graphs[0] as GridGraph;
            flyingGraph.center = center;
            flyingGraph.SetDimensions(size.x, size.y, flyingGraphNodeSize);
            AstarData.active.Scan();

            platformingGraph = AstarData.active.graphs[1] as PointGraph;

            // Set the platform tags so that AI can be set to go up platforms but
            // not down.
            var platforms = GetComponentsInChildren<PlatformController>(true);
            Array.Sort(
                platforms,
                (lhs, rhs) =>
                    Mathf.RoundToInt((lhs.transform.position.y - rhs.transform.position.y) * 5f));
            DebugAssert.Assert(platforms.Length <= 10, "Platform tags does not support more than 10 platforms");
            foreach (var platform in platforms) {
                if (!platform.gameObject.activeSelf) continue;
                platform.Apply(this);
            }

            GraphUtilities.GetContours(flyingGraph, OnGotContour, 1000f);
            AstarPath.active.FlushWorkItems();
            PlatformingGraph.ConnectNodes();
            AstarPath.active.AddWorkItem(ctx => ctx.EnsureValidFloodFill());
            AstarPath.active.FlushWorkItems();
        }

#region Build Navigation Graphs
        private void OnGotContour(Vector3[] contour) {
            var contourNodes = new GridNodeBase[contour.Length];
            contourNodes[0] = flyingGraph.GetNearestForce(contour[0] + Vector3.up * 0.1f, NNConstraint.Default).node as GridNodeBase;
            DebugDraw.DrawCross2D((Vector3)contourNodes[0].position, 0.2f, Color.blue, 10f, false);
            for (int i = 1; i < contour.Length; i++) {
                DebugDraw.DrawLine(contour[i - 1], contour[i], Color.magenta, 10f);
                DebugDraw.DrawCross2D(contour[i], 0.05f * i, Color.magenta, 10f, false);
                // BUG: When looking for the nearest node in the grid graph of a wall, it might return nodes that are not in the same X coordinate in the grid.
                //      The HACK to workaround it is by checking the border.x coordinates in the TagLine method.
                contourNodes[i] = flyingGraph.GetNearestForce(contour[i] + Vector3.up * 0.1f, NNConstraint.Default).node as GridNodeBase;
                DebugDraw.DrawCross2D((Vector3)contourNodes[i].position, 0.05f * i, Color.blue, 10f, false);
                var start = contourNodes[i - 1];
                var end = contourNodes[i];
                var borderStart = contour[i - 1];
                var borderEnd = contour[i];
                AstarData.active.AddWorkItem(() =>
                    TagLine(start,end, borderStart, borderEnd));

            }
            DebugDraw.DrawLine(contour.Last(), contour[0], Color.magenta, 10f);
            DebugDraw.DrawCross2D(contour[0], 0.2f, Color.magenta, 10f, false);
            AstarData.active.AddWorkItem(() =>
                TagLine(contourNodes.Last(),contourNodes[0], contour[0], contour.Last()));
        }

        /// <summary>
        /// Tag a horizontal or vertical line. Use with AddWorkItem.
        /// </summary>
        private void TagLine(GridNodeBase start, GridNodeBase end, Vector3 borderStart, Vector3 borderEnd) {
            DebugLog.Log($"Tag line: startGrid=({start.XCoordinateInGrid}, {start.ZCoordinateInGrid}) endGrid=({end.XCoordinateInGrid}, {end.ZCoordinateInGrid}) start={borderStart} end={borderEnd}");
            if (start.XCoordinateInGrid == end.XCoordinateInGrid || Mathf.Approximately(borderStart.x, borderEnd.x)) {
                // This is a wall.
                TagVerticalLine(start, end, WALL_NODE_TAG);
            } else {
                uint nodeTag;
                if (((Vector3)end.position).y < borderEnd.y) {
                    nodeTag = CEILING_NODE_TAG;
                } else {
                    nodeTag = STANDABLE_NODE_TAG;
                    // Found a floor.
                    AddPlatformToGraph(borderStart.x, borderEnd.x, borderEnd.y + 0.1f);
                }
                TagHorizontalLine(start, end, nodeTag);
            }
        }

        /// <summary>
        /// Tag a vertical line. Use with AddWorkItem.
        /// </summary>
        public void TagVerticalLine(GridNodeBase start, GridNodeBase end, uint nodeTag, int xOffset = 0) {
            var x = start.XCoordinateInGrid + xOffset;
            var startZ = Mathf.Min(start.ZCoordinateInGrid, end.ZCoordinateInGrid);
            var endZ = Mathf.Max(start.ZCoordinateInGrid, end.ZCoordinateInGrid);
            for (int z = startZ; z <= endZ; z++) {
                var node = flyingGraph.GetNode(x, z);
                // Only set tags for walkable nodes.
                if (!node.Walkable) continue;
                if (IsBetterTag(node.Tag, nodeTag)) {
                    node.Tag = nodeTag;
                }
            }
        }

        /// <summary>
        /// Tag a horizontal line. Use with AddWorkItem.
        /// </summary>
        public void TagHorizontalLine(GridNodeBase start, GridNodeBase end, uint nodeTag, int heightOffset = 0) {
            var z = start.ZCoordinateInGrid + heightOffset;
            var startX = Mathf.Min(start.XCoordinateInGrid, end.XCoordinateInGrid);
            var endX = Mathf.Max(start.XCoordinateInGrid, end.XCoordinateInGrid);
            for (int x = startX; x <= endX; x++) {
                var node = flyingGraph.GetNode(x, z);
                // Only set tags for walkable nodes.
                if (!node.Walkable) continue;
                // Prioritize Standable and Ceiling tags compared to Wall.
                if (IsBetterTag(node.Tag, nodeTag)) {
                    node.Tag = nodeTag;
                }
            }
        }

        /// <summary>
        /// Add a horizontal platform to the platforming graph. Use with AddWorkItem.
        /// </summary>
        public void AddPlatformToGraph(float startX, float endX, float y) {
            var width = Mathf.Abs(endX - startX);
            startX = Mathf.Min(startX, endX);

            var nodeSize = platformingGraphNodeDistance;
            var nodesCount = Mathf.FloorToInt((width - platformingGraphEdgeOffset) / nodeSize);
            DebugLog.Log($"Adding {nodesCount} nodes to graph. start: {startX}, end: {endX}");
            var offset = (width - (nodesCount - 1) * nodeSize) / 2f;
            startX += offset;
            var nodes = new PointNode[nodesCount];
            for (int i = 0; i < nodesCount; i++) {
                var position = new Int3(new Vector3(startX + nodeSize * i, y, 0f));
                // Only add walkable nodes.
                if (!flyingGraph.GetNearest((Vector3)position).node.Walkable) {
                    DebugDraw.DrawCircle2D((Vector3)position, 0.1f, Color.red, 10f);
                    continue;
                }
                var node = platformingGraph.AddNode(position);
                nodes[i] = node;

                // Connect the nodes.
                //if (i == 0) continue;
                //var prevNode = nodes[i - 1];
                //if (prevNode == null) continue;
                //var cost = (uint)(prevNode.position - position).costMagnitude;
                //prevNode.AddConnection(node, cost);
                //node.AddConnection(prevNode, cost);
            }
        }

        private bool IsBetterTag(uint currentTag, uint newTag) {
            // Tag priority order:
            // 1. Platform
            // 2. Standable
            // 3. Ceiling
            // 4. Wall
            // 5. Close to ground 1-3
            if (currentTag == 0) return newTag > 0;
            return newTag < currentTag;
        }
#endregion
    }
}*/