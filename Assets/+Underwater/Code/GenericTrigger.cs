using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class GenericTrigger : MonoBehaviour
    {
        [SerializeField] private LayerMask layer;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ( ((1 << collision.gameObject.layer) & layer) != 0 )
            {
                Debug.Log(name + " triggered with " + collision.gameObject.name);
                //Destroy(gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {

        }

        private void OnTriggerStay2D(Collider2D collision)
        {
        }
    }
}
