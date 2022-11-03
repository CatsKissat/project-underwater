using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class PlayerBehaviour : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                PlayerTakeDamage(20);
                Debug.Log(GameManager.gameManager.playerHealth.Health);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                PlayerHeal(20);
                Debug.Log(GameManager.gameManager.playerHealth.Health);
            }
        }
        private void PlayerTakeDamage(int damage)
        {
            GameManager.gameManager.playerHealth.DamageUnit(damage);
        }

        private void PlayerHeal(int healing)
        {
            GameManager.gameManager.playerHealth.HealUnit(healing);
        }
    }
}
