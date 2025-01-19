using System;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class Effect
{
    
    [Header("Duration")] 
    public int usageLeft = 1;
    public float effectTime = 1.0f;
    public float effectProgression = 1.0f;
    
    [Header("Movement")]
    public float speedModifier = 1.0f;
    public float jumpModifier = 1.0f;
    public int forceDirection;

    [Header("Physics")] 
    public float gravityScale = 1.0f;
    public bool kill;

    public bool IsEnd { get; private set; }
    

    public bool Use()
    {
        Debug.Log(IsEnd);
        if (!IsEnd && usageLeft == -1) return false;
        if (usageLeft == -1) return true;
        if (usageLeft == 0) return false;
        usageLeft -= 1;
        return true;
    }
    
    public void Reset()
    {
        effectProgression = 0;
        IsEnd = false;
    }

    public void Update(BasicIA ia)
    {
        if (kill)
            ia.Die();
        if (effectProgression >= effectTime)
            IsEnd = true;
        effectProgression += Time.deltaTime;
    }
}

public class PickupableEffect : MonoBehaviour
{
    
    public Effect AttachedEffect;
}
