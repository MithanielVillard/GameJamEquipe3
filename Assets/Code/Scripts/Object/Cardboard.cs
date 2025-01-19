using DG.Tweening;
using UnityEngine;

public class Cardboard : MonoBehaviour
{

    [SerializeField] private Sprite open;
    [SerializeField] private Sprite closed;

    private bool _hasPlayer;
    private ParticleSystem _particleSystem;
    private GameObject _ia;
    private SpriteRenderer _render;
    void Start()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _render = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BasicIA ia))
        {
            _ia = ia.gameObject;
            transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 1);
            transform.DOShakeRotation( 1, new Vector3(0, 0, 10));
            foreach (SpriteRenderer rend in GetComponentsInChildren<SpriteRenderer>())
            {
                rend.sprite = closed;
            }
            _hasPlayer = true;
            _particleSystem.Play();
            _ia.SetActive(false);
        }
    }

    public void OnRelease(GrabbableObject obj)
    {
        _render.material.SetColor("_Color", Color.green);
        if (!_hasPlayer || obj.BehindObject != null) return;
        //transform.DOPunchScale(new Vector3(0.6f, 0.6f, 0.6f), 1);

        _ia.SetActive(true);
        _ia.transform.position = transform.position - new Vector3(0, 1, 0);
        transform.DOShakeRotation( 1, new Vector3(0, 0, 5));
        _hasPlayer = false;

        _render.sprite = open;
        _particleSystem.Play();
    }
}
