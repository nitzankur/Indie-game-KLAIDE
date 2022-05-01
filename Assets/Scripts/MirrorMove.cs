using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rightUp, rightDown, leftUp, leftDown;

    [SerializeField] private int speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool move = true; 
        if (Input.GetKey(KeyCode.LeftArrow))
        {
           MoveHorizontal(false);
           move = false;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveHorizontal(true);
            move = false;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveVertical(false);
            move = false;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveVertical(true);
            move = true;
        }
        else if (move||GameManager.Trigger)
        {
            NoMove();
        }
        
    }

    void MoveHorizontal(bool right)
    {
        if (right)
        {
            rightUp.AddForce(Vector2.right *speed);
            rightDown.AddForce(Vector2.right*speed);
            leftDown.AddForce(Vector2.left*speed);
            leftUp.AddForce(Vector2.left*speed);
        }
        else
        {
            rightUp.AddForce(Vector2.left *speed);
            rightDown.AddForce(Vector2.left*speed);
            leftDown.AddForce(Vector2.right*speed);
            leftUp.AddForce(Vector2.right*speed);
        }
    }

    void MoveVertical(bool up)
    {
        if (up)
        {
            rightUp.AddForce(Vector2.up*speed);
            leftUp.AddForce(Vector2.up*speed);
            rightDown.AddForce(Vector2.down*speed);
            leftDown.AddForce(Vector2.down*speed);
        }
        else
        {
            rightDown.AddForce(Vector2.up*speed);
            leftDown.AddForce(Vector2.up*speed);
            rightUp.AddForce(Vector2.down*speed);
            leftUp.AddForce(Vector2.down*speed);
        }
    }

    void NoMove()
    {
        rightUp.velocity = Vector2.zero;
        rightDown.velocity = Vector2.zero;
        leftDown.velocity = Vector2.zero;
        leftUp.velocity = Vector2.zero; 
    }
}
