using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class Projectile : MonoBehaviour
    {
        
        [SerializeField]
        private Rigidbody2D rb2D;

        [SerializeField]
        float fireForce = 20f;

        [SerializeField]
        float lifeTime = 2;

        private void OnEnable()
        {
            rb2D.AddForce(-transform.right * fireForce, ForceMode2D.Impulse);
            Invoke(nameof(Destroy), lifeTime);
        }

        private void Destroy()
        {
            gameObject.SetActive(false);
        }
    }
}