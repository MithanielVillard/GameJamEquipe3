using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class VirtualCameraEnd : MonoBehaviour
{
    private float progress;

    // Update is called once per frame
    void Update()
    {
        progress += Time.deltaTime;
        if (progress < 0.1f) return;
        GetComponent<CinemachineVirtualCamera>().Priority = 2;
        GetComponent<VirtualCameraEnd>().enabled = false;
    }
}
