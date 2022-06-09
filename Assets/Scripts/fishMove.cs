
using UnityEngine;

public class fishMove : MonoBehaviour
{
    [SerializeField] private Vector2 veloc= new Vector2(5,0);
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = veloc;
    }
    
    void OnBecameInvisible() {
        Destroy(gameObject);
    }
    
}
