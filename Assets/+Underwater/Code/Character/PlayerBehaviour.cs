using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlamingApes.Underwater.States;

namespace FlamingApes.Underwater
{
    public class PlayerBehaviour : UnitBase
    {
        [ContextMenu("Debug: Kill player")]
        public override void Die()
        {
            PlayerAttack.Instance.CanAttack = false;
            Collider.enabled = false;
            gameObject.SetActive(false);
            GameStateManager.Instance.Go(StateType.GameOver);
        }
    }
}
