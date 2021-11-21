using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowFallScript : MonoBehaviour
{
    public bool eraseSnow;
    public List<GameObject> singleSnow;
    public List<GameObject> centerSnow;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(eraseSnow) //starts the animation
        {
            eraseSnow = false;
            anim.SetBool("fall", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) //collects all snow pieces that will be affected by the snow falling
    {
        if (collision.gameObject.CompareTag("snow"))
        {
            singleSnow.Add(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("center"))
        {
            centerSnow.Add(collision.gameObject);
        }
    }

    void removeFootPrints()//removes the footprints from the snow blocks that have snowprints
    {
        if(singleSnow.Count > 0)
        {
            for (int i = 0; i < singleSnow.Count; i++)
            {
                if (singleSnow[i].GetComponent<snowScript>().steppedOn)
                {
                    singleSnow[i].GetComponent<snowScript>().removeSnowFromManager();
                    singleSnow[i].GetComponent<snowScript>().resetSnow();
                }
            }
        }

        if (centerSnow.Count > 0)
        {
            for (int i = 0; i < centerSnow.Count; i++)
            {
                if (centerSnow[i].GetComponent<snowCrossPiece>().steppedOn)
                {
                    centerSnow[i].GetComponent<snowCrossPiece>().removeSnowFromManager();
                    centerSnow[i].GetComponent<snowCrossPiece>().resetSnow();
                    int index = playerDirectionManager.instance.playerRecentPositions.IndexOf(centerSnow[i].transform);
                    if (index > 0)
                    {
                        playerDirectionManager.instance.playerRecentPositions.RemoveAt(index);
                    }
                }
            }
        }

        singleSnow.Clear();
        centerSnow.Clear();
    }
}
