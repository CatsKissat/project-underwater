using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class EnemyBullet : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D bulletRB2D;

        [SerializeField]
        float fireForce = 20f;

        [SerializeField]
        float lifeTime = 2;

        private Transform target;

        private void Start()
        {         
            Vector3 vectorToTarget = target.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 180;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * fireForce);            
        }

        private void OnEnable()
        {
            target = GameObject.Find("HeroCharacter").transform;
            Vector2 bulletDirection = (target.transform.position - transform.position).normalized * fireForce;
            bulletRB2D.velocity = new Vector2(bulletDirection.x, bulletDirection.y);
            if (bulletRB2D == null)
            {
                Debug.LogError(name + " is missing " + bulletRB2D.GetType() + " component. Moving requires it!");
            }
            StartCoroutine(DestroyProjectile());
        }

        IEnumerator DestroyProjectile()
        {
            yield return new WaitForSeconds(lifeTime);
            gameObject.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            gameObject.SetActive(false);
        }
    }
}
