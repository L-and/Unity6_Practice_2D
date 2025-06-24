using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float jumpForce;

    #region 점프관련
    public float groundDistance = 0.05f;
    private BoxCollider2D _touchingCol;
    [ReadOnly] public bool isGrounded;
    
    public RaycastHit2D[] groundHits = new RaycastHit2D[5];
    #endregion
    
    #region 입력관련
    [ReadOnly] public float inputH;
    public bool inputJump;
    #endregion

    [SerializeField] private GameObject hitBox;
    private bool _isAttack = false;
    #region 컴포넌트
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    #endregion

    
    
    [SerializeField]
    public StateMachine playerStateMachine;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim =  GetComponent<Animator>();
        _touchingCol = GetComponent<BoxCollider2D>();
        
        // Rigidbody 설정
        rb.freezeRotation = true;
        
        // State 패턴 설정
        playerStateMachine = new StateMachine(this);
        playerStateMachine.Initialize(playerStateMachine.idleState);
    }
    
    void Update()
    {
        input();

        attack();
    }
    
    private void LateUpdate()
    {
        playerStateMachine.LateUpdate();
    }

    void attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !_isAttack)
        {
            StartCoroutine(AttackRoutine());
        }
    }
    
    

    private void input()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        inputJump = Input.GetKeyDown(KeyCode.Space);
    }

    void FixedUpdate()
    {
        playerStateMachine.FixedUpdate();
    }

    // Ray를 사용한 Ground Check
    public void GroundCheck()
    {
        isGrounded = _touchingCol.Cast(Vector2.down, groundHits, groundDistance) > 0;
    }

    IEnumerator AttackRoutine()
    {
        _isAttack = true;
        hitBox.SetActive(true);
        
        yield return new WaitForSeconds(0.25f);
        hitBox.SetActive(false);
        _isAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{other.name}");
        other.TryGetComponent<Monster>(out var monster);
        
        monster?.HitTest();
        
    }
}
