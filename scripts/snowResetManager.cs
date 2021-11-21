using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowResetManager : MonoBehaviour
{
    public List<GameObject> steppedOnSnow;
    public List<float> steppedOnTime;
    public float timer;
    public List<GameObject> steppedOnCrossPiece;
    public List<float> steppedOnTimeCrossPiece;

    #region

    public static snowResetManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    private void Update()
    {
        timer += Time.deltaTime;
        if (steppedOnSnow.Count > 0) //checks how long the first gameobject in the array has been in the array. If it has been ten seconds it removes the footprints and removes it from the array
        {
            float timeDiff = steppedOnTime[0] - timer;
            //Debug.Log(timeDiff);
            if(timeDiff <  -10)
            {
                steppedOnSnow[0].GetComponent<snowScript>().resetSnow();
                steppedOnSnow.RemoveAt(0);
                steppedOnTime.RemoveAt(0);
            }
        }

        if(steppedOnCrossPiece.Count > 0)//used in the same way as the above if statement but focusses on the crossroad snow blocks rather than the singular snowblocks
        {
            float timeDiffCrossPiece = steppedOnTimeCrossPiece[0] - timer;
            if (timeDiffCrossPiece < -10)
            {
                steppedOnCrossPiece[0].GetComponent<snowCrossPiece>().resetSnow();
                steppedOnCrossPiece.RemoveAt(0);
                steppedOnTimeCrossPiece.RemoveAt(0);
            }
        }
    }

    public void addSnow(GameObject snow, bool crosspiece) //adds a reference of the snow block to the relevant array and adds the time it was added to the array
    {
        if(crosspiece)
        {
            steppedOnCrossPiece.Add(snow);
            steppedOnTimeCrossPiece.Add(timer);
        }
        else
        {
            steppedOnSnow.Add(snow);
            steppedOnTime.Add(timer);
        }
    }

    public void resetSnowPos(GameObject snow, bool crosspiece) //removes snow block from the relevant array
    {
        if(crosspiece)
        {
            int pos = steppedOnCrossPiece.IndexOf(snow);
            steppedOnCrossPiece.RemoveAt(pos);
            steppedOnTimeCrossPiece.RemoveAt(pos);
        }
        else
        {
            int pos = steppedOnSnow.IndexOf(snow);
            steppedOnSnow.RemoveAt(pos);
            steppedOnTime.RemoveAt(pos);
        }
    }
}
