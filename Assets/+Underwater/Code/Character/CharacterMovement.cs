using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FlamingApes.Underwater
{
    public class CharacterMovement : MonoBehaviour
    {

        //references for rotation for aiming at mouse and keyboard
        [SerializeField]
        private Camera main;

        [SerializeField]
        private bool isGamepadActive;

        [SerializeField]
        InputActionReference pointerPosition;

        private Vector2 pointerInput;
        private Vector2 input;
        private WeaponParent weaponParent;

        //player movement references

        [SerializeField]
        float movementSpeed = 1;

        private void Awake()
        {
            weaponParent = GetComponentInChildren<WeaponParent>();
        }

        // Update is called once per frame

        void Update()
        {

            //Moves character, without letting the rotation to affect movement direction
            transform.Translate(input * movementSpeed * Time.deltaTime, Space.World);

            //If gamepad is active and sensing input from stick controller aim with right controller
            if (isGamepadActive)
            {
                //Right stick input reading for rotation
                var gamepad = Gamepad.current;
                Vector3 rightStickInput = gamepad.rightStick.ReadValue();
                
                if (rightStickInput.sqrMagnitude > Mathf.Epsilon)
                {
                    //TÄHÄN TARVISI KEKSIÄ MITEN TÄHTÄYS COORDINAATIT MUUTTUISI HAHMON SIJAINNIN MUKAAN, EIKÄ VAIN SAATUJEN JOYSTICKIN KOORDINAATTIEN MUKAAN
                    weaponParent.PointerPosition = rightStickInput;
                   
                    Debug.Log("Gamepad active" + "Gamepad coordinates" + rightStickInput);
                }
            }

            //if no gamepad active use mouse to aim to mouses current position
            else
            {
                pointerInput = GetMousePointerInput();
                weaponParent.PointerPosition = pointerInput;
                Debug.Log(pointerInput);
            }
        }

        //Find the mouse point on screen
        private Vector2 GetMousePointerInput()
        {
            Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
            mousePos.z = Camera.main.nearClipPlane;
            return Camera.main.ScreenToWorldPoint(mousePos);
        } 

    //input reading for controls
        public void Move(InputAction.CallbackContext context)
        {
            input = context.ReadValue<Vector2>();
        }

        //Unity event OnDeviceChange reads the boolean if controller is equiped
        public void OnDeviceChange(PlayerInput controllerInput)
        {
            isGamepadActive = controllerInput.currentControlScheme.Equals("Gamepad") ? true : false;
        }
    }
}