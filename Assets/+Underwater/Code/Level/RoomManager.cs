using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> adjacentRoomSpots;
        [SerializeField] private List<GameObject> adjacentRooms;
        private int slotCount;

        private void Awake()
        {
            foreach ( Transform child in transform )
            {
                slotCount++;
                if(transform.)
            }

            int[] spotEnabler = new int[slotCount];

            //for ( int i = 0; i < spotEnabler.Length; i++ )
            //{
            //    if ( i == (spotEnabler.Length - 1) )
            //    {
            //        spotEnabler[i] = Random.Range(0, 2);
            //    }
            //    else
            //    {
            //        spotEnabler[i] = 1;
            //    }
            //}

            for ( int i = 0; i < slotCount; i++ )
            {
                if ( i == (slotCount - 1) )
                {
                    spotEnabler[i] = Random.Range(0, 2);
                }
                else
                {
                    spotEnabler[i] = 1;
                }
            }



            foreach ( Transform child in transform )
            {
                if ( child.gameObject.activeInHierarchy )
                {
                    adjacentRoomSpots.Add(child.gameObject);
                }
            }
        }

        private void AddAdjacentSlotsToList()
        {
            foreach ( Transform child in transform )
            {
                if ( child.gameObject.activeInHierarchy )
                {
                    adjacentRoomSpots.Add(child.gameObject);
                }
            }
        }

        internal void InitializeRoom()
        {
            Debug.Log("Initializing room");

            // If AdjacentSpawnPoint's raycast collides with a another room, put it to a List of adjacentRooms.
            for ( int i = (adjacentRoomSpots.Count - 1); i > -1; i-- )
            {
                bool isAdjacentSpotFree = adjacentRoomSpots[i].GetComponent<CheckForRoomSpace>().CastCheckingRay();
                if ( !isAdjacentSpotFree )
                {
                    adjacentRooms.Add(adjacentRoomSpots[i]);
                    adjacentRoomSpots.RemoveAt(i);
                }
            }

            // Add available room slots to a list.
            for ( int i = 0; i < adjacentRoomSpots.Count; i++ )
            {
                LevelManager.Instance.adjacentRoomSlots.Add(adjacentRoomSpots[i]);
            }
        }

        //internal void AddSlotToAdjacentRooms()
        //{
        //    adjacentRooms.Add(adjacentRoomSpots[i]);
        //    adjacentRoomSpots.RemoveAt(i);
        //}

        private void SpawnWalls()
        {
            // Spawn after layout after generating a level
        }

        private void RandomizeRoomLayout()
        {
            // Spawn room layout (aka obstacles, spawn points and other predetermined this from a list) after generating walls.
        }
    }
}
