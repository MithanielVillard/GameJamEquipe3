using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Sensor : MonoBehaviour
{

    [SerializeField] private string culledTag;
    public bool isCollided;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(culledTag))
        {
            isCollided = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(culledTag))
        {
            isCollided = false;
        }
    }
    
}
