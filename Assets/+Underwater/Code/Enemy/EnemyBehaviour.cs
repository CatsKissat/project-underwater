using System;
using System.Collections.Generic;
using UnityEngine;


namespace FlamingApes.Underwater
{
	public class EnemyBehaviour : UnitBase
	{
        [SerializeField] private bool canDropLoot = false;
        [SerializeField] private List<GameObject> loots;
        [Tooltip("Drop chance in percentage.")]
        [SerializeField] private int dropChance = 20;

        public override void Die()
		{
			Collider.enabled = false;

            if ( canDropLoot )
            {
                int rngLoot = UnityEngine.Random.Range(0, 100);

                if ( rngLoot <= dropChance )
                {
                    int index = UnityEngine.Random.Range(0, loots.Count);

                    Instantiate(loots[index], transform.position, Quaternion.identity);
                } 
            }

			gameObject.SetActive(false);
		}
	}
}
