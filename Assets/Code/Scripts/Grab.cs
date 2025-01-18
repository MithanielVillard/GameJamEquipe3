using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{

    public bool enterCollision = false;
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Grue"))
        {
            objectP = collider.gameObject;
            enterCollision = true;
        }
    }
}
