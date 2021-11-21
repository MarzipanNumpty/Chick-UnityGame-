using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuScript : MonoBehaviour
{
    [SerializeField]
    GameObject menu;
    [SerializeField]
    GameObject levelSelect;
    public void openLevelSelect() //shows the level select screen
    {
        menu.SetActive(false);
        levelSelect.SetActive(true);
    }

    public void openMenu() //hows main menu screen
    {
        menu.SetActive(true);
        levelSelect.SetActive(false);
    }
}
