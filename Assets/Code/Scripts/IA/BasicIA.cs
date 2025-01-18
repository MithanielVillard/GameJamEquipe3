using System;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

public class BasicIA : MonoBehaviour
{

    [Header("Movements Properties")] 
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTime;
    [SerializeField] private float jumpProgress;
    [SerializeField] private float fallStartY;
    [SerializeField] private bool hasStartedFall;
    
    [Header("States Manager")]
    [SerializeField] private Sensor groundSensor;
    [SerializeField] private Sensor lWallSensor;
    [SerializeField] private Sensor rWallSensor;
    
    [Header("Points of Interest")]
    public Transform destination;
    [SerializeField] private Transform start;
    [SerializeField] private TextMeshProUGUI winText;

    [SerializeField] private Effect currentEffect = new();
    private Animator _stateMachine;
    private Rigidbody2D _rigidbody;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _movementX;

    [SerializeField] private float respawnTime;
    [SerializeField] private float respawnProgress;
    
    void Start()
    {

        jumpProgress = 0;

        _stateMachine = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

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
        currentEffect.Update(this);
        if (currentEffect.IsEnd)
        {
            currentEffect.Reset();
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
            if (respawnProgress > respawnTime)
            {
                winText.enabled = false;
                _stateMachine.SetTrigger("OnRevive");
                transform.position = start.position;
            }
        }
        
        if (lWallSensor.isCollided && _movementX < 0)
            _stateMachine.SetBool("IsJumping", true);
        if (rWallSensor.isCollided && _movementX > 0)
            _stateMachine.SetBool("IsJumping", true);

    }

    private void FixedUpdate()
    {
        
        // Make action based on state machine current state
        AnimatorStateInfo movementStateMachine = _stateMachine.GetCurrentAnimatorStateInfo(0);
        _movementX = Time.fixedDeltaTime * _direction.x * speed * currentEffect.speedModifier * Convert.ToInt32(!currentEffect.lockWalking);
        float jump = jumpForce * (1 - jumpProgress/jumpTime) * currentEffect.jumpModifier;
        
        if (movementStateMachine.IsName("Walk"))
        {
            _rigidbody.velocity = new Vector2(_movementX, _rigidbody.velocity.y);
        } else if (movementStateMachine.IsName("Jump"))
        {
            jumpProgress += Time.deltaTime;
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
                fallStartY = transform.position.y;
                hasStartedFall = true;
            }
        } else if (!movementStateMachine.IsName("Fall") && hasStartedFall)
        {
            float fallLenght = fallStartY - transform.position.y;
            if (fallLenght > 10)
                Debug.Log("Degat de chute pour " + fallLenght);
            hasStartedFall = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Goal":
                winText.enabled = true;
                Time.timeScale = 0.1f;
                break;
            case "Death":
                _stateMachine.SetTrigger("OnDeath");
                break;
            case "Effect":
            case "Far":
                Debug.Log("Pickuped");
                currentEffect = other.GetComponent<PickupableEffect>().AttachedEffect;
                break;
                
        }
    }
}
