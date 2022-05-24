using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class WorldsManager : MonoBehaviour
{
    [SerializeField] private Transform rightNodes, leftNodes,buttomNodes,topNodes;
    public static bool onRight, onLeft,onTop,onButtom,CharacterMove;
    private static WorldsManager _shared;

    
    
    void Start()
    {
        _shared = this;
        UpdateLeftNodes();
        UpdateRightNodes();
        UpdateTopNodes();
        UpdateButtomNodes();
    }
    
    void Update()
    {
        if (WorldsRotate.endDrag)
        {
            print("update graph");
            WorldsRotate.endDrag = false;
            UpdateLeftNodes();
            UpdateRightNodes();
            UpdateTopNodes();
            UpdateButtomNodes();
            AstarData.active.Scan();
        }
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
        foreach (var node in buttomNodes.GetComponentsInChildren<Transform>())
        {
            var pos = node.position;
            if (node == buttomNodes){ continue; }
            node.tag = (pos.y < 0 &&  Mathf.Abs(pos.x) <= Mathf.Abs(pos.y))?"Node":"Untagged";
        }
    } 

    

}
