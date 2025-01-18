using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class VirtualCamera : MonoBehaviour
{
    [Header("Booleans")]
    [SerializeField] private bool followPlayer;
    
    [Header("Virtual Cameras")]
    [SerializeField] private CinemachineVirtualCamera startCamera;
    [SerializeField] private CinemachineVirtualCamera endCamera;
    [SerializeField] private CinemachineVirtualCamera debugCamera;
    [SerializeField] private CinemachineVirtualCamera followCamera;
    
    private float progress;

    private void Start()
    {
        debugCamera.Priority = 0;
        if (followPlayer)
        {
            followCamera.Priority = 1;
            enabled = false;
            return;
        }
        startCamera.Priority = 1;
    }
    
    private void Update()
    {
        progress += Time.deltaTime;
        if (progress < 0.1f) return;
        endCamera.Priority = 2;
        enabled = false;
    }
}
