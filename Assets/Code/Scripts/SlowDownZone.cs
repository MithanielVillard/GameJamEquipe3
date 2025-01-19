using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") == false) return;

        collision.transform.GetComponent<BasicIA>().enabled = false;
        collision.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}
