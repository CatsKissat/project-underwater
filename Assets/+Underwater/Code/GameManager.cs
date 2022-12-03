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
        [ShowNonSerializedField] private int currentLevel = 1;

        public int Level
        {
            get => currentLevel;
            set => currentLevel++;
        }

        public int Gold
        {
            get => gold;
            set => gold += value;
        }

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

            DontDestroyOnLoad(this);
        }
    }
}
