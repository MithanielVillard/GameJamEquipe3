using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Sensor : MonoBehaviour
{

    [SerializeField] private string culledTag;
    public bool isCollided;
    private Collider2D collider;

    private void Start()
    {
    }

    public float GetMaximumPoint()
    {
        if (!collider) return 0;
        return collider.transform.position.y + collider.bounds.max.y;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(culledTag))
        {
            isCollided = true;
            collider = other.GetComponent<BoxCollider2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(culledTag))
        {
            isCollided = false;
            collider = null;
        }
    }

    private void OnMouseEnter()
    {
        Debug.Log("Caca");
    }
}
