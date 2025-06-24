using System;
using System.Collections;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    
    [SerializeField] protected float hp = 3f;
    [SerializeField] protected float moveSpeed = 3f;

    private float _dir = 1f;
    private bool _isMove = true;
    private bool _isHit = false;

    public abstract void Init();
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        
        Init();
    }

    void Update()
    {
        Move();
        MouseClick();
    }

    private void MouseClick()
    {
        if (Input.GetMouseButton(0))
            StartCoroutine(Hit(1f));
    }

    public void HitTest()
    {
        StartCoroutine(Hit(1f));
        
    }

    private void Move()
    {
        if (!_isMove)
            return;
        
        transform.position += Vector3.right * _dir * moveSpeed * Time.deltaTime;
    

        if (transform.position.x > 8f)
        {
            _dir = -1f;
            _spriteRenderer.flipX = true;
        }
        else if (transform.position.x < -8f)
        {
            _dir = 1f;
            _spriteRenderer.flipX = false;
        }
        
    }

    public IEnumerator Hit(float damage)
    {
        if (_isHit)
            yield break;
        
        _isHit = true;
        _isMove = false;
        _animator.SetTrigger("Hit");
        
        if (hp > 0f)
            hp -= damage;
        else
        {
            Debug.Log($"{name} 처치");
        }

        yield return new WaitForSeconds(0.5f);

        _isMove = true;
        _isHit = false;
    }
}
