using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class WorldManagerThird : MonoBehaviour
{
    [SerializeField] private Transform rightNodes, leftNodes,buttomNodes;
    public static bool onRight, onLeft,CharacterMove,onButtom;
    [Range(20, 200)] [SerializeField] private float fastParameter = 150;
    private static WorldManagerThird _shared;
    [SerializeField] private GameObject right, left,buttom;
    private static Transform leftTransform, rightTransform,buttomTransform;
    
    
    void Start()
    {
        _shared = this;
        UpdateLeftNodes();
        UpdateRightNodes();
        leftTransform = left.transform;
        rightTransform = right.transform;
        buttomTransform = buttom.transform;
    }
    
    void Update()
    {
        if (WorldRotateThird.endDrag)
        {
            print("update graph");
            WorldRotateThird.endDrag = false;
            UpdateLeftNodes();
            UpdateRightNodes();
            UpdateButtomNodes();
            AstarData.active.Scan();
        }
    }

    public static void RightRotate(float angle,float angleOffset)
    {
        rightTransform.eulerAngles = new Vector3(0, 0, angle + angleOffset);
    }

    public static void LeftRotate(float angle,float angleOffset)
    {
        leftTransform.eulerAngles = new Vector3(0, 0, angle + angleOffset);
    }
    public static void buttomRotate(float angle,float angleOffset)
    {
        buttomTransform.eulerAngles = new Vector3(0, 0, angle + angleOffset);
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

    void UpdateButtomNodes()
    {
        foreach (var node in buttomNodes.GetComponentsInChildren<Transform>())
        {
            var pos = node.position;
            if (node == leftNodes){ continue; }
            node.tag = pos.x > 0 ? "Untagged" : "Node";
        }
    }
  
}
