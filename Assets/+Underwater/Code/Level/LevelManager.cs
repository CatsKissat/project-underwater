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
        [SerializeField] private int firstLevelRoomCount;
        [SerializeField] internal List<GameObject> adjacentRoomSlots;

#if UNITY_EDITOR
        [Header("Debug")]
        [SerializeField] private bool EnableGenerating = true;
        [SerializeField] private int delay;
#endif

        private List<GameObject> rooms = new List<GameObject>();

        private void Awake()
        {
            InitializeInstance();
        }

        private void Start()
        {
#if UNITY_EDITOR
            if ( EnableGenerating )
            {
#endif
                GenerateLevel();
                StartCoroutine(ScanStarAI());

#if UNITY_EDITOR
            }
#endif
        }

        private void GenerateLevel()
        {
            // Create Level GameObject for the level in Hierarchy.
            GameObject level = new GameObject("Level");

            // Get level and adjust roomcount
            firstLevelRoomCount += GameManager.Instance.Level;

            for ( int i = 0; i < firstLevelRoomCount; i++ )
            {
                // Instantiate room from prefab
                rooms.Add(Instantiate(roomPrefab, SetSpawnPoint(i).position, Quaternion.identity));

                // Add number to it's name.
                rooms[i].name += "(" + (i + 1) + ")";

                // Set room to child of the Level GameObject.
                rooms[i].transform.parent = level.transform;

                // Empty adjacentRoomSlots because recreating it later again
                adjacentRoomSlots.Clear();

                for ( int k = 0; k < rooms.Count; k++ )
                {
                    RoomManager currentRoomManager = rooms[k].GetComponent<RoomManager>();

                    // Initialize room
                    currentRoomManager.InitializeRoom();
                }
            }

            // Debug message for room spawning
            if ( level.transform.childCount == firstLevelRoomCount )
            {
                Debug.Log("All rooms spawned, great job!");
            }
            else
            {
                Debug.LogError("For some reason not all room weren't instantiated!");
            }

            // Randomize which room should have the exit
            int randomRoom = Random.Range(0, rooms.Count);

            // Spawn all nessecary things into the rooms.
            for ( int i = 0; i < rooms.Count; i++ )
            {
                RoomManager roomManager = rooms[i].GetComponent<RoomManager>();
                // NOTE: Disabled this for now to faster iteration
                // TODO: Enable later when generating doors and walls automatically
                roomManager.SpawnWalls();
                roomManager.SpawnRoomLayout(rooms.Count, i);
            }

            // TODO: If time, better exit room:
            // Calculate all distance from all rooms to the starting room. If distance is greatest, place exit there. If equal, randomize between those rooms.
        }

        private Transform SetSpawnPoint(int index)
        {
            Transform spawnPoint;

            // TODO: If time, get rid of if else and make one adjacentRoomSlots for the first room. Delete point after using it.
            if ( index == 0 )
            {
                spawnPoint = transform.GetChild(0);
            }
            else
            {
                int randomIndex = Random.Range(0, adjacentRoomSlots.Count);
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

        IEnumerator ScanStarAI()
        {
            AstarPath.active.Scan();
            yield return null;
        }
    }
}
