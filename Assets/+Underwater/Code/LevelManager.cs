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

        private void Awake()
        {
            InitializeInstance();
            InitializeSpawnPoint();
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

        private async Task GenerateLevel()
        {
            // Create Level GameObject for the level.
            GameObject level = new GameObject("Level");

            for ( int i = 0; i < roomCount; i++ )
            {
#if UNITY_EDITOR
                // Debug. Delay Spawning so user can see spawned rooms one by one.
                await Task.Delay(delay);
#endif

                Debug.Log("GenerateLevel: " + (i + 1) + ". " + name);

                // Set next room's Z position for Raycast check.
                spawnPoint.position += new Vector3(0, 0, -i);

                // Instantiate room from prefab
                var tempRoom = Instantiate(roomPrefab, spawnPoint.position, Quaternion.identity);

                // Add number to it's name.
                tempRoom.name += "(" + (i + 1) + ")";

                // Set room to child of the Level GameObject.
                tempRoom.transform.parent = level.transform;

                // Initialize room
                //Debug.Log((i + 1) + ". room spawned.");
                RoomManager currentRoomManager = tempRoom.GetComponent<RoomManager>();
                currentRoomManager.InitializeRoom();
                Debug.Log((i + 1) + ". room initialized completed");

                Debug.Log((i + 1) + ". Add available room spots to a List.");
                for ( int j = 0; j < currentRoomManager.GetAdjacentRoomSpotsLength(); j++ )
                {
                    adjacentRoomSlots.Add(currentRoomManager.GetAdjacentRoomSpot(j));
                    Debug.LogWarning((i + 1) + ". added: " + currentRoomManager.GetAdjacentRoomSpot(j).name); 
                }

            }
            if ( level.transform.childCount == roomCount )
            {
                Debug.Log("All rooms spawned, great job!");
            }
            else
            {
                Debug.LogError("For some reason not all room weren't instantiated!");
            }
        }

        private void InitializeSpawnPoint()
        {
            spawnPoint = transform.GetChild(0);
            if ( spawnPoint == null )
            {
                Debug.LogError(name + "'s " + nameof(roomPrefab) + " variable is null. Can't spawn rooms without of it!");
            }
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
