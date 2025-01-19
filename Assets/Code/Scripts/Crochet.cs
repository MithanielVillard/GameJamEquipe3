using UnityEngine;

public class Crochet : MonoBehaviour
{
    private Cable _cable;

    void Start()
    {
        _cable = transform.parent.GetComponent<Cable>();
    }

    void Update()
    {
        transform.up = _cable.distance.normalized;

        if (transform.position.y > transform.parent.GetChild(0).transform.position.y)
        {
            transform.position.Set(transform.position.x, transform.parent.GetChild(0).transform.position.y,
                transform.position.z);
        }
    }
}
