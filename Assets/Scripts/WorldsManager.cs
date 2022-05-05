using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorldsManager : MonoBehaviour
{
    [SerializeField] private Transform right, left, top, bottom;
    public static bool onRight, onLeft, onTop, ONBottom,DragRight,DragLeft,CharacterMove;
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
        if (DragLeft)
        {
            if (!ONBottom) bottom.rotation =bottom.rotation * Quaternion.Euler(0, 0, fastParameter * Time.deltaTime);
            if (!onLeft) left.rotation =left.rotation * Quaternion.Euler(0, 0, fastParameter * Time.deltaTime);
            if (!onRight) right.rotation =right.rotation * Quaternion.Euler(0, 0, fastParameter * Time.deltaTime);
            if (!onTop)  top.rotation =top.rotation * Quaternion.Euler(0, 0, fastParameter * Time.deltaTime);
        }
        else if (DragRight)
        {
            if (!ONBottom) bottom.rotation =bottom.rotation * Quaternion.Euler(0, 0, -fastParameter * Time.deltaTime);
            if (!onLeft) left.rotation =left.rotation * Quaternion.Euler(0, 0, -fastParameter * Time.deltaTime);
            if (!onRight) right.rotation =right.rotation * Quaternion.Euler(0, 0, -fastParameter * Time.deltaTime);
            if (!onTop)  top.rotation =top.rotation * Quaternion.Euler(0, 0, -fastParameter * Time.deltaTime);
        }
    }
}
