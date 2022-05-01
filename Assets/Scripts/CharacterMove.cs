
using System;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
  private void OnCollisionEnter2D(Collision2D collision)
  {
      Collider2D collider = collision.collider;
      if (collider.CompareTag("Obstacle"))
      {
          Vector3 contactPoint = collision.contacts[0].point;
          Vector3 center = collider.bounds.center;
          GameManager.CollLeft = contactPoint.x > center.x;
          GameManager.CollRight = center.x > contactPoint.x;
          GameManager.CollUp = contactPoint.y > center.y;
          GameManager.collDown = center.y > contactPoint.y;
          print(GameManager.CollLeft);  
      }
     
  }

  private void OnCollisionExit2D(Collision2D other)
  {
      GameManager.CollRight = false;
      GameManager.CollLeft = false;
      GameManager.collDown = false;
      GameManager.CollUp = false;
  }
}
