using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    #region Variables

    [Header ("REFERENCES")]
    [SerializeField] protected Rigidbody2D _rb; 
    [SerializeField] protected PlayerInput _playerInput; 

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
    }

    private void Update()
    {
        if (_canDash)
        {
            _rb.linearVelocity = new Vector2(input * _speed, _rb.linearVelocity.y);
        }

        if (!_isFacingRight && input > 0f)
        {
            Flip();
        }
        else if (_isFacingRight && input < 0f)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
    }

    #endregion

    #region Public Methods

    public void Jump(InputAction.CallbackContext callback)
    {
        if (callback.performed && !_isDashing && isGrounded)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocityX, jumpForce);
        }

        if (callback.canceled  && _rb.linearVelocityY > 0f)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocityX, _rb.linearVelocityY * .5f);
        }
    }

    public void Move(InputAction.CallbackContext callback)
    {
        input = callback.ReadValue<Vector2>().x;
    }

    public void Shield (InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            ToggleShield();
        }
    }

    public void Dash(InputAction.CallbackContext callback)
    {
        if (callback.performed && _canDash)
        {            
            StartCoroutine(Dash());
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

    #endregion

}
