using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparkle : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndLoad());
    }
    

    // Update is called once per frame
    IEnumerator WaitAndLoad()
    {
        var waitTime = Random.value*Random.value * 50;
        yield return new WaitForSeconds(waitTime);
        _animator.SetTrigger("Start");
        StartCoroutine(WaitAndLoad());
        
    }
}
