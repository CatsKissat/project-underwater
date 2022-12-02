using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class PlayerHealthUI : MonoBehaviour
    {
        [SerializeField] private GameObject heartContainer;

        private List<HeartUI> hearts = new List<HeartUI>();
        private Health playerHealth;


        private void UpdateHealthToUI()
        {
            ClearHearts();

            float maxHealthRemainder = playerHealth.MaxHealth % 2;
            int heartsToMake = (int)((playerHealth.MaxHealth / 2) + maxHealthRemainder);
            for ( int i = 0; i < heartsToMake; i++ )
            {
                CreateEmptyHeart();
            }

            for ( int i = 0; i < hearts.Count; i++ )
            {
                int heartStateRemainder = (int)Mathf.Clamp(playerHealth.CurrentHealth - (i * 2), 0, 2);
                hearts[i].UpdateHeartIcon((HeartUI.HeartState)heartStateRemainder);
            }
        }

        private void CreateEmptyHeart()
        {
            // TODO: Instead of Destroying and re-Instantiating hearts enable and disable them.
            GameObject heart = Instantiate(heartContainer);
            heart.transform.SetParent(transform);

            // Set heart container scales to 1.0, because for some reason it was 0,16 by default
            Vector3 scale = new Vector3(1.0f, 1.0f, 1.0f);
            heart.transform.localScale = scale;

            HeartUI heartComponent = heart.GetComponent<HeartUI>();
            heartComponent.UpdateHeartIcon(HeartUI.HeartState.Empty);
            hearts.Add(heartComponent);
        }

        private void ClearHearts()
        {
            foreach ( Transform t in transform )
            {
                Destroy(t.gameObject);
            }
            hearts = new List<HeartUI>();
        }

        void Start()
        {
            playerHealth = CharacterMovement.Instance.gameObject.GetComponent<Health>();

            // TODO: Dynamically check and update heart count to UI
            //maxHearts = new Image[playerHealth.MaxHealth];

            UpdateHealthToUI();
            playerHealth.onHealthChanged += UpdateHealthToUI;
        }

        private void OnDisable()
        {
            playerHealth.onHealthChanged -= UpdateHealthToUI;
        }
    }
}
