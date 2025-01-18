using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampo : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float launchForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (launchForce <= 0.2)
        {
            launchForce = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("YO");
        if (collision.gameObject.tag == "Trampo")
        {
            rb.velocity = Vector2.up * launchForce;
            launchForce /= 1.5f;
        }
    }
}
