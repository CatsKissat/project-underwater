using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }

        [SerializeField] private GameObject roomPrefab;
        [SerializeField] private int roomCount;
        [SerializeField] private List<GameObject> adjacentRoomSlots;

#if UNITY_EDITOR
        [Header("Debug")]
        [SerializeField] private bool enableGenerating;
        [SerializeField] private int delay;
#endif

        private Transform spawnPoint;
        private List<GameObject> rooms = new List<GameObject>();

        private void Awake()
        {
            InitializeInstance();
            //InitializeSpawnPoint();
        }
        private async void Start()
        {
#if UNITY_EDITOR
            if ( enableGenerating )
            {
#endif
                await GenerateLevel();
#if UNITY_EDITOR
            }
#endif
        }

        // TODO: Remove async when level generating 
        private async Task GenerateLevel()
        {
            // Create Level GameObject for the level in Hierarchy.
            GameObject level = new GameObject("Level");

            for ( int i = 0; i < roomCount; i++ )
            {
#if UNITY_EDITOR
                // Add delay to see floor by floor level spawning.
                await Task.Delay(delay);
#endif

                // Instantiate room from prefab
                rooms.Add(Instantiate(roomPrefab, SetSpawnPoint(i).position, Quaternion.identity));

                // Add number to it's name.
                rooms[i].name += "(" + (i + 1) + ")";

                // Set room to child of the Level GameObject.
                rooms[i].transform.parent = level.transform;

                // TODO: Check is there space in adjacentRoomSlots or not

                RoomManager currentRoomManager = rooms[i].GetComponent<RoomManager>();

                // Initialize room
                currentRoomManager.InitializeRoom();
                currentRoomManager.InitializeRoom();

                // Add available room slots to a list.
                for ( int j = 0; j < currentRoomManager.GetAdjacentRoomSpotsLength(); j++ )
                {
                    adjacentRoomSlots.Add(currentRoomManager.GetAdjacentRoomSpot(j));
                }
            }

            // Debug message for room spawning
            if ( level.transform.childCount == roomCount )
            {
                Debug.Log("All rooms spawned, great job!");
            }
            else
            {
                Debug.LogError("For some reason not all room weren't instantiated!");
            }

            for ( int i = 0; i < rooms.Count; i++ )
            {
                // Set room's Z axis to zero. Not necessary, but looks better when viewing the level in 3D in editor :)
                rooms[i].transform.position += new Vector3(0.0f, 0.0f, i);
            }
        }

        private Transform SetSpawnPoint(int index)
        {
            // TODO: Get rid of if else and make one adjacentRoomSlots for the first room. Delete point after using it.
            if ( index == 0 )
            {
                spawnPoint = transform.GetChild(0);
            }
            else
            {
                int randomIndex = (int)Random.Range(0, adjacentRoomSlots.Count);
                spawnPoint = adjacentRoomSlots[randomIndex].transform;
            }
            if ( spawnPoint == null )
            {
                Debug.LogError(name + "'s " + nameof(roomPrefab) + " variable is null. Can't spawn rooms without of it!");
            }

            // Set next room's Z position for Raycast check.
            spawnPoint.position += new Vector3(0, 0, -1);

            return spawnPoint;
        }

        //private void InitializeSpawnPoint()
        //{
        //    spawnPoint = transform.GetChild(0);
        //    if ( spawnPoint == null )
        //    {
        //        Debug.LogError(name + "'s " + nameof(roomPrefab) + " variable is null. Can't spawn rooms without of it!");
        //    }
        //}

        private void InitializeInstance()
        {
            if ( Instance != null )
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
            }
        }
    }
}
