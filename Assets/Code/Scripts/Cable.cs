using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cable : MonoBehaviour
{
    [SerializeField] public Transform startPoint;
    [SerializeField] public Transform endPoint;
    [SerializeField] public Vector3 distance;
    public LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //lineRenderer.SetColors(Color(0, 0, 0, 0))
    }

    // Update is called once per frame
    void Update()
    {
        distance = startPoint.position - endPoint.position;

        lineRenderer.SetPosition(0, new Vector3(startPoint.transform.position.x, startPoint.transform.position.y, startPoint.transform.position.z));
        lineRenderer.SetPosition(1, new Vector3(endPoint.transform.position.x, endPoint.transform.position.y, endPoint.transform.position.z));
    }
}
