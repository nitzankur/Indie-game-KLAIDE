using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WorldsManagerToturial : MonoBehaviour
{
    [SerializeField] private Transform right, left;
    public static bool onRight, onLeft,DragRight,DragLeft,CharacterMove;
    [Range(20, 150)] [SerializeField] private float fastParameter = 150;

    private static WorldsManagerToturial _shared;
    // Start is called before the first frame update
    void Start()
    {
        _shared = this;
    }

    // Update is called once per frame
    void Update()
    {
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

