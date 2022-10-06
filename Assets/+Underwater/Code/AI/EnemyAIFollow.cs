using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class EnemyAIFollow : MonoBehaviour

    {
        [SerializeField]
        public float movementSpeed;
        [SerializeField]
        public Transform target;
        [SerializeField]
        // Desired distance between the enemy and target object
        public float targetDistance;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Vector2.Distance(transform.position, target.position) > targetDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
            }
                
        }
    }
}
