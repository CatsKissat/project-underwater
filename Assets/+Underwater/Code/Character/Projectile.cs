using FlamingApes.Underwater.Config;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rb2D;

        [SerializeField]
        float fireForce = 20f;

        [SerializeField]
        float lifeTime = 2;

        private SpriteRenderer spriteRenderer;
        private AudioSource openAudio;
        private new Collider2D collider;
        private Coroutine destroyCoroutineWallHit;
        private Coroutine destroyCoroutineEnemyHit;
        private Coroutine destroyCoroutineDestroyProjectile;



        private void Awake()
        {
            openAudio = GetComponent<AudioSource>();
            collider = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            gameObject.SetActive(true);
            collider.enabled = true;
            spriteRenderer.enabled = true;         
            rb2D.AddForce(-transform.right * fireForce, ForceMode2D.Impulse);
            
            destroyCoroutineDestroyProjectile = StartCoroutine(DestroyProjectile());          
        }

        private void OnDisable()
        {
            if (destroyCoroutineWallHit != null)
            {
                StopCoroutine(WallHit());
                destroyCoroutineWallHit = null;
            }

            if (destroyCoroutineEnemyHit != null)
            {
                StopCoroutine(EnemyHit());
                destroyCoroutineEnemyHit = null;
            }

            if (destroyCoroutineDestroyProjectile != null)
            {
                StopCoroutine(DestroyProjectile());
                destroyCoroutineDestroyProjectile = null;
            }
        }

        IEnumerator DestroyProjectile()
        {
            yield return new WaitForSeconds(lifeTime);
            gameObject.SetActive(false);
        }

        IEnumerator WallHit()
        {
            AudioManager.PlayClip(openAudio, SoundEffect.HitWall);
            collider.enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(1);
            gameObject.SetActive(false);
        }

        IEnumerator EnemyHit()
        {
            AudioManager.PlayClip(openAudio, SoundEffect.EnemyDamaged);
            collider.enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(1);
            gameObject.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            destroyCoroutineWallHit = StartCoroutine(WallHit());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            destroyCoroutineEnemyHit = StartCoroutine(EnemyHit());
        }


    }
}