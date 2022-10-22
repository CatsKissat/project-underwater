using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class CheckForRoomSpace : MonoBehaviour
    {
        [SerializeField] private LayerMask layer;
        [SerializeField] private float rayDistance = 10.0f;

        public bool CastCheckingRay()
        {
            // Cast a ray to check is there a floor layer
            Vector3 rayOrigin = new Vector3(transform.position.x, transform.position.y, -5);
            if ( Physics.Raycast(rayOrigin, transform.TransformDirection(Vector3.forward), rayDistance, layer))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
