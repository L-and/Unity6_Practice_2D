using System;
using UnityEngine;

namespace Runner
{
    public class SamuraiController : MonoBehaviour
    {
        private Animator _anim;
        private Rigidbody2D _rb;

        private bool _isGround = true;
        
        [SerializeField] private float jumpForce = 3f;
        
        private BoxCollider2D _col;
        
        private void Start()
        {
            _col = gameObject.GetComponent<BoxCollider2D>();
            _anim = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
        }

        public void GameStart()
        {
            _anim.SetTrigger("Run");
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && _isGround)
            {
                _isGround = false;
                _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _anim.SetTrigger("Attack");
            }
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Monster"))
            {
                other.gameObject.GetComponent<MonsterMove>().Die();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                _isGround = true;
            }
            
            if (other.gameObject.CompareTag("Monster"))
            {
                _anim.SetTrigger("Die");
                _rb.AddForceY(20,  ForceMode2D.Impulse);
                _col.enabled = false;
                GameManager.Instance.GameEnd();
            }
        }
    }
}

