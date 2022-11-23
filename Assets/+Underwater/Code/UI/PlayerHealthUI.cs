using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;

namespace FlamingApes.Underwater
{
    public class PlayerHealthUI : MonoBehaviour
    {
        [SerializeField] private GameObject heartPrefab;

        private Health playerHealth;

        void Start()
        {
            playerHealth = CharacterMovement.Instance.gameObject.GetComponent<Health>();

            // TODO: Dynamically check and update heart count to UI
            //maxHearts = new Image[playerHealth.MaxHealth];

            UpdateHealthToUI();
            playerHealth.onDamaged += UpdateHealthToUI;
        }

        private void OnDisable()
        {
            playerHealth.onDamaged -= UpdateHealthToUI;
        }

        private void UpdateHealthToUI()
        {
        }      
    }
}
