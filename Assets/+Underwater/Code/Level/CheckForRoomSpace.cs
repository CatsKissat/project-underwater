using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class CheckForRoomSpace : MonoBehaviour
    {
        [SerializeField] private LayerMask layer;
        [SerializeField] private float rayDistance = 10.0f;
        private float rayDuration = 0.1f;

        public bool CastCheckingRay()
        {
            // Cast a ray to check is there a floor layer
            if ( Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), rayDistance, layer) ||
                 Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), rayDistance, layer) )
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayDistance, Color.red, rayDuration);
                Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.forward) * rayDistance, Color.black, rayDuration);
                return false;
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayDistance, Color.yellow, rayDuration);
                Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.forward) * rayDistance, Color.green, rayDuration);
                return true;
            }
        }
    }
}
