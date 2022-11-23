using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FlamingApes.Underwater
{
    public class HeartUI : MonoBehaviour
    {
        private enum HeartState
        {
            Empty = 0,
            Half = 1,
            Full = 2
        }

        [SerializeField] private Sprite fullHeart;
        [SerializeField] private Sprite halfHeart;
        [SerializeField] private Sprite emptyHeart;

        private Image heartImage;

        private void Awake()
        {
            heartImage = GetComponent<Image>();
        }

        private void UpdateHeartState(HeartState state)
        {
            switch ( state )
            {
                case HeartState.Empty:
                    heartImage.sprite = emptyHeart;
                    break;
                case HeartState.Half:
                    heartImage.sprite = halfHeart;
                    break;
                case HeartState.Full:
                    heartImage.sprite = fullHeart;
                    break;
            }
        }
    }
}
