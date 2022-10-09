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

                // Instantiate room from prefab
                var tempRoom = Instantiate(roomPrefab, spawnPoint.position, Quaternion.identity);

                // Add number to it's name.
                tempRoom.name += "(" + (i + 1) + ")";

                // Set room to child of the Level GameObject.
                tempRoom.transform.parent = level.transform;

                // Initialize room
                Debug.Log(i + " starting to initialize");
                tempRoom.GetComponent<RoomManager>().InitializeRoom();
                Debug.Log(i + " room initialized");
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
