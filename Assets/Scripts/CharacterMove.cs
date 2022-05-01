
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Obstacle")) GameManager.Trigger = true;
  }
  
}
