using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class WorldsManager : MonoBehaviour
{
    [SerializeField] private Transform rightNodes, leftNodes,buttomNodes,topNodes;
    public static bool onRight, onLeft,onTop,onButtom,CharacterMove;
    private static WorldsManager _shared;
    [SerializeField] private GameObject right, left,top,buttom;
    private static Transform leftTransform, rightTransform,topTransform,buttomTransform;
    
    
    void Start()
    {
        _shared = this;
        UpdateLeftNodes();
        UpdateRightNodes();
        UpdateTopNodes();
        UpdateButtomNodes();
        leftTransform = left.transform;
        rightTransform = right.transform;
        buttomTransform = buttom.transform;
        topTransform = top.transform;
    }
    
    void Update()
    {
        if (WorldRotateTutorial.endDrag)
        {
            print("update graph");
            WorldRotateTutorial.endDrag = false;
            UpdateLeftNodes();
            UpdateRightNodes();
            UpdateTopNodes();
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
    public static void TopRotate(float angle,float angleOffset)
    {
       topTransform.eulerAngles = new Vector3(0, 0, angle + angleOffset);
    }
    public static void ButtomRotate(float angle,float angleOffset)
    {
        buttomTransform.eulerAngles = new Vector3(0, 0, angle + angleOffset);
    }
    
    void UpdateRightNodes()
    {
        foreach (var node in rightNodes.GetComponentsInChildren<Transform>())
        {
            var pos = node.position;
            if (node == rightNodes){ continue; }
            node.tag = (pos.x<0 && Mathf.Abs(pos.x) > Mathf.Abs(pos.y)) ? "Node":"Untagged" ;
        }
    }
    void UpdateLeftNodes()
    {
        foreach (var node in leftNodes.GetComponentsInChildren<Transform>())
        {
            var pos = node.position;
            if (node == leftNodes){ continue; }
            node.tag = (pos.x > 0 && pos.x > Mathf.Abs(pos.y)) ? "Node":"Untagged";
        }
    } 
    
    void UpdateTopNodes()
    {
        foreach (var node in topNodes.GetComponentsInChildren<Transform>())
        {
            var pos = node.position;
            if (node == topNodes){ continue; }
            node.tag =(pos.y > 0 && pos.y >= Mathf.Abs(pos.x))? "Node":"Untagged";
        }
    } 
    
    void UpdateButtomNodes()
    {
        foreach (var node in buttomNodes.GetComponentsInChildren<Transform>())
        {
            var pos = node.position;
            if (node == buttomNodes){ continue; }
            node.tag = (pos.y < 0 &&  Mathf.Abs(pos.x) <= Mathf.Abs(pos.y))?"Node":"Untagged";
        }
    } 

    

}
