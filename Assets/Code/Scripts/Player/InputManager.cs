using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private ObjectGrabber _grabber;
    private Grab _grab;

    private void Start()
    {
        _grabber = GetComponent<ObjectGrabber>();
        _grab = GameObject.FindWithTag("Player").GetComponentInParent<Grab>();
    }
    
    public void OnMousePressed(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            _grabber.OnMousePressed();
        
        if (ctx.canceled)
            _grabber.OnMouseReleased();
    }

    public void OnFPressed(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            {
                if (_grab.enterCollision)
                {
                    _grab.enterCollision = false;
                    
                    StartCoroutine(ReleaseAfter(0.2f));
                }
            }
        }
    }

    IEnumerator ReleaseAfter(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        _grab.GetComponent<BoxCollider2D>().enabled = true;
    }

}
