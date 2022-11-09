using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FlamingApes.Underwater
{
    public class PlayerAttack : MonoBehaviour
    {
        //shooting related references solely for PlayerCharacter
        [SerializeField]
        private GameObject projectile;

        //firePoint is empty gameobject, which is placed in parent object and works as point of bullets origin
        [SerializeField]
        private Transform firePoint;

        //boolean for shooting cooldown and amount cooldown is active
        private bool canAttack = true;

        [SerializeField]
        private UnitSensor enemySensor;

        //[SerializeField]
        //private Transform hand;

        [SerializeField]
        float shootingCooldown = 1f;


        //After CanShoot() numerator sends positive boolean, Shoot() calls bullet pool to set prefab active / visible 
        public void Shoot(InputAction.CallbackContext context)
        {
            if (!canAttack) return;
            //fetch pooled game object -> the projectile in this case
            GameObject projectile = ObjectPool.SharedInstance.GetPooledObject();
            if (projectile != null)
            {
                projectile.transform.position = firePoint.transform.position;
                projectile.transform.rotation = firePoint.transform.rotation;
                projectile.SetActive(true);
            }

            StartCoroutine(CanShoot());

        }

        //cooldown for shooting 
        IEnumerator CanShoot()
        {
            canAttack = false;
            yield return new WaitForSeconds(shootingCooldown);
            canAttack = true;
        }
    }
}
