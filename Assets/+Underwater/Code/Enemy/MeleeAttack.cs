using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    [RequireComponent(typeof(UnitSensor))]
    public class MeleeAttack : MonoBehaviour, IAttack
    {
        [SerializeField] private float attackRange;
        [SerializeField] private float meleeDuration = 0.2f;
        [SerializeField] private float attackCooldown = 1.0f;
        [SerializeField] private Collider2D hitCollider;

        private Animator animator;
        private bool isAttacking = false;

        private void Awake()
        {
            hitCollider.enabled = false;
            animator = GetComponent<Animator>();
        }

        public float GetAttackRange
        {
            get => attackRange;
        }

        public bool IsAttacking
        {
            get => isAttacking;
        }

        public IEnumerator Attack()
        {
            isAttacking = true;
            hitCollider.enabled = true;
            animator.SetTrigger("isAttacking");

            yield return new WaitForSeconds(meleeDuration);
            hitCollider.enabled = false;

            yield return new WaitForSeconds(attackCooldown);
            isAttacking = false;
        }

        private void OnEnable()
        {
            isAttacking = false;
            hitCollider.enabled = false;
        }

        private void OnDisable()
        {
            StopCoroutine(Attack());
        }
    }
}
