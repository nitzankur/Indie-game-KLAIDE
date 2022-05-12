using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkPress : MonoBehaviour
{
    [SerializeField] private List<Vector3> listPositions;
    [SerializeField] private int currentIndex;

    private void Start()
    {
        transform.position = listPositions[currentIndex];
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (currentIndex != listPositions.Count)
                currentIndex++;
            gameObject.SetActive(false);
        }
        
    }
}
