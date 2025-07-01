using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KnightController : MonoBehaviour
{
    [SerializeField] private KeyboardInput keyboardInput;
    [SerializeField] private JoystickInput joystickInput;
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private Vector2 _inputMoveDir;
    private bool _inputJump;
    private bool _inputAttack;
    private bool _inputComboAttack;

    private bool _isGround;
    private bool _isLadder;
    
    private Rigidbody2D _rb;
    private Animator _anim;
    private Collider2D _collider;

    [SerializeField] private Button jumpButton;
    [SerializeField] private Button attackButtom;

    [SerializeField, ReadOnly]private KnightControllerState state;

    private float[] originalCollSize = new float[2];
    public float crouchColliderOffset;
    public float crouchColliderSize;

    private float _prevDirX;

    private Coroutine _attackCoroutine;
    private Coroutine _comboCoroutine;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim =  GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        
        jumpButton.onClick.AddListener(Jump);
        attackButtom.onClick.AddListener(Attack);

        originalCollSize[0] = _collider.offset.y;
        originalCollSize[1] = _collider.bounds.size.y;
    }
    
    void Update()
    {
        InputUpdate();
        // StateUpdate();
        AnimationUpdate();
        
        switch (state)
        {
            case KnightControllerState.IDLE:
                Idle();
                break;
            case KnightControllerState.MOVE:
                Move();
                break;
            case KnightControllerState.JUMP:
                Jump();
                break;
            case KnightControllerState.ATTACK:
                Attack();
                break;
            case KnightControllerState.COMBO:
                Combo();
                break;
        }
    }


    private void AnimationUpdate()
    {
        _anim.SetFloat("moveX", _inputMoveDir.x);
        _anim.SetFloat("moveY", _inputMoveDir.y);
        _anim.SetFloat("velocityY", _rb.linearVelocityY);
    }

    public void InputUpdate()
    {
        // 이동입력
        if (joystickInput.moveDir != Vector2.zero || keyboardInput.moveDir != Vector2.zero)
        {
            _inputMoveDir = (joystickInput.moveDir + keyboardInput.moveDir).normalized;
        }
        else
        {
            _inputMoveDir = Vector2.zero;
        }
        
        _inputJump = keyboardInput.jump;
        
        if (_attackCoroutine != null)
        {
            _inputComboAttack = keyboardInput.attack;
        }
        else
        {
            _inputAttack = keyboardInput.attack;
        }

    }
    
    private void Idle()
    {
        if (_inputMoveDir != Vector2.zero)
        {
            state = KnightControllerState.MOVE;
        }
        
        if (_inputJump && _isGround)
            state = KnightControllerState.JUMP;
        
        if (_inputAttack)
            state = KnightControllerState.ATTACK;
    }

    private void Move()
    {
        if (_inputMoveDir == Vector2.zero)
        {
            state = KnightControllerState.IDLE;
        }
        
        if (_inputJump && _isGround)
            state = KnightControllerState.JUMP;
        
        if (_inputAttack)
            state = KnightControllerState.ATTACK;
        
        // 스프라이트 플립
        
        var scaleX = _prevDirX > 0 ? 1 : -1;
        transform.localScale = new Vector3(scaleX, 1, 1);
        _prevDirX = _inputMoveDir.x;

        // 앉기상태 콜라이더 크기변경
        // if (_inputMoveDir.y < 0)
        // {
        //     _collider.offset = new Vector2(_collider.offset.x, crouchColliderOffset);
        //     var colliderBounds = _collider.bounds;
        //     _collider = new Vector2(colliderBounds.size.x, crouchColliderSize);
        // }
        // else
        // {
        //     _collider.offset = new Vector2(_collider.offset.x, originalCollSize[0]);
        //     var colliderBounds = _collider.bounds;
        //     colliderBounds.size = new Vector2(colliderBounds.size.x, originalCollSize[1]);
        // }

        if (!_isLadder)
        {
            _rb.linearVelocityX = _inputMoveDir.x * speed;
        }
        
        if (_isLadder && _inputMoveDir.y != 0)
        {
            _rb.linearVelocityY = _inputMoveDir.y * speed;
        }
    }

    private void Jump()
    {
        if (_isLadder) // 사다리점프
        {
            _anim.SetTrigger("Jump");
            _rb.AddForce(new Vector2(transform.localScale.x, 1f).normalized * jumpForce, ForceMode2D.Impulse);
        }
        else // 일반점프
        {
            _anim.SetTrigger("Jump");
            _rb.AddForceY(jumpForce, ForceMode2D.Impulse);
        }
        if (_isGround)
            state = KnightControllerState.IDLE;
    }

    private void Attack()
    {
        Debug.Log(_attackCoroutine == null);
        // 공격or콤보공격중이 아닐때 실행
        if (_attackCoroutine == null)
        {
            _attackCoroutine = StartCoroutine(CoAttack());
        }

        if (_inputComboAttack)
        {
            _anim.SetBool("isCombo", true);
            state = KnightControllerState.COMBO;
        }
    }

    IEnumerator CoAttack()
    {
        _anim.SetTrigger("Attack");

        while (true)
        {
            yield return null;

            if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
                break;
        }

        Debug.Log("공격종료");
        _attackCoroutine = null;
        
        if (!_anim.GetBool("isCombo"))
            state = KnightControllerState.IDLE;
    }
    
    private void Combo()
    {
        if (_comboCoroutine == null)
        {
            _comboCoroutine = StartCoroutine(CoCombo());
        }
    }

    IEnumerator CoCombo()
    {
        Debug.Log("콤보시작");
        
        while (true)
        {
            yield return null;
            if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
                break;
        }
        Debug.Log("콤보종료");
        _anim.SetBool("isCombo", false);
        _comboCoroutine = null;
        state = KnightControllerState.IDLE;
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _anim.SetBool("isGround", true);
            _isGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _anim.SetBool("isGround", false);
            _isGround = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            _isLadder = true;
            _rb.gravityScale = 0f;
            _rb.linearVelocity = Vector2.zero;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            _isLadder = false;
            _rb.gravityScale = 2f;
        }
    }
}
