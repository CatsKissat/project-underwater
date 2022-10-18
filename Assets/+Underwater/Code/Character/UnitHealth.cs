using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class UnitHealth
    {
        int currentHealth;
        int currentMaxHealth;
        
        public int Health
        {
            get
            {
                return currentHealth;
            }

            set
            {
                currentHealth = value;
            }
        }
        public int MaxHealth
        {
            get
            {
                return currentMaxHealth;
            }

            set
            {
                currentMaxHealth = value;
            }
        }

        //Constructor
        public UnitHealth(int health, int maxHealth)
        {
            currentHealth = health;
            currentMaxHealth = maxHealth;
        }

        //Methods
        public void DamageUnit(int damageAmount)
        {
            if(currentHealth > 0)
            {
                currentHealth -= damageAmount;
            }


        }

        public void HealUnit(int healAmount)
        {
            if (currentHealth < currentMaxHealth)
            {
                currentHealth += healAmount;
            }

            //dont allow overheal, heal till currentMaxHealth
            if(currentHealth > currentMaxHealth)
            {
                currentHealth = currentMaxHealth;
            }


        }
    }
}
