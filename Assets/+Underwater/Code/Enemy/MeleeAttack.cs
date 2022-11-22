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

        private bool isAttacking = false;

        // TODO: Visualize melee range in editor with a gizmo

        private void Awake()
        {
            hitCollider.enabled = false;
            hitSprite.enabled = false;
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
            // TODO: Ensure enemy is targeting player before attacking
            Debug.Log(name + " attacked.");
            hitCollider.enabled = true;
            hitSprite.enabled = true;
            yield return new WaitForSeconds(meleeDuration);
            hitCollider.enabled = false;
            hitSprite.enabled = false;
            yield return new WaitForSeconds(attackCooldown);
            isAttacking = false;
        }
    }
}
