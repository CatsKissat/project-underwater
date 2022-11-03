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
        private InputActionReference pointerPosition;

        private float directionInput;
        private Vector2 pointerInput;
        private Vector2 input;
        private WeaponParent weaponParent;

        //player movement references

        // Animator
        private Animator animator;
        private string xSpawnParam = "spawn";
        private string xAxisParam = "x";
        private string yAxisParam = "y";
        private string canMoveParam = "canMove";
        private string isRunningParam = "isRunning";
        private string runLeftParam = "runLeft";
        private string runDiagonalRightParam = "runDiagonalRight";
        private SpriteRenderer spriteRender;

        [SerializeField]
        float movementSpeed = 1;

        private void Awake()
        {
            weaponParent = GetComponentInChildren<WeaponParent>();
        }

        // Update is called once per frame

        private void Start()
        {
            animator = GetComponent<Animator>();
            if ( animator == null )
            {
                Debug.LogError(name + " is missing an Animator reference!");
            }
            spriteRender = GetComponent<SpriteRenderer>();
            if ( spriteRender == null )
            {
                Debug.LogError(name + " is missing a SpriteRenderer reference!");
            }
        }
            

        void Update()
        {
            if ( animator.GetCurrentAnimatorStateInfo(0).IsName(xSpawnParam) )
            {
                animator.SetBool(canMoveParam, true);
                
            }

            if ( animator.GetBool(canMoveParam) )
            {
                //Moves character, without letting the rotation to affect movement direction
                transform.Translate(input * movementSpeed * Time.deltaTime, Space.World);
                
                if (isGamepadActive)
                {
                    //Right stick input reading for rotation
                    var gamepad = Gamepad.current;
                    Vector3 rightStickInput = gamepad.rightStick.ReadValue();
                
                    if (rightStickInput.sqrMagnitude > Mathf.Epsilon)
                    {                 
                        directionInput = GetGamepadDirection();
                        weaponParent.PointerDirection = directionInput;
                    }
                }
                //if no gamepad active use mouse to aim to mouses current position
                else
                {
                    pointerInput = GetMousePointerInput();
                    weaponParent.PointerPosition = pointerInput;
                }
            }

        }

        private float GetGamepadDirection()
        {
            var gamepad = Gamepad.current;
            Vector3 rightStickInput = gamepad.rightStick.ReadValue();
            float angleToLook = Mathf.Atan2(rightStickInput.y, rightStickInput.x) * Mathf.Rad2Deg - 90f;
            return angleToLook;
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

            // Animations
            if ( input.magnitude > 0.1 )
            {
                if ( input.x < -0.1 && input.y < 0.1 )
                {
                    spriteRender.flipX = false;
                }
                if ( input.x > 0.1f && input.y < 0.1 )
                {
                    spriteRender.flipX = true;
                }
                if ( input.x < -0.1 && input.y > 0.1 )
                {
                    spriteRender.flipX = true;
                }
                if ( input.x > 0.1f && input.y > 0.1 )
                {
                    spriteRender.flipX = false;
                }
                animator.SetFloat(xAxisParam, input.x);
                animator.SetBool(isRunningParam, true);
                animator.SetFloat(yAxisParam, input.y);
            }
            else
            {
                animator.SetBool(isRunningParam, false);
            }
        }

        //Unity event OnDeviceChange reads the boolean if controller is equiped
        public void OnDeviceChange(PlayerInput controllerInput)
        {
            isGamepadActive = controllerInput.currentControlScheme.Equals("Gamepad") ? true : false;
        }
    }
}