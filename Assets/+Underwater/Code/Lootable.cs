using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace FlamingApes.Underwater
{
    public class Lootable : MonoBehaviour, ICollectable
    {
        enum LootType
        {
            Gold,
            Health
        }

        [SerializeField] private LootType lootType;
        [ShowIf(nameof(LootTypeGold))]
        [SerializeField] private int goldAmount;
        [ShowIf(nameof(LootTypeHealth))]
        [SerializeField] private int healAmount;

        private Health playerHealth;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ( collision.GetComponent<CharacterMovement>() != null )
            {
                Debug.Log(name + " collided with a player");
                Collect();
            }
        }

        public void Collect()
        {
            switch ( lootType )
            {
                case LootType.Gold:
                    GameManager.Instance.SetGold(goldAmount);
                    break;
                case LootType.Health:
                    CharacterMovement.Instance.GetComponent<Health>().IncreaseHealth(healAmount);
                    break;
            }
            Destroy(gameObject);
        }

        public bool LootTypeGold()
        {
            return lootType == LootType.Gold ? true : false;
        }

        public bool LootTypeHealth()
        {
            return lootType == LootType.Health ? true : false;
        }
    }
}
