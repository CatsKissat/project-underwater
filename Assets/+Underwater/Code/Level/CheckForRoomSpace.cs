using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class CheckForRoomSpace : MonoBehaviour
    {
        [SerializeField] private LayerMask layer;
        [SerializeField] private float rayDistance = 500.0f;
        private float rayDuration = 1.1f;

        public bool CastCheckingRay()
        {
            Vector3 rayOrigin = new Vector3(transform.position.x, transform.position.y, -5);
            // Cast a ray to check is there a floor layer
            if ( Physics.Raycast(rayOrigin, transform.TransformDirection(Vector3.forward), rayDistance, layer))
            {
                Debug.DrawRay(rayOrigin, transform.TransformDirection(Vector3.forward) * rayDistance, Color.red, rayDuration);
                Debug.LogError(transform.parent.name + ", " + name + ": hit detected");
                return false;
            }
            else
            {
                Debug.DrawRay(rayOrigin, transform.TransformDirection(Vector3.forward) * rayDistance, Color.green, rayDuration);
                Debug.LogWarning(transform.parent.name + ", " + name + ": no hit");
                return true;
            }
        }
    }
}
