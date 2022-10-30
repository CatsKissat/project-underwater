using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FlamingApes.Underwater
{
    public class RoomManager : MonoBehaviour
    {
        // Generating room and adjacent rooms
        [SerializeField] private GameObject[] roomLayouts;
        [SerializeField] private List<GameObject> adjacentRoomSpots;
        [SerializeField] private List<GameObject> adjacentRooms;
        [SerializeField] private GameObject dummyFloor;
        private int slotCount;

        // Generating walls
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private Tile spotForWallTile;
        [SerializeField] private TileBase wallTile;
        [SerializeField] private Tile spotForDoorTile;
        [SerializeField] private TileBase doorTile;
        [SerializeField] BoundsInt roomArea;

        private void Awake()
        {
            //DisableRoomSpawnPoints();
        }

        private void DisableRoomSpawnPoints()
        {
            // NOTE: This is for the more spreading room spawning but there are some problems in it. I'll fix them if I have time later.
            // Get count of RoomSpawnPoints this room has.
            foreach ( Transform child in transform )
            {
                slotCount++;
            }

            int index = 0;
            foreach ( Transform child in transform )
            {
                index++;
                // Randomize is RoomSpawnPoint in use or not.
                int rngEnabler = Random.Range(0, 2);
                //Debug.Log(name + " rng: " + rngEnabler);

                if ( rngEnabler == 1 )
                {
                    Debug.Log(name + ", enabling RoomSpawnPoint: " + index);
                    adjacentRoomSpots.Add(child.gameObject);
                }
                else if ( index == slotCount && adjacentRoomSpots.Count == 0 )
                {
                    Debug.Log(name + ", No active slot, activating the last one.");
                    adjacentRoomSpots.Add(child.gameObject);
                }
                else
                {
                    // NOTE: Dummy idea is good, but if use like this it will give out of index error sometimes.
                    // TODO: If time, make a better logic to make dummys
                    // Spawn a dummy floor to take the space so the other spawning rooms think there is a room already.
                    //GameObject dummy = Instantiate(dummyFloor, child.transform.position, Quaternion.identity);
                }
            }
        }

        internal void InitializeRoom()
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
            for ( int i = 0; i < adjacentRoomSpots.Count; i++ )
            {
                LevelManager.Instance.adjacentRoomSlots.Add(adjacentRoomSpots[i]);
            }
        }

        internal void SpawnWalls()
        {
            // Check for wall spots and changes them to a wall tiles.
            ChangeCorrectTiles(spotForWallTile, wallTile);

            // TODO: Doors locations and fill them

            // Check for door spots and changes them to a door tiles.
            ChangeCorrectTiles(spotForDoorTile, doorTile);
        }

        private void ChangeCorrectTiles(Tile spotTile, TileBase TileToUse)
        {
            for ( int x = roomArea.xMin; x < roomArea.xMax; x++ )
            {
                for ( int y = roomArea.yMin; y < roomArea.yMax; y++ )
                {
                    Vector3Int intVector = new Vector3Int(x, y);
                    if ( tilemap.GetTile(intVector) == spotTile )
                    {
                        tilemap.SetTile(intVector, TileToUse);
                    }
                }
            }
        }

        internal void SpawnRoomLayout()
        {
            // Spawn room layout (aka obstacles, spawn points and other predetermined things from a list).
            int randomizedLayout = Random.Range(0, roomLayouts.Length);
            Instantiate(roomLayouts[randomizedLayout], transform.position, Quaternion.identity);
        }

        internal void SetRoomConnections()
        {
            // Set connections to adjacent rooms.
            // NOTE: This probably should happen at the same place where spawning walls.
        }

        internal void SpawnExit()
        {

        }

        internal void SpawnMonsters()
        {

        }

        internal void SpawnTreasures()
        {

        }
    }
}
