using FlamingApes.Underwater.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
	public class UIGameOver : MonoBehaviour
	{
		public void Restart()
		{
			GameStateManager.Instance.Go(StateType.InGame);
		}

		public void BackToMenu()
		{
			GameStateManager.Instance.Go(StateType.MainMenu);
		}

		public void Quit()
		{
			Application.Quit();
		}
	}
}
