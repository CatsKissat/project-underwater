using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class EnemyMoveAnimations : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private Vector3 previousPosition;
        private Vector3 currentMovementDirection;

        private void Update()
        {
            if ( previousPosition != transform.position )
            {
                animator.SetBool("isWalking", true);
                currentMovementDirection = -(previousPosition - transform.position).normalized;
                previousPosition = transform.position;

                animator.SetFloat("moveX", currentMovementDirection.x);
                animator.SetFloat("moveY", currentMovementDirection.y);
            }
            else
            {
                animator.SetBool("isWalking", false);
                Vector3 direction = transform.position - CharacterMovement.Instance.transform.position;
                animator.SetFloat("moveX", -direction.x);
                animator.SetFloat("moveY", -direction.y);
            }
        }
    }
}
