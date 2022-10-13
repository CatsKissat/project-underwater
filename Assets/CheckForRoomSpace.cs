using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class CheckForRoomSpace : MonoBehaviour
    {
        [SerializeField] private LayerMask layer;

        public bool CastCheckingRay()
        {
            //Debug.Log(transform.parent.name + ", " + name + ": is casting a check ray.");

            // Cast a ray to check is there a floor layer
            RaycastHit hit;
            if ( Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity) )
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red, 10.0f);
                //Debug.LogError(transform.parent.name + " " + name + "'s Ray hit with: " + hit);
                return false;
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.yellow, 10.0f);
                //Debug.LogWarning(transform.parent.name + " " + name + "'s Ray didn't hit");
            }

            return true;
        }
    }
}
