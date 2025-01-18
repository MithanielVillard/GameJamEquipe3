using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Crochet : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        var cable = transform.parent.GetComponent<Cable>();
        transform.up = cable.distance.normalized;

        if (transform.position.y > transform.parent.GetChild(0).transform.position.y)
        {
            transform.position.Set(transform.position.x, transform.parent.GetChild(0).transform.position.y, transform.position.z);
        }

    }
}
