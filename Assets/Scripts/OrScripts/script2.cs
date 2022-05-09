/*using UnityEngine;
using Pathfinding;

namespace Product {
    [RequireComponent(typeof(EdgeCollider2D))]
    public class PlatformController : GraphModifier {
        [SerializeField] int platformHeightInNodes = 2;

        /// <summary>
        /// Updates graphs with a created GUO.
        /// Creates a Pathfinding.GraphUpdateObject with a Pathfinding.GraphUpdateShape
        /// representing the polygon of this object and update all graphs using AstarPath.UpdateGraphs.
        /// This will not update graphs immediately. See AstarPath.UpdateGraph for more info.
        /// </summary>
        public void Apply(CombatRoomModel roomModel) {
            if (AstarPath.active == null) {
                Debug.LogError("There is no AstarPath object in the scene", this);
                return;
            }

            var edgeCollider = GetComponent<EdgeCollider2D>();
            var bounds = edgeCollider.bounds;
            var startPosition = new Vector3(bounds.min.x, bounds.min.y - 0.1f, 0f);
            var endPosition = new Vector3(bounds.max.x, bounds.min.y - 0.1f, 0f);
            var start = roomModel.FlyingGraph.GetNearest(startPosition).node as GridNodeBase;
            var end = roomModel.FlyingGraph.GetNearest(endPosition).node as GridNodeBase;

            AstarData.active.AddWorkItem(
                () => {
                    // Rows of platform tag.
                    for (int i = 0; i < platformHeightInNodes; i++) {
                        roomModel.TagHorizontalLine(start, end, CombatRoomModel.PLATFORM_NODE_TAG, -i);
                    }

                    // Ceiling below the platform.
                    roomModel.TagHorizontalLine(start, end, CombatRoomModel.CEILING_NODE_TAG, -platformHeightInNodes);
                    roomModel.TagHorizontalLine(start, end, CombatRoomModel.CLOSE_TO_GROUND_2_NODE_TAG, -platformHeightInNodes - 1);
                    roomModel.TagHorizontalLine(start, end, CombatRoomModel.CLOSE_TO_GROUND_3_NODE_TAG, -platformHeightInNodes - 2);

                    // Standable above the platform.
                    roomModel.TagHorizontalLine(start, end, CombatRoomModel.STANDABLE_NODE_TAG, 1);
                    roomModel.TagHorizontalLine(start, end, CombatRoomModel.CLOSE_TO_GROUND_2_NODE_TAG, 2);
                    roomModel.TagHorizontalLine(start, end, CombatRoomModel.CLOSE_TO_GROUND_3_NODE_TAG, 3);

                    // Add nodes to point graph.
                    var startX = startPosition.x;
                    var endX = endPosition.x;
                    var y = bounds.max.y + 0.1f;
                    roomModel.AddPlatformToGraph(startX, endX, y);
                });
        }
    }
}*/