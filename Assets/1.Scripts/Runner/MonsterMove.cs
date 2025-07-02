using System;
using Runner;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterMove : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody2D _rb;
    private BoxCollider2D _col;

    public int score = 5;
    public float dieMotionPower = 20f;
    void Start()
    {
        _col = gameObject.GetComponent<BoxCollider2D>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }
    

    private void Update()
    {
        gameObject.transform.transform.Translate(Vector3.left * (GameManager.Instance.gameSpeed * Time.deltaTime));
    }
    
    public void Die()
    {
        GameManager.Instance.score += score;
        // 사망애니메이션 재생 & 튀는효과
        _anim.SetTrigger("Die");
        _rb.AddForceY(dieMotionPower,  ForceMode2D.Impulse);
        _rb.AddForceX(Random.Range(-1f, 1f),  ForceMode2D.Impulse);
        _col.enabled = false;
    }

}
 