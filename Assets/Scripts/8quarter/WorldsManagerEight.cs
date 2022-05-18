using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class WorldsManagerEight : MonoBehaviour
{
    [SerializeField] private Transform rightNodes,leftNodes,bottomNodes,topNodes,
        rightInsideNodes,leftInsideNodes,bottomInsideNodes,topInsideNodes;
    public static bool onRight, onLeft,onTop,onBottom, onInsideRight, onInsideLeft,onInsideTop,onInsideBottom, CharacterMove;
    private static WorldsManagerEight _shared;
    [SerializeField] private GameObject right,left,top,bottom, rightInside,leftInside,topInside,bottomInside;
    private static Transform leftTransform, rightTransform,topTransform,
        bottomTransform,leftInsideTransform, rightInsideTransform,topInsideTransform,bottomInsideTransform;
    
    
    void Start()
    {
        _shared = this;
        UpdateOutsideCircle();
        leftTransform = left.transform;
        rightTransform = right.transform;
        bottomTransform = bottom.transform;
        topTransform = top.transform;
        
        //Inside circle:
        UpdateInsideCircle();
        leftInsideTransform = leftInside.transform;
        rightInsideTransform = rightInside.transform;
        bottomInsideTransform = bottomInside.transform;
        topInsideTransform = topInside.transform;
    }
    
    void Update()
    {
        if (WorldsRotateEight.endDrag)
        {
            WorldsRotateEight.endDrag = false;
            UpdateOutsideCircle();
            UpdateInsideCircle();
            AstarData.active.Scan();
        }
    }

    private void UpdateOutsideCircle()
    {
        UpdateLeftNodes();
        UpdateRightNodes();
        UpdateTopNodes();
        UpdateButtomNodes();
    }
    
    private void UpdateInsideCircle()
    {
        UpdateInsideLeftNodes();
        UpdateInsideBottomNodes();
        UpdateInsideRightNodes();
        UpdateInsideTopNodes();
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
        bottomTransform.eulerAngles = new Vector3(0, 0, angle + angleOffset);
    }
    
    //Inside circle:
    
    public static void RightInsideRotate(float angle,float angleOffset)
    {
        rightInsideTransform.eulerAngles = new Vector3(0, 0, angle + angleOffset);
    }
    public static void LeftInsideRotate(float angle,float angleOffset)
    {
        leftInsideTransform.eulerAngles = new Vector3(0, 0, angle + angleOffset);
    }
    public static void TopInsideRotate(float angle,float angleOffset)
    {
        topInsideTransform.eulerAngles = new Vector3(0, 0, angle + angleOffset);
    }
    public static void ButtomInsideRotate(float angle,float angleOffset)
    {
        bottomInsideTransform.eulerAngles = new Vector3(0, 0, angle + angleOffset);
    }


    

    void UpdateRightNodes()
    {
        foreach (var node in rightNodes.GetComponentsInChildren<Transform>())
        {
            var pos = node.position;
            if (node == rightNodes){ continue; }
            
            node.tag = (pos.x > 0 && pos.x > Mathf.Abs(pos.y)) ? "Node":"Untagged" ;
        }
    }
    void UpdateLeftNodes()
    {
        foreach (var node in leftNodes.GetComponentsInChildren<Transform>())
        {
            var pos = node.position;
            if (node == leftNodes){ continue; }
            node.tag = (pos.x<0 && Mathf.Abs(pos.x) > Mathf.Abs(pos.y)) ? "Node":"Untagged";
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
        foreach (var node in bottomNodes.GetComponentsInChildren<Transform>())
        {
            var pos = node.position;
            if (node == bottomNodes){ continue; }
            node.tag = (pos.y < 0 &&  Mathf.Abs(pos.x) <= Mathf.Abs(pos.y))?"Node":"Untagged";
        }
    } 

    //Update Inside circle nodes:

    void UpdateInsideRightNodes()
    {
        foreach (var node in rightInsideNodes.GetComponentsInChildren<Transform>())
        {
            var pos = node.position;
            if (node == rightInsideNodes){ continue; }
            
            node.tag = (pos.x > 0 && pos.x > Mathf.Abs(pos.y)) ? "Node":"Untagged" ;
        }
    }
    void UpdateInsideLeftNodes()
    {
        foreach (var node in leftInsideNodes.GetComponentsInChildren<Transform>())
        {
            var pos = node.position;
            if (node == leftInsideNodes){ continue; }
            node.tag = (pos.x<0 && Mathf.Abs(pos.x) > Mathf.Abs(pos.y)) ? "Node":"Untagged";
        }
    } 
    
    void UpdateInsideTopNodes()
    {
        foreach (var node in topInsideNodes.GetComponentsInChildren<Transform>())
        {
            var pos = node.position;
            if (node == topInsideNodes){ continue; }
            node.tag =(pos.y > 0 && pos.y >= Mathf.Abs(pos.x))? "Node":"Untagged";
        }
    } 
    
    void UpdateInsideBottomNodes()
    {
        foreach (var node in bottomInsideNodes.GetComponentsInChildren<Transform>())
        {
            var pos = node.position;
            if (node == bottomInsideNodes){ continue; }
            node.tag = (pos.y < 0 &&  Mathf.Abs(pos.x) <= Mathf.Abs(pos.y))?"Node":"Untagged";
        }
    } 
    
    
    
    

}
