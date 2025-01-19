using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject IA;
    public Vector2 xLimits { private get; set; }

    private void Start()
    {
        IA = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    private void Update()
    {
        Vector3 playerPosition = IA.transform.position;
        playerPosition.y = Mathf.Max(playerPosition.y, 0);
        playerPosition.x = Mathf.Clamp(playerPosition.x, xLimits.x, xLimits.y);
        playerPosition.z = transform.position.z;
        transform.position = playerPosition;
    }
}
