using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRight : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float max;

    private void Update()
    {
        if (transform.position.x < max)
        { 
            transform.Translate(Vector2.right * Time.deltaTime * speed); 
        }
    }
}
