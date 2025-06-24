using UnityEngine;

enum PlayerControllerState
{
    IDLE,
    MOVE,
    JUMP
}
public class OldPlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private float _inputH;
    private bool _inputJump;
    public bool isGrounded;
    
    private Rigidbody2D _rb;
    private SpriteRenderer _renderer;
    private Animator _anim;

    private PlayerControllerState _state;

    static StateMachine StateMachine;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _anim =  GetComponent<Animator>();
    }
    
    void Update()
    {
        GetInput();

        switch (_state)
        {
            case PlayerControllerState.IDLE:
                Idle();
                break;
            case PlayerControllerState.MOVE:
                Move();
                break;
            case PlayerControllerState.JUMP:
                Jump();
                break;
        }
    }

    private void GetInput()
    {
        _inputH = Input.GetAxis("Horizontal");
        _inputJump = Input.GetKeyDown(KeyCode.Space);
        
        if (_inputH != 0f) 
            _state = PlayerControllerState.MOVE;
        
        if (_inputJump)
            _state = PlayerControllerState.JUMP;
    }
    
    private void Idle()
    {
        
    }

    private void Move()
    {
        if (_inputH != 0f)
        {
            // 애니메이션 설정
            _anim.SetBool("isRun", true);
            // 스프라이트 플립
            var scaleX = _inputH > 0 ? 1 : -1;
            transform.localScale = new Vector3(scaleX, 1, 1);
        }
        else
        {
            _anim.SetBool("isRun", false);
        }
    }

    private void Jump()
    {
        if (!isGrounded) return;
        
        if (_inputJump)
            _rb.AddForceY(jumpForce, ForceMode2D.Impulse);
    }
    
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ground 상태
        if (other.gameObject.CompareTag("Ground") &&
            !isGrounded)
        {
            _anim.SetBool("isGround", true);
            isGrounded = _anim.GetBool("isGround");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Jump 상태
        if (other.gameObject.CompareTag("Ground") &&
            isGrounded)
        {
            _anim.SetBool("isGround", false);
            isGrounded = _anim.GetBool("isGround");
        }
    }

    
}
