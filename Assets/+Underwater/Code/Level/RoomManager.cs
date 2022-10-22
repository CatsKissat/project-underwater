using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> adjacentRoomSpots;
        [SerializeField] private List<GameObject> adjacentRooms;

        public void InitializeRoom()
        {
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
            for ( int i = 0; i < adjacentRoomSpots.Count ; i++ )
            {
                LevelManager.Instance.adjacentRoomSlots.Add(adjacentRoomSpots[i]);
            }
        }

        public int GetAdjacentRoomSpotsLength()
        {
            return adjacentRoomSpots.Count;
        }

        public GameObject GetAdjacentRoomSpot(int index)
        {
            return adjacentRoomSpots[index];
        }

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
