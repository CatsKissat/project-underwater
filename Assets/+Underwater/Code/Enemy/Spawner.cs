using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class Spawner : MonoBehaviour
    {
        [Tooltip("Spawn a new enemy after this time has passed (seconds).")]
        [SerializeField] private float spawnCooldown = 2.0f;

        private Coroutine spawnerCoroutine;
        private ObjectPool objectPool;

#if UNITY_EDITOR
        [Header("Debug")]
        [SerializeField] private bool enableSpawning = true;
#endif

        private void OnEnable()
        {
            spawnerCoroutine = StartCoroutine(SpawnEnemy());
        }

        private IEnumerator SpawnEnemy()
        {
#if UNITY_EDITOR
            if ( enableSpawning )
            {
#endif
                yield return new WaitForSeconds(spawnCooldown);

                //Instantiate(enemyPrefab, spawnOffset, Quaternion.identity);

                GameObject enemy = objectPool.GetPooledObject();

                if ( enemy != null )
                {
                    Vector2 directionToPlayer = (CharacterMovement.Instance.transform.position - transform.position).normalized;
                    Vector2 spawnOffset = transform.position + new Vector3(directionToPlayer.x, directionToPlayer.y);

                    enemy.transform.position = spawnOffset;
                    enemy.transform.rotation = transform.rotation;
                    enemy.GetComponent<Collider2D>().enabled = true;
                    enemy.SetActive(true);
                }

                spawnerCoroutine = null;
                spawnerCoroutine = StartCoroutine(SpawnEnemy());
#if UNITY_EDITOR
            }
#endif
        }

        private void Start()
        {
            objectPool = GetComponent<ObjectPool>();
        }

        private void OnDisable()
        {
            StopAndNullCoroutine();
        }

        private void OnDestroy()
        {
            StopAndNullCoroutine();
        }

        private void StopAndNullCoroutine()
        {
            if ( spawnerCoroutine != null )
            {
                StopCoroutine(SpawnEnemy());
                spawnerCoroutine = null;
            }
        }
    }
}
