using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlamingApes.Underwater.States;
using UnityEngine.SceneManagement;

namespace FlamingApes.Underwater
{
    public class LevelExitZone : MonoBehaviour
    {
        [SerializeField] private float enableExitDistance = 3.0f;

        private Animator animator;
        private Coroutine checkDistanceToPlayer;
        private bool isExitOpen;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            checkDistanceToPlayer = StartCoroutine(CheckPlayerLocation());
        }

        private IEnumerator CheckPlayerLocation()
        {
            var distance = (transform.position - CharacterMovement.Instance.transform.position).magnitude;

            if ( distance <= enableExitDistance )
            {
                animator.SetTrigger("openExit");
                isExitOpen = true;
            }

            yield return new WaitForSeconds(0.5f);

            checkDistanceToPlayer = null;
            if ( !isExitOpen )
            {
                checkDistanceToPlayer = StartCoroutine(CheckPlayerLocation());
            }
        }

        private void OnDisable()
        {
            StopAndNullCoroutine();
        }

        private void StopAndNullCoroutine()
        {
            if ( checkDistanceToPlayer != null )
            {
                StopCoroutine(CheckPlayerLocation());
                checkDistanceToPlayer = null;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ( collision.GetComponent<CharacterMovement>() != null )
            {
                Debug.Log(collision.gameObject.name + " collided with exit");
                GameManager.Instance.Level += 1;

                SceneManager.LoadScene("PlayScene");
            }
        }
    }
}
