using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlamingApes.Underwater.States;

namespace FlamingApes.Underwater
{
    public class PlayerBehaviour : UnitBase
    {
        public override void Die()
        {
            Collider.enabled = false;
            gameObject.SetActive(false);
            GameStateManager.Instance.Go(StateType.GameOver);
        }
    }
}
