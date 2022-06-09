using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishGenrate : MonoBehaviour
{
    [SerializeField] private GameObject fish1, fish2;
    [SerializeField] private Vector3 pos;
    private List<GameObject> fishList = new List<GameObject>();
    private IEnumerator _coro;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitAndLoad());
    }

    // Update is called once per frame
    IEnumerator GanerateFish1()
    {
        while (true)
        {
            fishList.Add(fish1);
            fishList.Add(fish2);
            var waitFor = Random.Range(5, 7);
            int randFish = Random.Range(0, 2);
            var fish = fishList[randFish];
            yield return new WaitForSeconds(waitFor);
            Instantiate(fish, pos, Quaternion.identity, transform);   
        }
    }

    IEnumerator waitAndLoad()
    {
        yield return new WaitForSeconds(1);
        _coro = GanerateFish1();
        StartCoroutine(_coro);
    }

}
