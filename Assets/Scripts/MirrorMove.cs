using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rightUp, rightDown, leftUp, leftDown;


    [SerializeField] private float speed;
    // Start is called before the first frame update



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)&&!GameManager.CollLeft)
        {
           MoveHorizontal(false);
        }
        if (Input.GetKey(KeyCode.RightArrow)&&!GameManager.CollRight)
        {
            MoveHorizontal(true);
        }

        if (Input.GetKey(KeyCode.DownArrow)&&!GameManager.collDown)
        {
            MoveVertical(false);
        }

        if (Input.GetKey(KeyCode.UpArrow)&&!GameManager.CollUp)
        {
            MoveVertical(true);
        }

        
    }

    #region movement function

    void MoveHorizontal(bool right)
    {
        if (right)
        {
            rightUp.position += Vector2.right *speed;
            rightDown.position += Vector2.right*speed;
            leftDown.position += Vector2.left*speed;
            leftUp.position += Vector2.left*speed;
        }
        else
        {
            rightUp.position += Vector2.left *speed;
            rightDown.position += Vector2.left*speed;
            leftDown.position += Vector2.right*speed;
            leftUp.position += Vector2.right*speed;
        }
    }

    void MoveVertical(bool up)
    {
        if (up)
        {
            rightUp.position += Vector2.up*speed;
            leftUp.position += Vector2.up*speed;
            rightDown.position += Vector2.down*speed;
            leftDown.position += Vector2.down*speed;
        }
        else
        {
            rightDown.position += Vector2.up*speed;
            leftDown.position += Vector2.up*speed;
            rightUp.position += Vector2.down*speed;
            leftUp.position += Vector2.down*speed;
        }
    }

    void NoMove()
    {
        rightUp.velocity = Vector2.zero;
        rightDown.velocity = Vector2.zero;
        leftDown.velocity = Vector2.zero;
        leftUp.velocity = Vector2.zero; 
    }

    #endregion
   
}
