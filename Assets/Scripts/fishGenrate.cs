using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishGenrate : MonoBehaviour
{
    [SerializeField] private GameObject fish1, fish2;
    [SerializeField] private Vector3 pos;
    private List<GameObject> fishList;
    private IEnumerator _coro;
    // Start is called before the first frame update
    void Start()
    {
        _coro = GanerateFish1();
        StartCoroutine(_coro);
        // fishList.Add(fish1);
        // fishList.Add(fish2);
    }

    // Update is called once per frame
    IEnumerator GanerateFish1()
    {
        while (true)
        {
            var waitFor = Random.Range(5, 7);
            var randFish = Random.Range(1, 2);
            // var fish = fishList[randFish];
            yield return new WaitForSeconds(waitFor);
            Instantiate(fish1, pos, Quaternion.identity, transform);   
        }
    }
    
}
