using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
		public abstract class UnitBase : MonoBehaviour
		{
			private IHealth health;
			public IHealth Health { get { return health; } }
			public Collider2D Collider { get; private set; }
			private new AudioSource audio;



		protected virtual void Awake()
			{
				health = GetComponent<IHealth>();
				if (health == null)
				{
					Debug.LogError("Can't find a component which implements the IHealth interface!");
				}

				Collider = GetComponent<Collider2D>();	
			}

			protected virtual void Update()
			{
			}

			public virtual void Die()
			{
			// The default implementation just destroys the gameObject.
			Destroy(gameObject);
			}
		}
	}

