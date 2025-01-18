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
        //Quaternion rotation = Quaternion.FromToRotation(transform.parent.GetComponent<Cable>().distance, transform.right);
        //Vector3 euler = rotation.eulerAngles;
        //transform.rotation = rotation;
        var cable = transform.parent.GetComponent<Cable>();
        transform.up = cable.distance.normalized;

    }
}
