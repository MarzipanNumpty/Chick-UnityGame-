using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowLeverScript : MonoBehaviour
{
    [SerializeField]
    GameObject snowFall;
    public void dropSnow() //lever used for dropping the snow
    {
        snowFall.GetComponent<snowFallScript>().eraseSnow = true;
    }
}
