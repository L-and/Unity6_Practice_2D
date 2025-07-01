using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Platformer
{
    public class Goblin : MonsterCore
    {
        private float _timer;
        private float _idleTime, _patrolTime;

        private float _traceDist = 5f;
        private float _attackDist = 1.8f;

        private bool _isAttack;
        private void Start()
        {
            Init(10f, 3f, 2f);

            StartCoroutine(FindPlayerCoroutine());
        }

        protected override void Init(float hp, float speed, float attackCoolTime)
        {
            base.Init(hp, speed, attackCoolTime);
        }

        IEnumerator FindPlayerCoroutine()
        {
            yield return null;
        }
        
        public override void Idle()
        {
            _timer += Time.deltaTime;
            if (_timer >= _idleTime)
            {
                _timer = 0f;
                _moveDir = Random.Range(0, 2) == 1 ? 1 : -1;
                transform.localScale = new Vector3(_moveDir, 1, 1);
                _patrolTime = Random.Range(1f, 5f);
                _anim.SetBool("isRun", true);

                ChangeState(MonsterState.PATROL);
            }
            
            if (_targetDist <= _traceDist && _isTrace)
            {
                _timer = 0f;
                _anim.SetBool("isRun", true);
                ChangeState(MonsterState.TRACE);
            }
        }

        public override void Patrol()
        {
            transform.position += Vector3.right * _moveDir * speed * Time.deltaTime;

            _timer += Time.deltaTime;
            if (_timer >= _patrolTime)
            {
                _timer = 0f;
                _idleTime = Random.Range(1f, 5f);
                _anim.SetBool("isRun", false);
                
                ChangeState(MonsterState.IDLE);
            }
            
            if (_targetDist <= _traceDist)
            {
                _timer = 0f;
                ChangeState(MonsterState.TRACE);
            }
        }

        public override void Trace()
        {
            var targetDir = (target.position - transform.position).normalized;
            transform.position += Vector3.right * targetDir.x * speed  * Time.deltaTime;

            var scaleX = targetDir.x > 0 ? 1 : -1;
            transform.localScale = new Vector3(scaleX, 1, 1);
            if (_targetDist > _traceDist)
            {
                _anim.SetBool("isRun", false);
                ChangeState(MonsterState.IDLE);
            }

            if (_targetDist < _attackDist)
            {
                ChangeState(MonsterState.ATTACK);
            }
        }

        public override void Attack()
        {
            if (!_isAttack)
                StartCoroutine(AttackCoroutine());
        }

        IEnumerator AttackCoroutine()
        {
            _isAttack = true;
            _anim.SetTrigger("Attack");
            yield return new WaitForSeconds(1f);
            _anim.SetBool("isRun", false);
            
            yield return new WaitForSeconds(attackCoolTime - 1f);
            _isAttack = false;
            ChangeState(MonsterState.IDLE);
        }
    }
}

