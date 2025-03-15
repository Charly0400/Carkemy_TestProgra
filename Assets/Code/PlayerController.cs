using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;
using System.Xml.Serialization;

#region Enums
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

#endregion

//Sistema general del movimiento del personaje
public class PlayerController : MonoBehaviour
{
    #region Variables

    [Header("STATES")]
    [SerializeField] StatesPlayer _statesPlayer;

    [Header("REFERENCES")]
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected PlayerInput _playerInput;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected Inventory _inventory;
    [SerializeField] protected GameManager _gameManager;
    public Store store;

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
    [SerializeField] protected bool _isShieldActive;

    [Header("ATTACK")]
    [SerializeField] protected GameObject _boxAttack;
    [SerializeField] protected bool isBoxActive;

    [Header("FLIP")]
    [SerializeField] protected bool _isFacingRight = true;
    #endregion

    #region Unity Methods

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _inventory = GetComponent<Inventory>();
        _animator = GetComponent<Animator>();
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

    #region Public Methods Input System

    public void Move(InputAction.CallbackContext callback)
    {
        input = callback.ReadValue<Vector2>().x;
    }

    //Manejo gradual y completo del salto con animación
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
        if (callback.performed)
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
    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && _statesPlayer != StatesPlayer.ATTACK)
        {
            SetStateForPlayer(StatesPlayer.ATTACK);
        }
    }

    #endregion

    #region Private Methods

    //Actualiza el estado del jugador
    protected void UpdateStateOfPlayer()
    {

        if (_statesPlayer == StatesPlayer.SHIELD ||
            _statesPlayer == StatesPlayer.DASH ||
            _statesPlayer == StatesPlayer.ATTACK ||
            _statesPlayer == StatesPlayer.DEATH)
        {
            return;
        }

        if (isGrounded)
        {
            SetStateForPlayer(input == 0 ? StatesPlayer.IDLE : StatesPlayer.WALK);
        }
        else
            SetStateForPlayer(StatesPlayer.JUMP);

    }

    //Establece la lógica del jugador
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

    #region PLayer Moevement Methods

    //Activa y desactiva eñ escudo
    private void ToggleShield()
    {
        _isShieldActive = !_isShieldActive;
        shield.SetActive(_isShieldActive);
    }
    //Activa y desactiva eñ escudo
    private void ToggleAttackBox()
    {
        isBoxActive = !isBoxActive;
        _boxAttack.SetActive(isBoxActive);
    }

    //Voltea el sprite del personajes
    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    //Verifica si está saltando
    private void GroundCheck()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
    }
    private IEnumerator EndAttack()
    {
        ToggleAttackBox();

        yield return new WaitForSeconds(.5f);

        ToggleAttackBox();

        SetStateForPlayer(isGrounded ? (input == 0 ? StatesPlayer.IDLE : StatesPlayer.WALK) : StatesPlayer.JUMP);
    }

    //Corrutina para manejar el dash
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

    #endregion

    #region states Methods

    private void IdleState()
    {
        _animator.Play("Idle_Original");
    }

    private void WalkState()
    {
        _animator.PlayInFixedTime("walk_original", 0);
    }

    private void JumpState()
    {
        float time = Map(_rb.linearVelocityY, jumpForce, -jumpForce, 0, 1, true);
        _animator.speed = 0;
        _animator.Play("Jump_Original", 0, time);
    }

    private void DashState()
    {
        StartCoroutine(Dash());
    }

    private void ShieldState()
    {
        ToggleShield();
        if (!_isShieldActive)
        {
            SetStateForPlayer(isGrounded ? (input == 0 ? StatesPlayer.IDLE : StatesPlayer.WALK) : StatesPlayer.JUMP);
        }
        Debug.Log(_statesPlayer.ToString());
    }

    private void AttackState()
    {
        _animator.Play("Origin_Attack");
        StartCoroutine(EndAttack());
    }

    private void DeathState()
    {
    }

    #endregion

    //Metodo para mapear el valor de un rango a otro
    public static float Map(float value, float fromSource, float toSource, float fromTarget, float toTarget, bool clamp = false)
    {
        float t = (value - fromSource) / (toSource - fromSource);
        if (clamp)
            t = Mathf.Clamp01(t);
        return Mathf.Lerp(fromTarget, toTarget, t);
    }

    #endregion

}
