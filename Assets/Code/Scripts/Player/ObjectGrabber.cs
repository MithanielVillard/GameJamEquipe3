using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ObjectGrabber : MonoBehaviour
{

    [SerializeField] private Lock _lock;
    [SerializeField, Header("Stats")]
    private float grabOffset = 0.5f;
    
    public delegate void ClickedCallback(GrabbableObject obj);
    public ClickedCallback ClickedCallbackAction { get; set; }
    
    
    
    private GrabbableObject _DraggingObject;
    private Camera _camera;
    private Vector3 _offset;
    private Vector3 _startPos;
    
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

            if (_DraggingObject.UseBound)
            {
                pos.x = Mathf.Clamp(pos.x, _DraggingObject.MinBound.x, _DraggingObject.MaxBound.x);
                pos.y = Mathf.Clamp(pos.y, _DraggingObject.MinBound.y, _DraggingObject.MaxBound.y);
            }
            
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
            if (hit.collider.CompareTag("Ground"))
            {
                var l = Instantiate(_lock);
                l.transform.position = hit.transform.position;
                return;
            }
            _DraggingObject = hit.collider.GetComponent<GrabbableObject>();
            _startPos = _DraggingObject.transform.position;
            _offset = _startPos - _camera.ScreenToWorldPoint(Input.mousePosition);
            _offset.y += grabOffset;
            _DraggingObject.BeginDrag();
            ClickedCallbackAction(_DraggingObject);
        }
    }

    public void OnMouseReleased()
    {
        
        if (_DraggingObject == null) return;
        if (_DraggingObject.BehindObject != null)
        {
            if (_DraggingObject.BehindObject.TryGetComponent(out BasicIA ia))
            {
                ia.Die();
            }
            _DraggingObject.transform.DOMove(_startPos, 0.5f).SetEase(Ease.InOutExpo);
        }
        
        var pos = _DraggingObject.transform.position;
        pos.y -= grabOffset;
        _DraggingObject.transform.position = pos;
        _DraggingObject.EndDrag();
        _DraggingObject = null;
    }
}
