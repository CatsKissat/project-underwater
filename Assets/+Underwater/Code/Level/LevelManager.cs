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
        [SerializeField] internal List<GameObject> adjacentRoomSlots;

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
        private void Start()
        {
#if UNITY_EDITOR
            if ( enableGenerating )
            {
#endif
                GenerateLevel();
#if UNITY_EDITOR
            }
#endif
        }

        // TODO: Remove async when level generating 
        private async void GenerateLevel()
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

                // Empty adjacentRoomSlots because recreating it later again
                adjacentRoomSlots.Clear();

                RoomManager roomManager = rooms[i].GetComponent<RoomManager>();
                roomManager.AddAdjacentSlotsToList();

                for ( int k = 0; k < rooms.Count; k++ )
                {
                    RoomManager currentRoomManager = rooms[k].GetComponent<RoomManager>();

                    // Initialize room
                    currentRoomManager.InitializeRoom();
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
            return spawnPoint;
        }

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
