using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    
    [SerializeField, Header("Stats")] private Vector2 shadowOffset = new Vector2(0, -3);
    [SerializeField] private Color shadowColor;
    [field:SerializeField] public bool UseBound { get; set; }
    [field:SerializeField] public Vector2 MinBound { get; set; }
    [field:SerializeField] public Vector2 MaxBound { get; set; }

    public GameObject BehindObject { get; private set; }
    
    private GameObject _shadow;
    private Vector3 _startScale;
    private SpriteRenderer _renderer;
    private Material _mat;
    private BoxCollider2D _collider;
    private bool HasCreatedRigidbody;

    private void Start()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _mat = _renderer.material;
        _collider = GetComponent<BoxCollider2D>();
    }

    public void BeginDrag()
    {
        transform.DOScale(transform.localScale * 1.5f, 0.3f).SetEase(Ease.OutElastic);

        _collider.size /= 1.5f;
        _shadow = Instantiate(_renderer.gameObject, transform, true);
        _shadow.transform.localPosition = new Vector3(shadowOffset.x, shadowOffset.y, 0f);
        _renderer.sortingOrder = 11;
        _mat.SetInt("_Enabled", 1);

        Rigidbody2D rb;
        if (!transform.TryGetComponent(out rb))
        {
            rb = transform.AddComponent<Rigidbody2D>();
            HasCreatedRigidbody = true;
            rb.gravityScale = 0.0f;
        }
        
        rb.excludeLayers = LayerMask.GetMask("Ignore Raycast");   
        
        DeactivateChildrenCollision();
        if (_shadow.TryGetComponent(out SpriteRenderer renderer))
        {
            Material mat = new Material(Shader.Find("Sprites/Default"));    
            renderer.material = mat;
            renderer.color = shadowColor;
            
            renderer.sortingOrder = 10;
        }

    }

    public void EndDrag()
    {
        if (HasCreatedRigidbody)
            Destroy(GetComponent<Rigidbody2D>());
        _collider.size *= 1.5f;
        ActivateChildrenCollision();
        _mat.SetInt("_Enabled", 0);
        transform.DOScale(transform.localScale / 1.5f, 0.3f).SetEase(Ease.OutElastic);
        Destroy(_shadow);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        _mat.SetColor("_Color", Color.red);
        BehindObject = other.gameObject;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        _mat.SetColor("_Color", Color.green);
        BehindObject = null;
    }

    private void DeactivateChildrenCollision()
    {
        foreach (Collider2D co in GetComponentsInChildren<Collider2D>())
        {
            co.isTrigger = true;
        }
    }

    private void ActivateChildrenCollision()
    {
        foreach (Collider2D co in GetComponentsInChildren<Collider2D>())
        {
            co.isTrigger = false;
        } 
    }
}
