using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GraphPoint : MonoBehaviour
{
   
   
    public static PointGraph platformingGraph;
    
    // Start is called before the first frame update
    void Start()
    {
        platformingGraph = AstarData.active.graphs[0] as PointGraph;
    }

    
}
