using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class WeaponParent : MonoBehaviour
    {
       public Vector2 PointerPosition { get; set; }

        // Update is called once per frame
        void Update()
        {
            transform.up = (PointerPosition - (Vector2)transform.position).normalized;
        }
    }
}
