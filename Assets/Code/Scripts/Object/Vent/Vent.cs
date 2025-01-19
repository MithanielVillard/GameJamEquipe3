using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    [SerializeField] private GameObject otherVent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float posOffset = 0;
            if (otherVent.transform.position.x > gameObject.transform.position.x)
            {
                posOffset += 2;
            }
            else
            {
                posOffset -= 2;
            }
            collision.transform.position = otherVent.transform.position + new Vector3(posOffset, 3);
        }
    }
}
