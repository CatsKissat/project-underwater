using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class RoomManager : MonoBehaviour
    {
        public void InitializeRoom()
        {
            Debug.Log(name + " Start and await");
            Task.Delay(100);
            Debug.Log(name + " Wait completed");
        }
    }
}
