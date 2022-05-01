
using System;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public static bool collisionEnter;

    private void OnCollisionEnter2D(Collision2D collision)
  {
      Collider2D collider = collision.collider;
      if (collider.CompareTag("Obstacle"))
      {
          /*GetComponent<Rigidbody2D>().velocity = Vector2.zero;
          GetComponent<Rigidbody2D>().angularVelocity = 0;*/
          
          Vector3 contactPoint = collision.contacts[0].point;
          Vector3 center = collider.bounds.center;
          GameManager.CollLeft = contactPoint.x > center.x;
          GameManager.CollRight = center.x > contactPoint.x;
          GameManager.CollUp = contactPoint.y > center.y;
          GameManager.collDown = center.y > contactPoint.y;
          print(GameManager.CollLeft);  
      }
      
      else if (collider.CompareTag("Price"))
          GameManager.winGame = true;
      
     
  }

  private void OnCollisionExit2D(Collision2D other)
  {
      GameManager.CollRight = false;
      GameManager.CollLeft = false;
      GameManager.collDown = false;
      GameManager.CollUp = false;
  }
}
