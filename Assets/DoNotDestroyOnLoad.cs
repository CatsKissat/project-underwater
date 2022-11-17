using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class DoNotDestroyOnLoad : MonoBehaviour
    {

        public static DoNotDestroyOnLoad Instance { get; private set; }

        // Start is called before the first frame update
        void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
            }

            else
            {
                Instance = this;
            }
            DontDestroyOnLoad(gameObject);
        }
    }
}
