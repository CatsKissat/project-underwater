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
        private List<GameObject> rooms;

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

        private async Task GenerateLevel()
        {
            // Create Level GameObject for the level.
            GameObject level = new GameObject("Level");

            for ( int i = 0; i < roomCount; i++ )
            {
#if UNITY_EDITOR
                await Task.Delay(delay);
#endif

                // TODO: Use rooms[i] instead of tempRoom.

                // Instantiate room from prefab
                //GameObject tempRoom = Instantiate(roomPrefab, spawnPoint.position, Quaternion.identity);
                GameObject tempRoom = Instantiate(roomPrefab, SetSpawnPoint(i).position, Quaternion.identity);

                // Add number to it's name.
                tempRoom.name += "(" + (i + 1) + ")";

                // Set room to child of the Level GameObject.
                tempRoom.transform.parent = level.transform;

                // Initialize room
                RoomManager currentRoomManager = tempRoom.GetComponent<RoomManager>();
                currentRoomManager.InitializeRoom();

                for ( int j = 0; j < currentRoomManager.GetAdjacentRoomSpotsLength(); j++ )
                {
                    adjacentRoomSlots.Add(currentRoomManager.GetAdjacentRoomSpot(j));
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

            // TODO: Use rooms[i] instead of roomCount.
            for ( int i = 0; i < roomCount; i++ )
            {
                // Set room's Z axis to zero. Not necessary, but looks better when viewing the level in 3D in editor :)
                //tempRoom.transform.position = new Vector3(tempRoom.transform.position.x, tempRoom.transform.position.y, 0.0f);
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
