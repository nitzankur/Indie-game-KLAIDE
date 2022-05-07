using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;


public class WorldsManager : MonoBehaviour
{
    [SerializeField] private Transform right, left, top, bottom;
    [SerializeField] private Transform rightNodes, leftNodes, topNodes, bottomNodes;
    public static bool onRight, onLeft, onTop, ONBottom,Drag;
    [Range(20, 150)] [SerializeField] private float fastParameter = 150;

    
    private static WorldsManager _shared;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _shared = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Drag)
        {
            if (!ONBottom)
            {
                print("bottom");
                bottom.rotation =bottom.rotation * Quaternion.Euler(0, 0, -fastParameter * Time.deltaTime);
            }

            if (!onLeft)
            {
                print("left");
                left.rotation =left.rotation * Quaternion.Euler(0, 0, -fastParameter * Time.deltaTime);
            }
            if (!onRight)
            {
                print("right rot");
                right.rotation =right.rotation * Quaternion.Euler(0, 0, -fastParameter * Time.deltaTime);
            }

            if (!onTop)
            {
                print("left top");
                top.rotation =top.rotation * Quaternion.Euler(0, 0, -fastParameter * Time.deltaTime);
            }
            AstarData.active.Scan();
        }

        foreach (var node in bottomNodes.GetComponentsInChildren<Transform>())
        {
            var pos = node.position;
            if (node == bottomNodes){ continue; }
            if (pos.y >= 0 || Mathf.Abs(pos.x) > Mathf.Abs(pos.y))
            {
                node.tag = "Untagged";
            }
            else
            {
                node.tag = "Node";
            }
        }
        
        foreach (var node in rightNodes.GetComponentsInChildren<Transform>())
        {
            if (node == rightNodes){ continue; }
            print(node.name);
            var pos = node.position;
            if (pos.x <= 0 || pos.x <= Mathf.Abs(pos.y))
            {
                //node.gameObject.SetActive(false);
                node.tag = "Untagged";
            }
            else
            {
                node.tag = "Node";
            }
        }
    }
    
}
