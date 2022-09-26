using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UTS
{
    public class CharacterMovement : MonoBehaviour
    {

        //references for rotation for aiming at mouse
        public new Rigidbody2D rigidbody2D;
        public Camera main;
        Vector2 mousePosWorld;
        Vector3 mousePosCurrent;

        //player movement references
        private Vector2 input;

        [SerializeField]
        float speed = 1;

        // Update is called once per frame
        //Character moving with new unity input system, Space.World is for letting the rotation to affect movement direction
        void Update()
        {  
            transform.Translate(input * speed * Time.deltaTime, Space.World);
        }

        public void Move(InputAction.CallbackContext context)
        {
            input = context.ReadValue<Vector2>();
        }

        //rotating the character towards the point of the mouse in the world by rotating characters rigidbody2D
        private void FixedUpdate()
        {
            mousePosCurrent = Mouse.current.position.ReadValue();
            mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosCurrent);
            Vector2 lookDir = mousePosWorld - rigidbody2D.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            rigidbody2D.rotation = angle;
        }
    }
}
