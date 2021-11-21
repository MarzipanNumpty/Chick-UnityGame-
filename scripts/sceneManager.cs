using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour //this script is used for loadinglevels
{
    #region

    public static sceneManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public void loadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void loadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void loadLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void loadLevel4()
    {
        SceneManager.LoadScene("Level4");
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
