using System.Threading;
using UnityEngine;

namespace Platformer
{
    public class Golem : Monster
    {
        private float moveDir;

        private float timer;
        private float idleTime = 3f;
        private float patrolTime = 5f;
        private float attackTime;

        private float traceDistance = 8f;
        private float attackDistance = 1f;
        void Start ()
        {
            Init();
            attackTime = monsterData.attackTime;
        }

        protected override void Idle()
        {
            timer += Time.deltaTime;
            if (timer > idleTime)
            {
                timer = 0f;
                patrolTime = Random.Range(0f, 5f);

                int ranNumber = Random.Range(0, 2);

                moveDir = Random.Range(0,2) == 0 ? -1 : 1;

                transform.localScale = new Vector3(moveDir, 1, 1);

                anim.SetBool("IsRun" , true);
                ChangeState(MonsterState.Patrol);
            }

            if ( distance <= traceDistance)
            {
                anim.SetBool("IsRun", true);
                ChangeState(MonsterState.Trace);
            }
        }

        protected override void Patrol()
        {

        }

        protected override void Trace()
        {
            anim.SetBool("IsRun", true );
            // 타겟을 향한 방향
            Vector2 targetDir = (target.position - transform.position).normalized;

            transform.position += Vector3.right * targetDir.x * moveSpeed * Time.deltaTime;

            if (targetDir.x < 0)
                moveDir = 1;
            else if ( targetDir.x > 0)
                moveDir = -1;

            transform.localScale = new Vector3(moveDir, 1, 1);

            if (distance > traceDistance)
            {
                timer = 0f;
                idleTime = Random.Range(0f, 5f);

                anim.SetBool("IsRun", false);
                ChangeState(MonsterState.Idle);
            }
            else if  ( distance <= attackDistance)
            {
                timer = attackTime;
                anim.SetBool("IsRun", false);
                ChangeState(MonsterState.Attack);
            }
        }

        protected override void Attack()
        {

            rb.linearVelocity = Vector2.zero;

            timer += Time.deltaTime;

            if (timer >= attackTime)
            {
                timer = 0f;
                anim.SetTrigger("Attack");
            }

            if (distance > attackDistance)
            {
                anim.SetTrigger("Attack");
                ChangeState(MonsterState.Trace);
            }
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Wall"))
            {
                Debug.Log("방향을 반대로 변경");
                moveDir *= -1;
                transform.localScale = new Vector3(moveDir, 1, 1);
            }
        }



    }
}
