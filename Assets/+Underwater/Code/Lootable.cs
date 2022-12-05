using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using FlamingApes.Underwater.Config;
using UnityEngine.Rendering.Universal;

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
        private AudioSource openAudio;
        private Health playerHealth;

        [SerializeField]
        private float destroyObjectDelay = 5;

        private new Collider2D collider;
    
        private void Awake()
        {
            openAudio = GetComponent<AudioSource>();
            collider = GetComponent<Collider2D>();
        }

        
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
                    GameManager.Instance.Gold = goldAmount;
                    //Play the aduio sound in audioContainer
                    if (openAudio != null)
                    {
                        AudioManager.PlayClip(openAudio, SoundEffect.PickUpCoin);
                    }
                    break;
                case LootType.Health:
                    CharacterMovement.Instance.GetComponent<Health>().IncreaseHealth(healAmount);
                    if (openAudio != null)
                    
                    {
                        AudioManager.PlayClip(openAudio, SoundEffect.PickUpHeal);
                    }
                    break;
            }
            Destroy();
        }

        public bool LootTypeGold()
        {
            return lootType == LootType.Gold ? true : false;
        }

        public bool LootTypeHealth()
        {
            return lootType == LootType.Health ? true : false;
        }

        private void Destroy()
        {
            collider.enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<ShadowCaster2D>().enabled = false;
            Destroy(gameObject, destroyObjectDelay);
        }
    }
}
