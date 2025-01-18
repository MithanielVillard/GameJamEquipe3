using System;
using UnityEngine;

[Serializable]
public class Effect
{
    [Header("Duration")]
    public float effectTime = 1.0f;
    public float effectProgression = .0f;
    
    [Header("Movement")]
    public float speedModifier = 1.0f;
    public float jumpModifier = 1.0f;
    public Transform destination;
    public bool lockWalking = false;

    public bool IsEnd { get; private set; }

    public void Reset()
    {
        speedModifier = 1;
        jumpModifier = 1;
        lockWalking = false;
    }

    public void Update(BasicIA ia)
    {
        if (destination)
            ia.destination = destination;
        if (effectProgression > effectTime)
            IsEnd = true;
        effectProgression += Time.deltaTime;
    }
}

public class PickupableEffect : MonoBehaviour
{
    
    public Effect AttachedEffect;

}
