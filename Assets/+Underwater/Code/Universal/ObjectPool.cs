using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private List<GameObject> pooledObjects;
        [SerializeField] private GameObject objectToPool;
        [SerializeField] private int amountToPool;

        public void SetPoolObject(GameObject poolObject)
        {
            objectToPool = poolObject;
        }

        void Start()
        {
            pooledObjects = new List<GameObject>();
            GameObject tmp;
            for ( int i = 0; i < amountToPool; i++ )
            {
                tmp = Instantiate(objectToPool);
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }
        }

        public GameObject GetPooledObject()
        {
            for ( int i = 0; i < amountToPool; i++ )
            {
                if ( !pooledObjects[i].activeInHierarchy )
                {
                    return pooledObjects[i];
                }
            }
            return null;
        }
    }
}
