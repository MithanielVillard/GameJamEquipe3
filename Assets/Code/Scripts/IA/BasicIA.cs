using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class BasicIA : MonoBehaviour
{

    [Header("Movements Properties")] 
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTime;
    [SerializeField] private float jumpProgress;
    [SerializeField] private bool canJump;
    [SerializeField] private float fallStartY;
    [SerializeField] private bool hasStartedFall;
    [SerializeField] private bool groundedOnBin;
    
    [Header("States Manager")]
    [SerializeField] private Sensor groundSensor;
    [SerializeField] private Sensor lWallSensor;
    [SerializeField] private Sensor rWallSensor;
    
    [Header("Points of Interest")]
    [SerializeReference] public List<Effect> _Effects;
    [SerializeField] private SuicidePoint nearestSuicidePoint;
    [SerializeField] private Transform levelStart; 
    [SerializeField] private Transform levelEnd;
    
    [Header("Other parameters")]
    [SerializeField] private TextMeshProUGUI winText;
    public Transform destination;
    
    private Animator _stateMachine;
    private Rigidbody2D _rigidbody;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _movementX;

    [SerializeField] private float respawnTime;
    [SerializeField] private float respawnProgress;

    private float _yMaxColliderPoint;
    
    void Start()
    {

        jumpProgress = 0;
        canJump = true;

        _stateMachine = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _yMaxColliderPoint = GetComponent<SpriteRenderer>().bounds.max.y;
        
        _Effects.Add(new Effect());

    }
    
    void Update()
    {

        // Update the state machine with current detected attribute 
        if (groundSensor.isCollided)
        {
            _stateMachine.SetBool("IsGrounded", true);
        } else
            _stateMachine.SetBool("IsGrounded", false);

        if (lWallSensor.isCollided)
        {
            _stateMachine.SetTrigger("OnWall");
        } else if (rWallSensor.isCollided)
        {
            _stateMachine.SetTrigger("OnWall");
        }
        
        // Apply an effect on the IA


        for (int i = 0; i < _Effects.Count; i++)
        {
            _Effects[i].Update(this);
            if (_Effects[i].IsEnd)
            {
                _Effects[i].Reset();
                _Effects.Remove(_Effects[i]);
                i--;
            }
        }

        if (nearestSuicidePoint)
        {
            destination = nearestSuicidePoint.transform;
        } else if (!destination)
        {
            destination = levelEnd;
        }

        _direction = new Vector2(destination.position.x - transform.position.x, destination.position.y - transform.position.y);
        Vector3.Normalize(_direction);
        if (_direction.x < 0)
        {
            _direction.x = -1;
        } 
        if (_direction.x > 0)
        {
            _direction.x = 1;
        } 
        
        AnimatorStateInfo lifeStateMachine = _stateMachine.GetCurrentAnimatorStateInfo(1);
        if (lifeStateMachine.IsName("Dead"))
        {
            respawnProgress += Time.deltaTime;
            winText.text = "Gros noob";
            winText.enabled = true;
            _direction = new Vector2();
            if (respawnProgress > respawnTime)
            {
                winText.enabled = false;
                _stateMachine.SetTrigger("OnRevive");
                transform.position = levelStart.position;
            }
            return;
        }

        if (lWallSensor.isCollided && _movementX < 0)
        {
            float maxY = lWallSensor.GetMaximumPoint();
            Debug.Log(maxY);
            Debug.Log(_yMaxColliderPoint + transform.position.y + jumpForce/2);
            if (_yMaxColliderPoint + transform.position.y < maxY)
            {
                if (nearestSuicidePoint) 
                    Destroy(nearestSuicidePoint.gameObject);
                nearestSuicidePoint = null;
            }
            else if (canJump)
            {
                _stateMachine.SetBool("IsJumping", true);
            }
        }

        if (rWallSensor.isCollided && _movementX > 0)
        {
            float maxY = lWallSensor.GetMaximumPoint();
            if (_yMaxColliderPoint + transform.position.y < maxY)
            {
                if (nearestSuicidePoint) 
                    Destroy(nearestSuicidePoint.gameObject);
                nearestSuicidePoint = null;
            }
            else if (canJump)
            {
                _stateMachine.SetBool("IsJumping", true);
            }
        }

    }

    private void FixedUpdate()
    {
        AnimatorStateInfo movementStateMachine = _stateMachine.GetCurrentAnimatorStateInfo(0);

        // Use to accumulate all collected effects
        float jumpBoost = 1;
        float speedBoost = 1;
        float gravityScale = 1;
        int forceForward = 0;
        
        foreach (var effect in _Effects)
        {
            jumpBoost *= effect.jumpModifier;
            speedBoost *= effect.speedModifier;
            gravityScale *= effect.gravityScale;
            forceForward = effect.forceDirection;
        }

        if (forceForward != 0)
            _direction.x = forceForward;
        
        // Calculate IA movement
        _movementX = Time.fixedDeltaTime * _direction.x * speed * speedBoost;
        float jump = jumpForce * (1 - jumpProgress/jumpTime) * jumpBoost;
        
        _rigidbody.gravityScale = gravityScale;
        if (movementStateMachine.IsName("Walk"))
        {
            _rigidbody.velocity = new Vector2(_movementX, _rigidbody.velocity.y);
        } else if (movementStateMachine.IsName("Jump"))
        {
            jumpProgress += Time.deltaTime;
            canJump = false;
            if (jumpProgress >= jumpTime)
            {
                _stateMachine.SetBool("IsJumping", false);
                jumpProgress = 0;
                return;
            }

            _rigidbody.velocity = new Vector2(_movementX, jump);
        } else if (movementStateMachine.IsName("Fall"))
        {
            if (!hasStartedFall)
            {
                groundedOnBin = false;
                fallStartY = transform.position.y;
                hasStartedFall = true;
            }
            else
            {
                if (transform.position.y > fallStartY)
                    fallStartY = transform.position.y;
            }
        } else if (!movementStateMachine.IsName("Fall") && hasStartedFall)
        {
            float fallLenght = fallStartY - transform.position.y;
            if (fallLenght > 15)
            {
                Die();
                Debug.Log("Mry");
            }

            hasStartedFall = false;
            canJump = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Benne":
                groundedOnBin = true;
                break;
            case "Goal":
                winText.enabled = true;
                Time.timeScale = 0.1f;
                break;
            case "Death":
                respawnProgress = 0;
                Die();
                break;
            case "SuicidePoint":
                Debug.Log("Feur");
                nearestSuicidePoint = other.GetComponent<SuicidePoint>();
                break;
            case "Effect":
            case "Far":
                Effect atchEffect = other.GetComponent<PickupableEffect>().AttachedEffect;
                if (atchEffect.Use())
                {
                    Debug.Log("Pickuped");
                    _Effects.Add(atchEffect);
                }
                break;
                
        }
    }

    public void Die()
    {
        Debug.Log("MORT");
        _stateMachine.SetTrigger("OnDeath");
    }
}
