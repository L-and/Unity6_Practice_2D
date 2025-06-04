using Unity.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float jumpForce;

    // 점프관련
    public float groundDistance = 0.05f;
    private CapsuleCollider2D _touchingCol;
    [ReadOnly] public bool isGrounded;
    public RaycastHit2D[] groundHits = new RaycastHit2D[5];
    #region 입력관련
    [ReadOnly] public float inputH;
    [ReadOnly] public bool inputJump;
    #endregion
    
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
        _touchingCol = GetComponent<CapsuleCollider2D>();
        
        // Rigidbody 설정
        rb.freezeRotation = true;
        
        // State 패턴 설정
        playerStateMachine = new StateMachine(this);
        playerStateMachine.Initialize(playerStateMachine.idleState);
    }
    
    void Update()
    {
        inputJump = Input.GetKeyDown(KeyCode.Space);
        
        // Ray를 사용한 Ground Check
        isGrounded = _touchingCol.Cast(Vector2.down, groundHits, groundDistance) > 0;
        
        playerStateMachine.Update();
    }

    void FixedUpdate()
    {
        inputH = Input.GetAxisRaw("Horizontal");

        playerStateMachine.FixedUpdate();
    }
}
