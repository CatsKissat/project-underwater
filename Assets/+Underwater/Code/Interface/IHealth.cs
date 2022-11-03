using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public interface IHealth
    {

		public interface IHealth
		{
			
			// Returns the current health.
			
			int CurrentHealth { get; }

			
			// Maximum health.
			// The CurrentHealth can never exceed this.
			
			int MaxHealth { get; }

		
			//Minimum health.
			//The CurrentHealth can never be below this.

			int MinHealth { get; }


			// Increases the health by the amount.
			// Note: CurrentHealth can never exceed the MaxHealth.

			void IncreaseHealth(int amount);


			// Decreases the health by the amount.
			// Note: CurrentHealth can never be below the MinHealth.

			bool DecreaseHealth(int amount);


			// Resets the component to its default values.
			void Reset();
		}
	}

}

