using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
	public class EnemyBehaviour : UnitBase
	{
		public override void Die()
		{
			Collider.enabled = false;
			gameObject.SetActive(false);
		}
	}
}
