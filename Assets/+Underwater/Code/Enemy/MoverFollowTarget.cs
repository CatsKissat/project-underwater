using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class MoverFollowTarget : MonoBehaviour, IMover
    {
        [SerializeField] private float moveSpeed = 4.0f;
        private Rigidbody2D rb2d;
        private Vector2 direction;
        private float angle;
        private IAttack enemyAttack;

        private void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            if ( rb2d == null )
            {
                Debug.LogError(name + " is missing " + rb2d.GetType() + " component. Moving requires it!");
            }

            enemyAttack = GetComponent<IAttack>();
            if ( enemyAttack == null )
            {
                Debug.LogError(name + " is missing " + enemyAttack.GetType() + " component. Attacking requires it!");
            }
        }

        private void Update()
        {
            direction = (CharacterMovement.Instance.enemyTarget.position - transform.position).normalized;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }

        private void FixedUpdate()
        {
            float distance = Vector2.Distance(transform.position, CharacterMovement.Instance.enemyTarget.position);

            if ( distance >= enemyAttack.GetAttackRange && !enemyAttack.IsAttacking )
            {
                rb2d.rotation = angle;
                Move();
            }
            else
            {
                rb2d.velocity = new Vector3(0.0f, 0.0f);
                if ( !enemyAttack.IsAttacking )
                {
                    StartCoroutine(enemyAttack.Attack());
                }
            }
        }

        public void Move()
        {
            rb2d.velocity = new Vector2(direction.x * moveSpeed * Time.deltaTime, direction.y * moveSpeed * Time.deltaTime);
        }
    }
}
