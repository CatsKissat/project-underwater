using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class EnemyBulletScript : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D bulletRB2D;

        [SerializeField]
        float fireForce = 20f;

        [SerializeField]
        float lifeTime = 2;

        private Transform target;

        private void OnEnable()
        {
            target = GameObject.Find("HeroCharacter").transform;
            Vector2 moveDie = (target.transform.position - transform.position).normalized * fireForce;
            bulletRB2D.velocity = new Vector2(moveDie.x, moveDie.y);

            if (bulletRB2D == null)
            {
                Debug.LogError(name + " is missing " + bulletRB2D.GetType() + " component. Moving requires it!");
            }
            Invoke(nameof(Destroy), lifeTime);
        }

        private void Destroy()
        {
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
