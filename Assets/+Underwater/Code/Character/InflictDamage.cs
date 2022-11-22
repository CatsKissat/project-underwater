using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlamingApes.Underwater.States;

namespace FlamingApes.Underwater
{
    public class InflictDamage : MonoBehaviour
    {

        public enum Faction
        {
            Player,
            Enemy
        }

        [SerializeField]
        private Faction faction;

        [SerializeField]
        private int damage = 1;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            IHealth health = collision.gameObject.GetComponentInChildren<IHealth>();
            UnitBase targetUnit = collision.gameObject.GetComponentInChildren<UnitBase>();
            InflictDamage player = GetComponent<InflictDamage>();
            InflictDamage enemy = collision.gameObject.GetComponent<InflictDamage>();

            // do both objects in this collision actually have a faction membership? 
            // If not, then we are not interested in this collision.
            if (player == null || enemy == null)
            {
                return;
            }
            // check if we have encountered an enemy
            if (player.faction != enemy.faction && health != null)
            {

                if (!health.DecreaseHealth(damage))
                {
                    // Target died
                    if (targetUnit != null)
                    {
                        // TODO: Character should have a death effect and transitioning to GameOver should be delayed
                        targetUnit.Die();
                    }
                }
            }

        }
    }
}
