using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FlamingApes.Underwater
{
    public class PlayerAttack : MonoBehaviour
    {
        public static PlayerAttack Instance { get; private set; }

        //shooting related references solely for PlayerCharacter
        [SerializeField]
        private GameObject projectile;

        //firePoint is empty gameobject, which is placed in parent object and works as point of bullets origin
        [SerializeField]
        private Transform firePoint;

        //boolean for shooting cooldown and amount cooldown is active
        private bool canAttack = true;

        [SerializeField]
        float shootingCooldown = 1f;

        private ObjectPool objectPool;

        public bool CanAttack { set => canAttack = value; }

        private void Awake()
        {
            if ( Instance != null )
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            CanAttack = true;
        }

        private void Start()
        {
            objectPool = GetComponent<ObjectPool>();
        }

        //After CanShoot() numerator sends positive boolean, Shoot() calls bullet pool to set prefab active / visible 
        public void Shoot(InputAction.CallbackContext context)
        {
            if ( !canAttack ) return;
            //fetch pooled game object -> the projectile in this case
            GameObject projectile = objectPool.GetPooledObject();
            if ( projectile != null )
            {
                projectile.transform.position = firePoint.transform.position;
                projectile.transform.rotation = firePoint.transform.rotation;
                projectile.GetComponent<Collider2D>().enabled = true;
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
