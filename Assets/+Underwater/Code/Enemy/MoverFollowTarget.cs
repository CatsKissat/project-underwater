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
        private IAttack attack;

        private void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            if ( rb2d == null )
            {
                Debug.LogError(name + " is missing " + rb2d.GetType() + " component. Moving requires it!");
            }

            attack = GetComponent<IAttack>();
            if ( attack == null )
            {
                Debug.LogError(name + " is missing " + attack.GetType() + " component. Attacking requires it!");
            }
        }

        private void Update()
        {
            direction = (CharacterMovement.Instance.transform.position - transform.position).normalized;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }

        private void FixedUpdate()
        {
            float distance = Vector2.Distance(transform.position, CharacterMovement.Instance.transform.position);
            rb2d.rotation = angle;
            if ( distance >= attack.GetAttackRange )
            {
                Move();
            }
            else
            {
                rb2d.velocity = new Vector3(0.0f, 0.0f);
                if ( !attack.IsAttacking )
                {
                    StartCoroutine(attack.Attack());
                }
            }
        }

        public void Move()
        {
            rb2d.velocity = new Vector2(direction.x * moveSpeed * Time.deltaTime, direction.y * moveSpeed * Time.deltaTime);
        }
    }
}
