using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]

        private int damage = 1;

        [SerializeField]
        private Rigidbody2D rb2D;

        [SerializeField]
        float fireForce = 20f;

        [SerializeField]
        float lifeTime = 2;

        [SerializeField]
        private UnitSensor enemySensor;

        private void OnEnable()
        {
            rb2D.AddForce(-transform.right * fireForce, ForceMode2D.Impulse);
            Invoke(nameof(Destroy), lifeTime);
        }

        private void Destroy()
        {
            gameObject.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        { 
            if (enemySensor.IsActive)
            {
                Debug.Log($"Attacking {enemySensor.ActiveUnit}");

                if (!enemySensor.ActiveUnit.Health.DecreaseHealth(damage))
                {
                    // Enemy died
                    enemySensor.ActiveUnit.Die();
                }
            }

            gameObject.SetActive(false);
        }
    }
}