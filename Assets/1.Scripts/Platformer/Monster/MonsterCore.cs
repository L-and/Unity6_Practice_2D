using System;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public abstract class MonsterCore : MonoBehaviour, IDamageable
    {
        public enum MonsterState {IDLE, PATROL, TRACE, ATTACK}
        public MonsterState monsterState = MonsterState.IDLE;

        public ItemManager itemManager;

        public Transform target;
        protected Animator _anim;
        protected Rigidbody2D _monsterRb;
        protected Collider2D _monsterColl;
        public Image hpBar;
        
        public float hp;
        public float currHp;
        public float speed;
        public float attackCoolTime;
        public float attackDamage;
        
        protected float _moveDir;
        protected float _targetDist;

        protected bool _isTrace;
        private bool _isDead;
        protected virtual void Init(float hp, float speed, float attackCoolTime, float attackDamage)
        {
            this.hp = hp;
            this.speed = speed;
            this.attackCoolTime = attackCoolTime;
            this.attackDamage = attackDamage;

            itemManager = FindFirstObjectByType<ItemManager>();
            
            target = GameObject.FindGameObjectWithTag("Player").transform;
            
            _anim = GetComponent<Animator>();
            _monsterRb = GetComponent<Rigidbody2D>();
            _monsterColl = GetComponent<Collider2D>();

            currHp = hp;
            hpBar.fillAmount = currHp / hp;
        }
        
        private void Update()
        {
            if (_isDead)
                return;
            
            switch (monsterState)
            {
                case MonsterState.IDLE:
                    Idle();
                    break;
                case MonsterState.PATROL:
                    Patrol();
                    break;
                case MonsterState.TRACE:
                    Trace();
                    break;
                case MonsterState.ATTACK:
                    Attack();
                    break;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"고블린 공격");
            if (other.CompareTag("Return"))
            {
                _moveDir *= -1;
                transform.localScale = new Vector3(_moveDir, 1, 1);
            }
            
            if (other.GetComponent<IDamageable>() != null)
            {
                other.GetComponent<IDamageable>().TakeDamage(attackDamage);
            }
        }
        
        public abstract void Idle();
        public abstract void Patrol();
        public abstract void Trace();
        public abstract void Attack();

        public void ChangeState(MonsterState newState)
        {
            if (newState != monsterState)
                monsterState = newState;
        }
        
        public void TakeDamage(float damage)
        {
            currHp -= damage;
            hpBar.fillAmount = currHp / hp; // 현재체력 / 최대체력
            if (currHp <= 0f)
                Death();
        }

        public void Death()
        {
            _isDead = true;
            _anim.SetTrigger("Death");
            _monsterColl.enabled = false;
            _monsterRb.gravityScale = 0f;
        
            itemManager.DropItem(transform.position);
        }
    }
}

