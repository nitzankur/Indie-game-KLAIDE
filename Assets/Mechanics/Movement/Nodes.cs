using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Nodes : MonoBehaviour
{
    private PointGraph graph;
    void Start()
    {
       graph = GraphPoint.platformingGraph;
    }
    

    public void DestroyNode()
    {
        for (int i = 0; i < graph.nodes.Length; i++)
        {
            if (graph.nodes[i].gameObject == gameObject)
            {
                graph.nodes[i].Destroy();
            }
        }
    }
}
