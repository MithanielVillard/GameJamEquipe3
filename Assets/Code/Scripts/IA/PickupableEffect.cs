using System;
using UnityEngine;

[Serializable]
public class Effect
{
    [Header("Duration")]
    public float effectTime = 1.0f;
    public float effectProgression = 1.0f;
    
    [Header("Movement")]
    public float speedModifier = 1.0f;
    public float jumpModifier = 1.0f;
    public int priority = 0;
    public Transform destination;
    public bool lockWalking = false;

    [Header("Physics")] 
    public float gravityScale = 1.0f;

    public bool IsEnd { get; private set; }

    public void Reset()
    {
        effectProgression = 0;
        IsEnd = false;
    }

    public void Update(BasicIA ia)
    {
        if (destination)
            ia.destination = destination;
        if (effectProgression >= effectTime)
            IsEnd = true;
        effectProgression += Time.deltaTime;
    }
}

public class PickupableEffect : MonoBehaviour
{
    
    public Effect AttachedEffect;

}
