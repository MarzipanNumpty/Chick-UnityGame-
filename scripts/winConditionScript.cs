using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winConditionScript : MonoBehaviour
{
    [SerializeField]
    GameObject winScreen;

    private void OnTriggerEnter2D(Collider2D collision) //used to show the victory screen to the player
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            winScreen.SetActive(true);
            collision.gameObject.SetActive(false);
        }
    }
}

