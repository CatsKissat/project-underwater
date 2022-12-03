using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class ExitSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject exitsParent;

        private List<GameObject> possibleExitLocation = new List<GameObject>();

        private void Awake()
        {
            foreach ( Transform exit in exitsParent.transform )
            {
                possibleExitLocation.Add(exit.gameObject);
            }

            foreach ( var exit in possibleExitLocation )
            {
                exit.SetActive(false);
            }
        }

        public void RandomizeExit()
        {
            int exitIndex = Random.Range(0, possibleExitLocation.Count);

            for ( int i = 0; i < possibleExitLocation.Count; i++ )
            {
                if ( i == exitIndex )
                {
                    possibleExitLocation[exitIndex].SetActive(true);
                }
            }
        }
    }
}
