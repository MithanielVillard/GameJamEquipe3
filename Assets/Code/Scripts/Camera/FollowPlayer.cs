using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject IA;

    void Update()
    {
        Vector3 playerPosition = IA.transform.position;
        playerPosition.y = Mathf.Max(playerPosition.y, 0);
        playerPosition.z = transform.position.z;
        transform.position = playerPosition;
    }
}
