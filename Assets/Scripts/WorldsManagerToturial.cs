using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;



public class WorldsManagerToturial : MonoBehaviour
{
    [SerializeField] private Transform right, left;
    [SerializeField] private Transform rightNodes, leftNodes;
    public static bool onRight, onLeft,DragRight,DragLeft,CharacterMove;
    [Range(20, 150)] [SerializeField] private float fastParameter = 150;
    //private PointGraph platformingGraph;
    private static WorldsManagerToturial _shared;
    
    
    void Start()
    {
        _shared = this;
       // platformingGraph = AstarData.active.graphs[0] as PointGraph;
        UpdateLeftNodes();
        UpdateRightNodes();
    }

    
    void Update()
    {
        if (WorldRotateTutorial.endDrag)
        {
            UpdateLeftNodes();
            UpdateRightNodes();
            WorldRotateTutorial.endDrag = false;
            AstarData.active.Scan();
        }
        if (DragLeft)
        {
            if (!onLeft) left.rotation =left.rotation * Quaternion.Euler(0, 0, fastParameter * Time.deltaTime);
            if (!onRight) right.rotation =right.rotation * Quaternion.Euler(0, 0, fastParameter * Time.deltaTime);
           
        }
        else if (DragRight)
        {
            if (!onLeft) left.rotation =left.rotation * Quaternion.Euler(0, 0, -fastParameter * Time.deltaTime);
            if (!onRight) right.rotation =right.rotation * Quaternion.Euler(0, 0, -fastParameter * Time.deltaTime);
        }

        if (DragLeft || DragRight)
        {
            foreach (var node in leftNodes.GetComponentsInChildren<Transform>())
            {
                var pos = node.position;
                if (node == leftNodes) continue;
                if (node.gameObject.layer == 8)
                    node.rotation = Quaternion.LookRotation(rightNodes.position);
            }
        }
    }

   
    
    void UpdateRightNodes()
    {
        foreach (var node in leftNodes.GetComponentsInChildren<Transform>())
        {
            var pos = node.position;
            if (node == rightNodes) continue;
            if (node.gameObject.layer == 8)
            {
                node.tag = pos.x > 0f ? "Node" : "Untagged";
            }
        }
    }
    
    void UpdateLeftNodes()
    {
        foreach (var node in leftNodes.GetComponentsInChildren<Transform>())
        {
            var pos = node.position;
            if (node == leftNodes) continue; 
            if (node.gameObject.layer == 6)
                node.tag = pos.x <= 0f ? "Node" : "Untagged";
        }
    }
}

