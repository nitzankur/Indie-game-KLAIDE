using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;



public class WorldsManagerToturial : MonoBehaviour
{
    [SerializeField] private Transform rightNodes, leftNodes;
    public static bool onRight, onLeft,CharacterMove;
    private static WorldsManagerToturial _shared;
    [SerializeField] private GameObject right, left;
    private static Transform leftTransform, rightTransform;
    
    
    void Start()
    {
        _shared = this;
        UpdateLeftNodes();
        UpdateRightNodes();
        leftTransform = left.transform;
        rightTransform = right.transform;
    }
    
    void Update()
    {
        print(WorldRotateTutorial.EndDrag + " end drag");
        if (WorldRotateTutorial.EndDrag)
        {
            print("update graph");
            WorldRotateTutorial.EndDrag = false;
            UpdateLeftNodes();
            UpdateRightNodes();
            AstarData.active.Scan();
        }
    }

    // public static void RightRotate(float angle,float angleOffset)
    // {
    //    rightTransform.eulerAngles = new Vector3(0, 0, angle + angleOffset);
    // }
    public static void RightRotate(Vector3 startPos,Vector3 curPos)
    {
        startPos.z = 0;
        curPos.z = 0;
        rightTransform.rotation = Quaternion.FromToRotation(startPos, curPos);
    }

    // public static void LeftRotate(float angle,float angleOffset)
    // {
    //     leftTransform.eulerAngles = new Vector3(0, 0, angle + angleOffset);
    // }
    public static void LeftRotate(Vector3 startPos,Vector3 curPos)
    {
        startPos.z = 0;
        curPos.z = 0;
        leftTransform.rotation = Quaternion.FromToRotation(startPos, curPos);
        print("left rot" + leftTransform.rotation);
    }
    
    
    void UpdateRightNodes()
    {
        foreach (var node in rightNodes.GetComponentsInChildren<Transform>())
        {
            var pos = node.position;
            if (node == rightNodes){ continue; }
            node.tag = pos.x <= 0 ? "Untagged" : "Node";
        }
    }
    
    void UpdateLeftNodes()
    {
        foreach (var node in leftNodes.GetComponentsInChildren<Transform>())
        {
            var pos = node.position;
            if (node == leftNodes){ continue; }
            node.tag = pos.x > 0 ? "Untagged" : "Node";
        }
    } 
  
}

