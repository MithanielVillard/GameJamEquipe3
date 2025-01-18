using DG.Tweening;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    
    [SerializeField, Header("Main")] private bool isSelectable;
    [SerializeField, Header("Stats")] private Vector2 shadowOffset;
    [SerializeField] private Color shadowColor;
    
    public static GrabbableObject CurrentObject;
    
    private GameObject _shadow;
    private Vector3 _startScale;
    
    void OnMouseEnter()
    {
        CurrentObject = this;
    }

    private void OnMouseExit()
    {
        CurrentObject = null;
    }

    public void BeginDrag()
    {
        transform.DOScale(transform.localScale * 1.5f, 0.3f).SetEase(Ease.OutElastic);
        _shadow = Instantiate(gameObject, transform, true);
        _shadow.transform.localPosition = new Vector3(shadowOffset.x, shadowOffset.y, 0f);
        Destroy(_shadow.GetComponent<GrabbableObject>());
        if (_shadow.TryGetComponent(out SpriteRenderer renderer))
        {
            renderer.color = shadowColor;
            renderer.sortingOrder = -1;
        }

    }

    public void EndDrag()
    {
        transform.DOScale(transform.localScale / 1.5f, 0.3f).SetEase(Ease.OutElastic);
        Destroy(_shadow);
    }
}
