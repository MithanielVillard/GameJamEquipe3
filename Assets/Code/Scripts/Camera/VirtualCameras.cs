using Cinemachine;
using UnityEngine;

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
        FollowPlayer followPlayerScript = followCamera.GetComponent<FollowPlayer>();
        debugCamera.Priority = 0;
        if (followPlayer)
        {
            followCamera.Priority = 1;
            followPlayerScript.enabled = true;
            followPlayerScript.xLimits = new Vector2(startCamera.transform.position.x, endCamera.transform.position.x);
            enabled = false;
            return;
        }
        startCamera.Priority = 1;
        followPlayerScript.enabled = false;
    }
    
    private void Update()
    {
        progress += Time.deltaTime;
        if (progress < 0.1f) return;
        endCamera.Priority = 2;
        enabled = false;
    }
}
