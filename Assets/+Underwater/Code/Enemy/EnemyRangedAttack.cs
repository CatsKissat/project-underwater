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


        // Start is called before the first frame update
        void Start()
        {
            objectPool = GetComponent<ObjectPool>();
        }

        // Update is called once per frame
        void Update()
        {
            float distanceFromPlayer = Vector2.Distance(target.position, transform.position);
            if (!canAttack) return;
                if(distanceFromPlayer <= shootingRange)
                {
                    Attack();                    
                }
        }

        private void Attack()
        {
            GameObject enemyBullet = objectPool.GetPooledObject();
            if (enemyBullet != null)
            {
                enemyBullet.transform.position = firePoint.transform.position;
                enemyBullet.transform.rotation = firePoint.transform.rotation;
                enemyBullet.GetComponent<Collider2D>().enabled = true;
                enemyBullet.SetActive(true);
            }
            StartCoroutine(CanShoot());

        }

        void OnEnable()
        {
            target = GameObject.Find("HeroCharacter").transform;
        }

        IEnumerator CanShoot()
        {
            canAttack = false;
            yield return new WaitForSeconds(shootingCooldown);
            canAttack = true;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, shootingRange);

        }
    }
}
