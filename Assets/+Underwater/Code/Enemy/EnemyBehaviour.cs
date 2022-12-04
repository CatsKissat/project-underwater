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
        [SerializeField] private Animator animator;

        public override void Die()
		{
			Collider.enabled = false;
			gameObject.SetActive(false);
		}
	}
}
