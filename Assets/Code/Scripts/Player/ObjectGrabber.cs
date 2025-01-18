using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectGrabber : MonoBehaviour
{

    [SerializeField, Header("Stats")]
    private float grabOffset = 0.5f;
    
    private GrabbableObject _DraggingObject;
    private Camera _camera;

    private Vector3 _offset;
    
    void Start()
    {
        _camera = Camera.main;
    }
    
    void Update()
    {
        if (_DraggingObject)
        {
            var pos =_camera.ScreenToWorldPoint(Input.mousePosition) + _offset;
            pos.z = 0;

            pos.x = Mathf.Clamp(pos.x, _DraggingObject.minBound.x, _DraggingObject.maxBound.x);
            pos.y = Mathf.Clamp(pos.y, _DraggingObject.minBound.y, _DraggingObject.maxBound.y);
            
            var distance = pos - _DraggingObject.transform.position;
            var rot = _DraggingObject.transform.rotation.eulerAngles;

            _DraggingObject.transform.position = Vector3.Lerp(_DraggingObject.transform.position, pos, Time.deltaTime*10.0f);

            float scalar = distance.x > 0 ? -1 : 1;
            rot.z = distance.sqrMagnitude/2 *  scalar;
            _DraggingObject.transform.rotation = Quaternion.Euler(rot);
        }
    }
    
    public void OnMousePressed()
    {
        RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if(hit.collider != null)
        {
            print(hit.collider.transform.name);
            _DraggingObject = hit.collider.GetComponent<GrabbableObject>();
            _offset = _DraggingObject.transform.position - _camera.ScreenToWorldPoint(Input.mousePosition);
            _offset.y += grabOffset;
            _DraggingObject.BeginDrag();
        }
    }

    public void OnMouseReleased()
    {
        if (_DraggingObject == null) return;
        var pos = _DraggingObject.transform.position;
        //pos.y -= grabOffset;
        _DraggingObject.transform.position = pos;
        _DraggingObject.EndDrag();
        _DraggingObject = null;
    }
}
