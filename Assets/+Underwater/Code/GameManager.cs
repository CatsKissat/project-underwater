using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager gameManager { get; private set; }

        //set the units currentHealth and maxCurrentHealth
        public UnitHealth playerHealth = new UnitHealth(100, 100);

        // Start is called before the first frame update
        //Make sure that no duplicate gamManagers are made, if so delete them.
        void Awake()
        {
            if(gameManager != null && gameManager != this)
            {
                Destroy(this);
            }

            else
            {
                gameManager = this;
            }
        }
    }
}
