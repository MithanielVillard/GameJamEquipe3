using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Lock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 0.3f).SetEase(Ease.OutBounce);
        transform.DOPunchRotation(new Vector3(0, 0, 20), 1).onComplete = () =>
        {
            transform.DOScale(new Vector3(0.0f, 0.0f, 0.0f), 0.3f).SetEase(Ease.OutBounce).onComplete = () =>
            {
                Destroy(this);
            };
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
