using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> adjacentRoomSpots;
        [SerializeField] private List<GameObject> adjacentRooms;

        public void InitializeRoom()
        {
            Debug.Log(name + " initializing");

            // Check is another room under this room
            // If there is a room under this one, despawn

            // Enable List of AdjacentSpawnPoints
            for ( int i = 0; i < adjacentRoomSpots.Count; i++ )
            {
                adjacentRoomSpots[i].SetActive(true);
                Debug.LogWarning("Setting: " + adjacentRoomSpots[i] + " active");
                //Debug.Log(name + " Spawned " + adjacentRoomSpots[i]);

                // If AdjacentSpawnPoint collides with a another room, put it to a List of AdjacentRooms
                //bool isAdjacentSlotFree = adjacentRoomSpots[i].GetComponent<CheckForRoomSpace>().OnTriggerEnter2D();
                bool isAdjacentSpotFree = adjacentRoomSpots[i].GetComponent<CheckForRoomSpace>().CastCheckingRay();
                if ( !isAdjacentSpotFree )
                {
                    Debug.LogWarning("Adding: " + i + ": " + adjacentRoomSpots[i] + " to the " + nameof(adjacentRooms));
                    adjacentRooms.Add(adjacentRoomSpots[i]);
                    Debug.LogError("Removing: " + i + ": " + adjacentRoomSpots[i] + " from the " + nameof(adjacentRoomSpots));
                    adjacentRoomSpots.RemoveAt(i);
                }
            }

            // TODO: Move adjacentRoomSpots to adjacentRooms List if there is a ray hit.

            for ( int k = 0; k < adjacentRoomSpots.Count; k++ )
            {
                Debug.LogError("RoomManager: " + transform.parent.name + ", " + name + ", " + adjacentRoomSpots[k].name);
            }

            Debug.Log(name + ": All " + nameof(adjacentRoomSpots) + " initialized!");
        }

        public int GetAdjacentRoomSpotsLength()
        {
            return adjacentRoomSpots.Count;
        }

        public GameObject GetAdjacentRoomSpot(int index)
        {
            Debug.Log(transform.parent + ", " + name + " lenght: " + adjacentRoomSpots.Count);
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
