using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverScript : MonoBehaviour
{
    [SerializeField]
    GameObject door;

    public void openDoor() //opens the door in game
    {
        door.SetActive(false);
    }
}
