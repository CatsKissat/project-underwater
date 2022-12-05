using FlamingApes.Underwater.Config;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace FlamingApes.Underwater
{
    public class EnemyBullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D bulletRB2D;
        [SerializeField] float fireForce = 20f;
        [SerializeField] float lifeTime = 2;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Light2D glowLight;

        private Transform target;
        private AudioSource openAudio;
        private Coroutine destroyCoroutinePlayerHit;
        private Coroutine destroyCoroutineDestroyProjectile;
        private Coroutine destroyCoroutineWallHit;
        private new Collider2D collider;

        private void Awake()
        {
            target = CharacterMovement.Instance.enemyTarget;
            openAudio = GetComponent<AudioSource>();
        }

        private void Update()
        {
            Vector3 vectorToTarget = target.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 180;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * fireForce);
        }

        private void OnEnable()
        {
            collider = GetComponent<Collider2D>();
            glowLight.enabled = true;
            gameObject.SetActive(true);
            collider.enabled = true;
            spriteRenderer.enabled = true;
            target = CharacterMovement.Instance.enemyTarget;

            Vector2 bulletDirection = (target.transform.position - transform.position).normalized * fireForce;
            bulletRB2D.velocity = new Vector2(bulletDirection.x, bulletDirection.y);
            if ( bulletRB2D == null )
            {
                Debug.LogError(name + " is missing " + bulletRB2D.GetType() + " component. Moving requires it!");
            }

            destroyCoroutineDestroyProjectile = StartCoroutine(DestroyProjectile());
        }

        IEnumerator DestroyProjectile()
        {
            yield return new WaitForSeconds(lifeTime);
            glowLight.enabled = false;
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            if ( destroyCoroutineWallHit != null )
            {
                StopCoroutine(WallHit());
                destroyCoroutineWallHit = null;
            }

            if ( destroyCoroutinePlayerHit != null )
            {
                StopCoroutine(PlayerHit());
                destroyCoroutinePlayerHit = null;
            }

            if ( destroyCoroutineDestroyProjectile != null )
            {
                StopCoroutine(DestroyProjectile());
                destroyCoroutineDestroyProjectile = null;
            }
        }

        IEnumerator PlayerHit()
        {
            AudioManager.PlayClip(openAudio, SoundEffect.PlayerDamaged);
            collider.enabled = false;
            spriteRenderer.enabled = false;
            glowLight.enabled = false;
            yield return new WaitForSeconds(1);
            gameObject.SetActive(false);
        }

        IEnumerator WallHit()
        {
            //AudioManager.PlayClip(openAudio, SoundEffect.HitWall);
            collider.enabled = false;
            spriteRenderer.enabled = false;
            glowLight.enabled = false;
            yield return new WaitForSeconds(1);
            gameObject.SetActive(false);
        }

        //Set gameObject to false if it collides with walls or terrain
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if ( collision.gameObject.GetComponent<Lootable>() == null && collision.gameObject.GetComponent<LevelExitZone>() == null )
            {
                destroyCoroutineWallHit = StartCoroutine(WallHit());
            }
        }

        //set gameObject to false when it hits players hitbox
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ( collision.gameObject.GetComponent<Lootable>() == null && collision.gameObject.GetComponent<LevelExitZone>() == null )
            {
                destroyCoroutinePlayerHit = StartCoroutine(PlayerHit());
            }
        }

    }
}
