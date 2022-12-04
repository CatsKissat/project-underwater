using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class EnemySpawner : MonoBehaviour
    {
        [Tooltip("If distance to player is less than this, spawner will spawn enemies.")]
        [SerializeField] private float spawnDistance = 10.0f;

        [Tooltip("Spawn a new enemy after this time has passed (seconds).")]
        [SerializeField] private float spawnCooldown = 2.0f;
        [SerializeField] private List<GameObject> possibleEnemies;

        private Coroutine spawnerCoroutine;
        private ObjectPool objectPool;
        private float distanceToPlayer;

#if UNITY_EDITOR
        [Header("Debug")]
        [SerializeField] private bool enableSpawning = true;
#endif
        private void Update()
        {
            distanceToPlayer = (CharacterMovement.Instance.transform.position - transform.position).magnitude;

            if ( distanceToPlayer < spawnDistance && spawnerCoroutine == null )
            {
                spawnerCoroutine = StartCoroutine(SpawnEnemy());
            }
        }

        private IEnumerator SpawnEnemy()
        {
#if UNITY_EDITOR
            if ( enableSpawning )
            {
#endif
                yield return new WaitForSeconds(spawnCooldown);

                GameObject enemy = objectPool.GetPooledObject();

                if ( enemy != null )
                {
                    Vector2 directionToPlayer = (CharacterMovement.Instance.transform.position - transform.position).normalized;
                    Vector2 spawnOffset = transform.position + new Vector3(directionToPlayer.x, directionToPlayer.y);

                    enemy.transform.position = spawnOffset;
                    enemy.transform.rotation = transform.rotation;
                    enemy.GetComponent<Collider2D>().enabled = true;
                    enemy.SetActive(true);

                    spawnerCoroutine = null;
                }
#if UNITY_EDITOR
            }
#endif
        }

        private void Awake()
        {
            RandomizeEnemy();
        }

        private void RandomizeEnemy()
        {
            objectPool = GetComponent<ObjectPool>();

            int enemyIndex = Random.Range(0, possibleEnemies.Count);
            objectPool.SetPoolObject(possibleEnemies[enemyIndex]);
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, spawnDistance);
        }
    }
}
