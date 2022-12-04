using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class EnemyRangedAttack : MonoBehaviour
    {
        [SerializeField]
        private float shootingRange = 10;

        [SerializeField]
        private Transform firePoint;

        [SerializeField]
        private float shootingCooldown = 2.0f;

        [SerializeField]
        private float fireRate = 1F;

        private Transform target;
        private ObjectPool objectPool;
        private bool canAttack = true;
        private Coroutine attackCooldownCoroutine;

        void Start()
        {
            target = CharacterMovement.Instance.enemyTarget;
            objectPool = GetComponent<ObjectPool>();
        }

        void Update()
        {
            float distanceToPlayer = Vector2.Distance(target.position, transform.position);
            if ( !canAttack ) return;
            if ( distanceToPlayer <= shootingRange )
            {
                Attack();
            }
        }

        private void Attack()
        {
            GameObject enemyBullet = objectPool.GetPooledObject();
            if ( enemyBullet != null )
            {
                enemyBullet.transform.position = firePoint.transform.position;
                enemyBullet.transform.rotation = firePoint.transform.rotation;
                enemyBullet.GetComponent<Collider2D>().enabled = true;
                enemyBullet.GetComponent<EnemyBullet>().enabled = true;
                enemyBullet.SetActive(true);
                attackCooldownCoroutine = StartCoroutine(CanShoot());
            }
        }

        IEnumerator CanShoot()
        {
            canAttack = false;
            yield return new WaitForSeconds(shootingCooldown);
            canAttack = true;
            attackCooldownCoroutine = null;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, shootingRange);
        }

        private void OnEnable()
        {
            canAttack = true;
        }

        private void OnDisable()
        {
            if ( attackCooldownCoroutine != null )
            {
                StopCoroutine(CanShoot());
                attackCooldownCoroutine = null;
            }
        }
    }
}
