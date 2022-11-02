using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class SimpleCameraFollower : MonoBehaviour
    {
        private Transform target;

        private void Awake()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void LateUpdate()
        {
            float zOffset = gameObject.transform.position.z;

            transform.position = new Vector3(target.position.x, target.position.y, zOffset);
        }
    }
}
