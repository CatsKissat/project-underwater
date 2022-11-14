using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [Tooltip("Spawn a new enemy after this time has passed (seconds).")]
        [SerializeField] private float spawnCooldown = 2.0f;
        private Coroutine spawnerCoroutine;

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

                Vector2 directionToPlayer = (CharacterMovement.Instance.transform.position - transform.position).normalized;
                Vector2 spawnOffset = transform.position + new Vector3(directionToPlayer.x, directionToPlayer.y);

                Instantiate(enemyPrefab, spawnOffset, Quaternion.identity);
                spawnerCoroutine = null;
                spawnerCoroutine = StartCoroutine(SpawnEnemy());
#if UNITY_EDITOR
            }
#endif
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
