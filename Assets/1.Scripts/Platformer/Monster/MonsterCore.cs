using System;
using UnityEngine;

namespace Platformer
{
    public abstract class MonsterCore : MonoBehaviour
    {
        public enum MonsterState {IDLE, PATROL, TRACE, ATTACK}
        public MonsterState monsterState = MonsterState.IDLE;
        
        protected Animator _anim;
        protected Rigidbody2D _monsterRb;
        protected Collider2D _monsterColl;

        public Transform target;
        
        public float hp;
        public float speed;
        public float attackCoolTime;
        
        protected float _moveDir;
        protected float _targetDist;

        protected bool _isTrace;
        protected virtual void Init(float hp, float speed, float attackCoolTime)
        {
            this.hp = hp;
            this.speed = speed;
            this.attackCoolTime = attackCoolTime;
            
            target = GameObject.FindGameObjectWithTag("Player").transform;
            
            _anim = GetComponent<Animator>();
            _monsterRb = GetComponent<Rigidbody2D>();
            _monsterColl = GetComponent<Collider2D>();
        }
        
        private void Update()
        {
            _targetDist = Vector3.Distance(transform.position, target.position);
            Vector3 monsterDir = Vector3.right * _moveDir;
            Vector3 playerDir = (transform.position - target.position).normalized;
            float dotValue = Vector3.Dot(monsterDir, playerDir);
            
            _isTrace = dotValue > 0;
            
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
            if (other.CompareTag("Return"))
            {
                _moveDir *= -1;
                transform.localScale = new Vector3(_moveDir, 1, 1);
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
    }
}

