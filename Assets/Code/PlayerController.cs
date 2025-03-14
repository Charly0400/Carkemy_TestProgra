using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;
using System.Xml.Serialization;


public enum StatesPlayer
{
    JUMP,
    WALK,
    IDLE,
    SHIELD,
    DASH,
    ATTACK,
    DEATH
}

public class PlayerController : MonoBehaviour
{
    #region Variables

    [Header("STATES")]
    [SerializeField] StatesPlayer _statesPlayer;
    //[SerializeField] protected bool _isStateComplete;


    [Header("REFERENCES")]
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected PlayerInput _playerInput;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected Inventory _inventory;
    [SerializeField] protected GameManager _gameManager;
    [SerializeField] protected Item_ScriptableObject _item_SO;

    [Header("MOVEMENT")]
    [SerializeField] protected float _speed = 5f;
    [SerializeField] protected float jumpForce = 10f;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected bool isGrounded;
    [SerializeField] protected float input;

    [Header("DASH")]
    [SerializeField] protected float _dashPower = 15f;
    [SerializeField] protected float _dashDuration = 0.2f;
    [SerializeField] protected bool _isDashing;
    [SerializeField] protected bool _canDash = true;

    [Header("SHIELD")]
    [SerializeField] protected GameObject shield;
    [SerializeField] protected bool shieldActive;

    [Header("FLIP")]
    [SerializeField] protected bool _isFacingRight = true;
    #endregion

    #region Unity Methods

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        UpdateStateOfPlayer();

        if (_canDash)
        {
            _rb.linearVelocity = new Vector2(input * _speed, _rb.linearVelocity.y);
        }

        if (!_isFacingRight && input > 0f || _isFacingRight && input < 0f)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
       GroundCheck();
    }

    #endregion

    #region Public Methods

        #region Player Input Systen

    public void Move(InputAction.CallbackContext callback)
    {
        input = callback.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext callback)
    {

        if (callback.performed && !_isDashing && isGrounded)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocityX, jumpForce);
        }

        if (callback.canceled && _rb.linearVelocityY > 0f)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocityX, _rb.linearVelocityY * .5f);
        }
    }

    public void Shield(InputAction.CallbackContext callback)
    {
        if (callback.performed )
        {
            SetStateForPlayer(StatesPlayer.SHIELD);
        }
    }

    public void Dash(InputAction.CallbackContext callback)
    {
        if (callback.performed && _canDash)
        {
            SetStateForPlayer(StatesPlayer.DASH);
        }
    }

    public void Inventory(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            _gameManager.ToggleInventoryPanel();
            _inventory.ListItems();
        }
    }

    #endregion

    protected void UpdateStateOfPlayer()
    {

        if (_statesPlayer == StatesPlayer.SHIELD ||
            _statesPlayer == StatesPlayer.DASH ||
            _statesPlayer == StatesPlayer.ATTACK ||
            _statesPlayer == StatesPlayer.DEATH)
        {
            return;
        }

        //_isStateComplete = false;

        if (isGrounded)
        {
            SetStateForPlayer(input == 0 ? StatesPlayer.IDLE : StatesPlayer.WALK);
        }
        else
            SetStateForPlayer(StatesPlayer.JUMP);
     
    }

    protected void SetStateForPlayer(StatesPlayer statesPlayer)
    {
        _statesPlayer = statesPlayer;

        switch (_statesPlayer)
        {
            case StatesPlayer.IDLE:
                IdleState();
                break;
            case StatesPlayer.WALK:
                WalkState();
                break;
            case StatesPlayer.JUMP:
                JumpState();
                break;
            case StatesPlayer.SHIELD:
                ShieldState();
                break;
            case StatesPlayer.DASH:
                DashState();
                break;
            case StatesPlayer.ATTACK:
                AttackState();
                break;
            case StatesPlayer.DEATH:
                DeathState();
                break;

        }
    } 

    #endregion

    #region Private Methods

    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;
        float originGravity = _rb.gravityScale;
        _rb.gravityScale = 0f;
        _rb.linearVelocity = new Vector2(transform.localScale.x * _dashPower, 0f);
        yield return new WaitForSeconds(_dashDuration);
        _rb.gravityScale = originGravity;
        _isDashing = false;
        _canDash = true;
        SetStateForPlayer(isGrounded ? (input == 0 ? StatesPlayer.IDLE : StatesPlayer.WALK) : StatesPlayer.JUMP);
    }

    private void ToggleShield()
    {
        shieldActive = !shieldActive;
        shield.SetActive(shieldActive);
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void GroundCheck()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
    }

        #region states Methods

    private void IdleState()
    {
        Debug.Log(_statesPlayer.ToString());
    }

    private void WalkState()
    {
        //float velX = _rb.linearVelocityX;
        //_animator.speed = Mathf.Abs(input) < .1f / _speed;
        Debug.Log(_statesPlayer.ToString());
    }

    private void JumpState()
    {
        //float time = Map(_rb.linearVelocityY, jumpForce, -jumpForce, 0, 1, true);
        //_animator.Play("Jump", 0, time);
        //_animator.speed = 0;
        Debug.Log(_statesPlayer.ToString());
    }
    
    private void DashState()
    {
        StartCoroutine(Dash());
        Debug.Log(_statesPlayer.ToString());
    }

    private void ShieldState()
    {
        ToggleShield();
        if (!shieldActive)
        {
            SetStateForPlayer(isGrounded ? (input == 0 ? StatesPlayer.IDLE : StatesPlayer.WALK) : StatesPlayer.JUMP);
        }
        Debug.Log(_statesPlayer.ToString());
    }

    private void AttackState()
    {
        Debug.Log(_statesPlayer.ToString());
    }

    private void DeathState()
    {
        Debug.Log(_statesPlayer.ToString());
    }
    
        #endregion 

    #endregion

}
