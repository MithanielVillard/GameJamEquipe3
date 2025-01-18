using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{

    public bool enterCollision = false;
    private GameObject objectP;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enterCollision)
        {
            transform.position = objectP.transform.position;
            rb.velocity = objectP.GetComponent<Rigidbody2D>().velocity;
            GetComponent<PolygonCollider2D>().enabled = false;
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
