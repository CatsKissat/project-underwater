using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlamingApes.Underwater.States;

namespace FlamingApes.Underwater
{
    public class LevelExitZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ( collision.GetComponent<CharacterMovement>() != null )
            {
                Debug.Log(collision.gameObject.name + " collided with exit");
                GameManager.Instance.Level += 1;
                GameStateManager.Instance.Go(StateType.InGame);
            }
        }
    }
}
