using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlamingApes.Underwater.States
{
	public class OptionsState : GameStateBase
	{
		public override string SceneName
		{
			get { return "Options"; }
		}

		public override StateType Type
		{
			get { return StateType.Options; }
		}

		public override bool IsAdditive
		{
			get { return true; }
		}

		public OptionsState() : base()
		{
			AddTargetState(StateType.InGame);
			AddTargetState(StateType.MainMenu);
		}
	}
}
