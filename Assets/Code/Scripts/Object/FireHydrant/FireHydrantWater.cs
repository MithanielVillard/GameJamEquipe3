using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHydrantWater : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("cco");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 600));
        }
    }
}
