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
        private float _attackDist = 1.5f;

        private bool _isAttack;
        private void Start()
        {
            Init(30f, 3f, 2f, 10f);

            StartCoroutine(FindPlayerCoroutine());
        }

        protected override void Init(float hp, float speed, float attackCoolTime, float attackDamage)
        {
            base.Init(hp, speed, attackCoolTime, attackDamage);
            
            _idleTime = Random.Range(1f, 5f);
        }

        IEnumerator FindPlayerCoroutine()
        {
            while (true)
            {
                yield return null;
                _targetDist = Vector3.Distance(transform.position, target.position);

                if (monsterState == MonsterState.IDLE || monsterState == MonsterState.PATROL)
                {
                    Vector3 monsterDir = Vector3.right * _moveDir;
                    Vector3 playerDir = (transform.position - target.position).normalized;
                    float dotValue = Vector3.Dot(monsterDir, playerDir);
                    _isTrace = dotValue < -0.5f && dotValue >= -1f;

                    if (_targetDist <= _traceDist && _isTrace)
                    {
                        _anim.SetBool("isRun", true);

                        ChangeState(MonsterState.TRACE);
                    }
                }
                else if (monsterState == MonsterState.TRACE)
                {
                    if (_targetDist > _traceDist)
                    {
                        _timer = 0f;
                        _idleTime = Random.Range(1f, 5f);
                        _anim.SetBool("isRun", false);
                        
                        ChangeState(MonsterState.IDLE);
                    }
                    if (_targetDist < _attackDist)
                    {
                        ChangeState(MonsterState.ATTACK);
                    }
                }
            }
        }
        
        public override void Idle()
        {
            _timer += Time.deltaTime;
            if (_timer >= _idleTime)
            {
                _timer = 0f;
                // 오브젝트 플립
                _moveDir = Random.Range(0, 2) == 1 ? 1 : -1;
                transform.localScale = new Vector3(_moveDir, 1, 1);
                
                _patrolTime = Random.Range(1f, 5f);
                _anim.SetBool("isRun", true);

                ChangeState(MonsterState.PATROL);
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
        }

        public override void Trace()
        {
            var targetDir = (target.position - transform.position).normalized;
            transform.position += Vector3.right * targetDir.x * speed  * Time.deltaTime;

            var scaleX = targetDir.x > 0 ? 1 : -1;
            transform.localScale = new Vector3(scaleX, 1, 1);
            hpBar.transform.localScale = new Vector3(scaleX, 1, 1);
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
            float currAnimLength = _anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(currAnimLength);
            
            
            _anim.SetBool("isRun", false);
            var targetDir = (target.position - transform.position).normalized;
            var scaleX = targetDir.x > 0 ? 1 : -1;
            hpBar.transform.localScale = new Vector3(scaleX, 1, 1);
            yield return new WaitForSeconds(attackCoolTime - 1f);
            
            _isAttack = false;
            _anim.SetBool("isRun", true);
            ChangeState(MonsterState.TRACE);
        }
    }
}

