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
        private Rigidbody2D rb2D;

        [SerializeField]
        private Camera main;

        [SerializeField] 
        private bool isGamepadActive;
        
        private Vector2 mousePositionWorld;
        private Vector3 mousePositionCurrent;

        //player movement references
       
        private Vector2 input;
        

        [SerializeField]
        float movementSpeed = 1;

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
                    float angleToLook = Mathf.Atan2(rightStickInput.y, rightStickInput.x) * Mathf.Rad2Deg - 90f;
                    //rb2D.rotation = angleToLook;
                    Debug.Log("Gamepad active");
                }
            }
            //if no gamepad active use mouse to aim to mouses current position
            else
            {
                mousePositionCurrent = Mouse.current.position.ReadValue();
                mousePositionWorld = main.ScreenToWorldPoint(mousePositionCurrent);

                Vector2 lookDirection = mousePositionWorld - rb2D.position;
                float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;

                //rb2D.rotation = angle;
            }
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