using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDirectionManager : MonoBehaviour
{
    #region

    public static playerDirectionManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion //makes it referencable from all scripts

    public bool goingRight;
    public bool goingLeft;
    public bool goingUp;
    public bool goingDown;
    public List<Transform> playerRecentPositions;
    public bool playerSeen;
    public int listLength;

    public void AddTransform(Transform pos) //used for the enemy to follow the player with the use of an array holding recent positions
    {
        listLength = playerRecentPositions.Count;
        if(listLength > 0)
        {
            if (playerRecentPositions[listLength - 1] != pos)
            {
                playerRecentPositions.Add(pos);
            }
        }
        else
        {
            playerRecentPositions.Add(pos);
        }
    }
}
