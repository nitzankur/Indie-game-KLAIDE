using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;



public class WorldsManagerToturial : MonoBehaviour
{
    [SerializeField] private Transform right, left;
    [SerializeField] private Transform rightNodes, leftNodes;
    public static bool onRight, onLeft,DragRight,DragLeft,CharacterMove;
    [Range(20, 150)] [SerializeField] private float fastParameter = 150;

    private static WorldsManagerToturial _shared;
   
    void Start()
    {
        _shared = this;
    }

    
    void Update()
    {
        if (WorldRotateTutorial.endDrag)
        {
            WorldRotateTutorial.endDrag = false;
            foreach (var node in leftNodes.GetComponentsInChildren<Transform>())
            {
                var pos = node.position;
                if (node == leftNodes) continue;
                node.tag = pos.x <= 0 ? "Node" : "Untagged";
            }
            
            foreach (var node in rightNodes.GetComponentsInChildren<Transform>())
            {
                var pos = node.position;
                if (node == rightNodes) continue;
                node.tag = pos.x > 0 ? "Node" : "Untagged";
            }
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
    }
}

