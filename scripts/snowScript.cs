using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowScript : MonoBehaviour
{
    [SerializeField]
    Sprite footprints;
    [SerializeField]
    Sprite plainSnow;
    SpriteRenderer sr;
    public bool[] direction = new bool[] { false, false, false, false }; //up down left right
    public bool steppedOn;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) //flips the sprite in the relevant direction
        {
            if(steppedOn)
            {
                removeSnowFromManager();
            }
            direction[0] = false;
            direction[1] = false;
            direction[2] = false;
            direction[3] = false;
            if (playerDirectionManager.instance.goingUp)
            {
                sr.flipY = false;
                direction[0] = true;
            }
            else if(playerDirectionManager.instance.goingDown)
            {
                sr.flipY = true;
                direction[1] = true;
            }

            if(playerDirectionManager.instance.goingRight)
            {
                sr.flipY = true;
                direction[2] = true;
            }
            else if (playerDirectionManager.instance.goingLeft)
            {
                sr.flipY = false;
                direction[3] = true;
            }
            sr.sprite = footprints;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //adds snow to the array which removes footprints
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            snowResetManager.instance.addSnow(gameObject, false);
            steppedOn = true;
        }
    }

    public void resetSnow() //removes the footprints
    {
        sr.sprite = plainSnow;
        direction[0] = false;
        direction[1] = false;
        direction[2] = false;
        direction[3] = false;
        sr.flipY = false;
        steppedOn = false;
    }

    public void removeSnowFromManager() //removes the snow block from the array that removes footprints
    {
        snowResetManager.instance.resetSnowPos(gameObject, false);
    }
}
