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
        [SerializeField] private SpriteRenderer hitSprite;
        [SerializeField] private Animator animator;

        private bool isAttacking = false;

        // TODO: Visualize melee range in editor with a gizmo

        private void Awake()
        {
            hitCollider.enabled = false;
            hitSprite.enabled = false;
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
            hitSprite.enabled = true;
            animator.SetTrigger("isAttacking");

            yield return new WaitForSeconds(meleeDuration);
            hitCollider.enabled = false;
            hitSprite.enabled = false;

            yield return new WaitForSeconds(attackCooldown);
            isAttacking = false;
        }

        private void OnEnable()
        {
            isAttacking = false;
            hitCollider.enabled = false;
            hitSprite.enabled = false;
        }

        private void OnDisable()
        {
            StopCoroutine(Attack());
        }
    }
}
