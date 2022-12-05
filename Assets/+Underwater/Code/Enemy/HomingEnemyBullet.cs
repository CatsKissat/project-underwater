using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class HomingEnemyBullet : MonoBehaviour
    {
        [SerializeField]
        private float bulletSpeed = 5;

        [SerializeField]
        float lifeTime = 2;

        [SerializeField]
        private float rotateSpeed = 200;

        [SerializeField]
        private Transform bullet;

        private Transform target;
        private Coroutine destroyCoroutine;


        private void Start()
        {
            target = CharacterMovement.Instance.enemyTarget;
        }

        private void OnEnable()
        {
            destroyCoroutine = StartCoroutine(DestroyProjectile());
        }

        public void FixedUpdate()
        {

            transform.position = Vector2.MoveTowards(transform.position, target.position, bulletSpeed * Time.deltaTime);
            Vector3 vectorToTarget = target.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 180;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * bulletSpeed);
            StartCoroutine(DestroyProjectile());
        }

        // Update is called once per frame

        IEnumerator DestroyProjectile()
        {
            yield return new WaitForSeconds(lifeTime);
            gameObject.SetActive(false);
        }


        //Set gameObject to false if it collides with walls or terrain

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if ( collision.gameObject.GetComponent<Lootable>() == null && collision.gameObject.GetComponent<LevelExitZone>() == null )
            {
                gameObject.SetActive(false);
                StopAndNullCoroutine();
            }
        }

        //set gameObject to false when it hits players hitbox
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ( collision.gameObject.GetComponent<Lootable>() == null && collision.gameObject.GetComponent<LevelExitZone>() == null )
            {
                gameObject.SetActive(false);
                StopAndNullCoroutine();
            }
        }

        private void StopAndNullCoroutine()
        {
            if ( destroyCoroutine != null )
            {
                StopCoroutine(DestroyProjectile());
                destroyCoroutine = null;
            }
        }
    }
}
