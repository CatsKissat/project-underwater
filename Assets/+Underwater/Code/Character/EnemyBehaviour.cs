using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Profiling;
using Thread = System.Threading.Thread;


namespace FlamingApes.Underwater
{
	public class EnemyBehaviour : UnitBase
	{
        public void Start()
        {
			StartCoroutine(ScanStarAI());
        }

        public override void Die()
		{
			Collider.enabled = false;
			gameObject.SetActive(false);
		}

		IEnumerator ScanStarAI()
{
			yield return new WaitForSeconds(1);
			AstarPath.active.Scan();
			

		}
	}
}
