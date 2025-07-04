using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KnightController_Topdown : MonoBehaviour
{
    [SerializeField] private KeyboardInput keyboardInput;
    [SerializeField] private JoystickInput joystickInput;
    
    [SerializeField] private float speed;

    private Vector2 _inputMoveDir;
    private bool _inputAttack;

    private bool _isGround;
    
    private Rigidbody2D _rb;
    private Animator _anim;

    [SerializeField, ReadOnly]private KnightControllerState state;

    public bool _isAttack;
    public bool _isCombo;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim =  GetComponent<Animator>();
    }
    
    void Update()
    {
        InputUpdate();
        StateUpdate();
        AnimationUpdate();
        
        switch (state)
        {
            case KnightControllerState.IDLE:
                Idle();
                break;
            case KnightControllerState.MOVE:
                Move();
                break;
            case KnightControllerState.ATTACK:
                Attack();
                break;
        }
    }


    private void AnimationUpdate()
    {
        _anim.SetFloat("moveX", _inputMoveDir.x);
        _anim.SetFloat("moveY", _inputMoveDir.y);
        _anim.SetFloat("velocityY", _rb.linearVelocityY);
    }
    
    private void StateUpdate()
    {
        if (_inputMoveDir == Vector2.zero)
        {
            state = KnightControllerState.IDLE;
        }
        else if (_inputMoveDir != Vector2.zero)
        {
            state = KnightControllerState.MOVE;
        }
        
     
        if (_inputAttack)
            state = KnightControllerState.ATTACK;
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
        
        _inputAttack = keyboardInput.attack;

    }
    
    private void Idle()
    {
        
    }

    private void Move()
    {
        if (_inputMoveDir == Vector2.zero) return;
        
        // 스프라이트 플립
        var scaleX = _inputMoveDir.x > 0 ? 1 : -1;
        transform.localScale = new Vector3(scaleX, 1, 1);
            
        // _rb.linearVelocityX = _inputMoveDir.x * speed;
        _rb.linearVelocity = _inputMoveDir * speed;
    }

    private void Jump()
    {
        if (!_isGround) return;
        
        _anim.SetTrigger("Jump");
    }

    private void Attack()
    {
        if (!_isAttack && !_isCombo)
        {
            Debug.Log("공격입력");
            _isAttack = true;
            _anim.SetTrigger("Attack");
        }
        else
        {
            Debug.Log("콤보입력");
            Combo();
        }
    }

    private void Combo()
    {
        if (!_isCombo)
        {
            _isCombo = true;
            _anim.SetBool("isCombo", true);
        }
    }
    
    public void AttackDone()
    {
        Debug.Log("공격 종료");
        _isAttack = false;
        _isCombo = false;
    }

    public void CrouchAttackDone()
    {
        Debug.Log("앉기공격 종료");
        _isAttack = false;
        _isCombo = false;
        _anim.SetBool("isCombo", false);
    }

    public void ComboDone()
    {
        Debug.Log("콤보 종료");

        _isAttack = false;
        _isCombo = false;
        _anim.SetBool("isCombo", false);
    }
}
