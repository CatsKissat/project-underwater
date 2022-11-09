using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace FlamingApes.Underwater
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        [ShowNonSerializedField] private int gold;

        private void Awake()
        {
            if ( Instance != null )
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public void SetGold(int amount)
        {
            gold += amount;
            Debug.Log("Picked up gold. Current amount is now: " + gold);
        }
    }
}
