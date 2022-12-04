using FlamingApes.Underwater.States;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;
using TMPro;
using System.Collections;

namespace FlamingApes.Underwater.UI
{
	public class UIOptions : MonoBehaviour
	{
		[SerializeField]
		private UIVolumeControl masterVolume;

		[SerializeField]
		private UIVolumeControl musicVolume;

		[SerializeField]
		private UIVolumeControl sfxVolume;

		[SerializeField]
		private AudioMixer mixer;

		[SerializeField]
		private string masterVolumeName;

		[SerializeField]
		private string musicVolumeName;

		[SerializeField]
		private string sfxVolumeName;

		[SerializeField]
		private List<ResolutionSetting> resolutions = new List<ResolutionSetting>();

		[SerializeField]
		private TMP_Text resolutionLabel;

		private int selectedResolution;

		private void Start()
		{
			masterVolume.Setup(mixer, masterVolumeName);
			musicVolume.Setup(mixer, musicVolumeName);
			sfxVolume.Setup(mixer, sfxVolumeName);
		}

		public void Save()
		{
			Debug.Log("Save options");
			masterVolume.Save();
			musicVolume.Save();
			sfxVolume.Save();
		}

		public void Close()
		{
			GameStateManager.Instance.GoBack();
		}

		public void ExitToMenu()
		{
			GameStateManager.Instance.Go(StateType.MainMenu);
		}

		[System.Serializable]
		public class ResolutionSetting
        {
			public int horizontal, vertical;
        }

		public void ResolutionToggleRight()
        {
			selectedResolution++;
			if(selectedResolution > resolutions.Count - 1)
            {
				selectedResolution = resolutions.Count - 1;
            }
			UpdateResolutionLabel();
		}

		public void ResolutionToggleLeft()
		{
			selectedResolution--;
			if(selectedResolution < 0)
            {
				selectedResolution = 0;
            }
			UpdateResolutionLabel();
		}

        public void UpdateResolutionLabel()
        {
			resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + "x" + resolutions[selectedResolution].vertical.ToString();
        }

		public void ApplyResolution()
        {  
			if(resolutions[selectedResolution].horizontal != 1920 && resolutions[selectedResolution].vertical != 1080)
            {
				Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, false);
			}

			else
            {
				Screen.SetResolution(resolutions[selectedResolution].horizontal = 1920, resolutions[selectedResolution].vertical = 1080, true);
            }
		}
	}
}
