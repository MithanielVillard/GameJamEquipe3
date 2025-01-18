using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{

    private bool enterCollision = false;
    private GameObject objectP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enterCollision)
        {
            transform.position = objectP.transform.position;
        }
    }

    public void OnTriggerEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Grue"))
        {
            objectP = collision.gameObject;
            enterCollision = true;
            Debug.Log("GameObject1 collided with ");
        }
    }
}
