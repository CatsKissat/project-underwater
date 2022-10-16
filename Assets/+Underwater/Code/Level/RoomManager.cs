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
            // Enable List of AdjacentSpawnPoints.
            for ( int i = 0; i < adjacentRoomSpots.Count; i++ )
            {
                adjacentRoomSpots[i].SetActive(true);
            }

            // If AdjacentSpawnPoint collides with a another room, put it to a List of adjacentRooms.
            for ( int i = (adjacentRoomSpots.Count - 1); i > -1; i-- )
            {
                bool isAdjacentSpotFree = adjacentRoomSpots[i].GetComponent<CheckForRoomSpace>().CastCheckingRay();
                // TODO: Cast raycast check for all the old rooms too.
                // TODO: Set adjacentToomSpots to to adjacentRooms if there is a room.
                if ( !isAdjacentSpotFree )
                {
                    adjacentRooms.Add(adjacentRoomSpots[i]);
                    adjacentRoomSpots.RemoveAt(i);
                }
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
