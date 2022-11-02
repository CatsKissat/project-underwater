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

                //If gamepad is active and sensing input from stick controller aim with right controller
                if ( isGamepadActive )
                {
                    //Right stick input reading for rotation
                    var gamepad = Gamepad.current;
                    Vector3 rightStickInput = gamepad.rightStick.ReadValue();
                    if ( rightStickInput.sqrMagnitude > Mathf.Epsilon )
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