using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRight : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * 2);
    }
}
