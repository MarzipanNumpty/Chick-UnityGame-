using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowCrossPiece : MonoBehaviour
{
    [SerializeField]
    Sprite footprints;
    [SerializeField]
    Sprite plainSnow;
    [SerializeField]
    Sprite straightPrints;
    SpriteRenderer sr;
    public bool[] direction = new bool[] { false, false, false, false }; //up down right left
    float rot;
    public bool steppedOn;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (steppedOn) //resets countdown before footprints disappear
            {
                removeSnowFromManager();
            }
            if (playerDirectionManager.instance.goingUp) //sets the rotation of the sprite
            {
                rot = 270;
            }
            else if (playerDirectionManager.instance.goingDown)
            {
                rot = 90;
            }

            if (playerDirectionManager.instance.goingRight)
            {
                rot = 180;
            }
            else if (playerDirectionManager.instance.goingLeft)
            {
                rot = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            direction[0] = false;
            direction[1] = false;
            direction[2] = false;
            direction[3] = false;
            if (playerDirectionManager.instance.goingUp)  //rotates the sprite in the relevant direction
            {
                direction[0] = true;
                if (rot == 0)
                {
                    flipAxis(false, false);
                    sr.sprite = footprints;
                }
                else if (rot == 90)
                {
                    flipAxis(true, false);
                    sr.sprite = straightPrints;
                }
                else if (rot == 180)
                {
                    flipAxis(false, true);
                    sr.sprite = footprints;
                }
                else if (rot == 270)
                {
                    flipAxis(true, false);
                    sr.sprite = straightPrints;
                }
            }
            else if (playerDirectionManager.instance.goingDown)
            {
                direction[1] = true;
                if (rot == 0)
                {
                    flipAxis(false, true);
                    sr.sprite = footprints;
                }
                else if (rot == 90)
                {
                    flipAxis(true, false);
                    sr.sprite = straightPrints;
                }
                else if (rot == 180)
                {
                    flipAxis(false, false);
                    sr.sprite = footprints;
                }
                else if (rot == 270)
                {
                    flipAxis(true, false);
                    sr.sprite = straightPrints;
                }
            }

            if (playerDirectionManager.instance.goingRight)
            {
                direction[2] = true;
                if (rot == 0)
                {
                    flipAxis(true, false);
                    sr.sprite = straightPrints;
                }
                else if (rot == 90)
                {
                    flipAxis(false, true);
                    sr.sprite = footprints;
                }
                else if (rot == 180)
                {
                    flipAxis(true, false);
                    sr.sprite = straightPrints;
                }
                else if (rot == 270)
                {
                    flipAxis(false, false);
                    sr.sprite = footprints;
                }
            }
            else if (playerDirectionManager.instance.goingLeft)
            {
                direction[3] = true;
                if (rot == 0)
                {
                    flipAxis(true, false);
                    sr.sprite = straightPrints;
                }
                else if (rot == 90)
                {
                    flipAxis(false, false);
                    sr.sprite = footprints;
                }
                else if (rot == 180)
                {
                    flipAxis(true, false);
                    sr.sprite = straightPrints;
                }
                else if (rot == 270)
                {
                    flipAxis(false, true);
                    sr.sprite = footprints;
                }
            }
            snowResetManager.instance.addSnow(gameObject, true); //adds snow to an array that will remove footprints after a preiod of time
            transform.localRotation = Quaternion.Euler(0, 0, rot);
            if(playerDirectionManager.instance.playerSeen) //adds the transform to an array telling the enemy the player's taken path
            {
                playerDirectionManager.instance.AddTransform(transform);
            }
            steppedOn = true;
        }
    }

    void flipAxis(bool x, bool y) //flips sprite
    {
        sr.flipX = x;
        sr.flipY = y;
    }

    public void resetSnow() //removes footprints from snow and makes sprite plain white snow
    {
        sr.sprite = plainSnow;
        direction[0] = false;
        direction[1] = false;
        direction[2] = false;
        direction[3] = false;
        flipAxis(false, false);
        steppedOn = false;
    }

    public void removeSnowFromManager() //puts this snow block at the end of the array which resets footprints
    {
        snowResetManager.instance.resetSnowPos(gameObject, true);
    }
}
