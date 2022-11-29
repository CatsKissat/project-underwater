using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class MapScanner : MonoBehaviour
    {
        [SerializeField]
        private float waitForScan = 1;
        public void Start()
        {
            StartCoroutine(ScanStarAI());
        }

        IEnumerator ScanStarAI()
        {
            yield return new WaitForSeconds(waitForScan);
            AstarPath.active.Scan();
        }
    }
}
