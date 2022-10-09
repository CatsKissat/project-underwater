using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class Projectile : MonoBehaviour
    {

        public GameObject bullet;
        public new Rigidbody2D rigidbody2D;

        [SerializeField]
        float fireForce = 20;

        [SerializeField]
        float lifeTime = 2;


        // Start is called before the first frame update
        void FixedUpdate()
        {
            bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * (fireForce * Time.deltaTime), ForceMode2D.Impulse);
        }

        private void OnEnable()
        {
            Invoke("Destroy", lifeTime);
        }

        private void Destroy()
        {
            gameObject.SetActive(false);
        }
    }
}