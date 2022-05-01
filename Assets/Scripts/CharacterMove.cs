
using System;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
  private void OnTriggerStay2D(Collider2D other)
  {
    if (other.CompareTag("Obstacle"))
    {
      print("Obstacle");
      GameManager.Trigger = true;
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Obstacle"))
    {
      print("Obstacle exit");
      GameManager.Trigger = false;
    }
  }
}
