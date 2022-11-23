using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

namespace FlamingApes.Underwater
{
    public class Health : MonoBehaviour, IHealth
    {
        private enum UnitType
        {
            Enemy = 0,
            Player = 1
        }

        [SerializeField] private int maxHealth;
        private int minHealth;
        private int currentHealth;

        // Player HP in UI
        [SerializeField] private UnitType unit;
        public UnityAction onDamaged;

        public int CurrentHealth
        {
            get
            {
                return currentHealth;
            }
            private set
            {
                // Makes sure the currentHealth stays within the defined range
                currentHealth = Mathf.Clamp(value, MinHealth, MaxHealth);
            }
        }

        public int MaxHealth
        {
            get { return maxHealth; }
        }

        public int MinHealth
        {
            get { return minHealth; }
        }

        private void Awake()
        {
            Reset();
        }

        // Decreases the health.
        /// </summary>
        /// <param name="amount">The amount to decrease</param>
        /// <returns>True, if the object is still alive. False otherwise.</returns>
        public bool DecreaseHealth(int amount)
        {
            if ( amount < 0 ) return CurrentHealth > MinHealth;

            CurrentHealth -= amount;

            if ( unit == UnitType.Player )
            {
                Debug.Log("Player damaged");
                onDamaged.Invoke();
            }

            return CurrentHealth > MinHealth;
        }

        /// <summary>
        /// Increases the health.
        /// </summary>
        /// <param name="amount">Amount to increase</param>
        public void IncreaseHealth(int amount)
        {
            if ( amount < 0 ) return;

            CurrentHealth += amount;
        }

        public void Reset()
        {
            CurrentHealth = maxHealth;
        }

        public void HealthUpdated()
        {

        }
    }
}
