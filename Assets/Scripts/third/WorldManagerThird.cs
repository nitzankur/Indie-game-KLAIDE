using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class WorldManagerThird : MonoBehaviour
{
    [SerializeField] private Transform rightNodes, leftNodes,buttomNodes;
    public static bool onRight, onLeft,CharacterMove,onButtom;
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
        print(WorldRotateThird.EndDrag + " end drag");
        if (WorldRotateThird.EndDrag)
        {
            print("update graph");
            WorldRotateThird.EndDrag = false;
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
            node.tag = (pos.x > 0f && (pos.y >0 || pos.y <0 &&pos.x>Mathf.Abs(pos.y)+1)) ? "Node": "Untagged";
        }
    }
    
    void UpdateLeftNodes()
    {
        foreach (var node in leftNodes.GetComponentsInChildren<Transform>())
        {
            var pos = node.position;
            if (node == leftNodes){ continue; }
            node.tag = (pos.x <= 0f &&(pos.y >0 || pos.y <0 &&pos.x<pos.y-1f) ) ? "Node":"Untagged" ;
        }
    }

    void UpdateButtomNodes()
    {
        int count = 0;
        foreach (var node in buttomNodes.GetComponentsInChildren<Transform>())
        {
            count++;
            var pos = node.position;
            if (node == buttomNodes){ continue; }
            node.tag = (pos.y<0 && ((pos.x<0 && (pos.x >pos.y-1f))|| pos.x>0 && (pos.x<Mathf.Abs(pos.y)+1f))) ? "Node":"Untagged";
        }
    }
  
}
