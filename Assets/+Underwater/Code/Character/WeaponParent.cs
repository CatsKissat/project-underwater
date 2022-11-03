using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace FlamingApes.Underwater
{
    public class WeaponParent : MonoBehaviour
    {
        public Vector2 PointerPosition { get; set; }
        public float PointerDirection { get; set; }

        [SerializeField]
        private bool isGamepadActive;

        // Update is called once per frame
        void Update()
        {
            if (isGamepadActive)
            {
                transform.rotation = Quaternion.Euler(0 ,0 ,PointerDirection);
                Debug.Log("IM LOOKING AT THIS" + PointerDirection);
            } 
            
            else
            {
                transform.up = (PointerPosition - (Vector2)transform.position).normalized;
            }
            
        }

        public void OnDeviceChange(PlayerInput controllerInput)
        {
            isGamepadActive = controllerInput.currentControlScheme.Equals("Gamepad") ? true : false;
        }
    }
}
