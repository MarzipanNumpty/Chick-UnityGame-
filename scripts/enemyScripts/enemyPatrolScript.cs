using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrolScript : MonoBehaviour
{
    [SerializeField]
    Transform[] patrolPoints;
    int nextPos;
    [SerializeField]
    float waitTime;
    float actualWaitTime;
    float timer;
    bool atLocation;
    [SerializeField]
    float speed;
    float actualSpeed;
    [SerializeField]
    bool dontLoopPositions;
    bool goBack;
    float rotation;
    public bool foundPrints;
    public bool findNextPrints;
    [SerializeField]
    LayerMask raycastLayer;
    public Transform nextDestination;
    bool[] direction = new bool[] { false, false, false, false }; //up down left right
    int[] rotNum = new int[] { 90, 270, 0, 180 }; //up down left right
    GameObject previousCrossPiece;
    public bool playerSeen;
    [SerializeField]
    GameObject player;
    bool stop;
    SpriteRenderer sprite;
    [SerializeField]
    GameObject deathScreen;
    float resetTimer;
    bool reset;
    Transform rotateNextDest;
    [SerializeField]
    int rotateDoorClosestPos;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        nextPos = 1;
        nextDestination = patrolPoints[nextPos];
        actualSpeed = speed;
        actualWaitTime = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(!stop)
        {
            if (playerSeen) //changes the speed of the enmy depending on what state it is in
            {
                actualSpeed = speed * 2.5f;
                actualWaitTime = waitTime / 4;
            }
            else if (foundPrints)
            {
                actualSpeed = speed * 1.5f;
                actualWaitTime = waitTime / 2;
            }
            else
            {
                actualSpeed = speed;
                actualWaitTime = waitTime;
            }

            if (!atLocation) //moves the enemy
            {
                transform.position = Vector2.MoveTowards(transform.position, nextDestination.position, actualSpeed * Time.deltaTime);
            }

            if (nextDestination == player.transform && playerDirectionManager.instance.playerRecentPositions.Count > 0) //if the enemy is moving towards the players transform and a position appears in the array go towards that position instead of the player
            {
                nextDestination = playerDirectionManager.instance.playerRecentPositions[0];
            }

            if (Vector2.Distance(transform.position, nextDestination.position) < 0.05 && !atLocation) //check if the enemy is close enough to the relevant destination
            {
                timer = actualWaitTime;
                atLocation = true;
                if (playerSeen) //checks if there is a position in the array that the enemy can aim for. If there isnt just go straight for the player.
                {
                    float x = Mathf.Abs(player.transform.localPosition.x - transform.localPosition.x);
                    float y = Mathf.Abs(player.transform.localPosition.y - transform.localPosition.y);
                    if (playerDirectionManager.instance.playerRecentPositions.Count > 0)
                    {
                        nextDestination = playerDirectionManager.instance.playerRecentPositions[0];
                        playerDirectionManager.instance.playerRecentPositions.RemoveAt(0);

                    }
                    else if (x < 0.2 || y < 0.2)
                    {
                        nextDestination = player.transform;
                    }
                    else
                    {
                        playerSeen = false;
                    }
                    rotateEnemy(transform, nextDestination);
                }
                else if (!foundPrints)//this gets the next position from the predefined patrol points that is given to the enemy so the enemy can be rotated appropriately
                {
                    int nextPosNum = nextPos;
                    if (!dontLoopPositions)
                    {
                        nextPosNum++;
                        if (nextPosNum >= patrolPoints.Length)
                        {
                            nextPosNum = 0;
                        }
                    }
                    else
                    {
                        if (goBack)
                        {
                            nextPosNum--;
                        }
                        else
                        {
                            nextPosNum++;
                        }
                        if (nextPosNum >= patrolPoints.Length)
                        {
                            nextPosNum = patrolPoints.Length - 2;
                        }
                        if (nextPosNum < 0)
                        {
                            nextPosNum = 1;
                        }
                    }
                    rotateEnemy(patrolPoints[nextPos], patrolPoints[nextPosNum]);
                }
                else //rotates enemy
                {
                    transform.localRotation = Quaternion.Euler(0, 0, rotation);
                }
            }

            if (timer > 0 && atLocation) //timer counts down
            {
                timer -= Time.deltaTime;
            }
            else if (timer <= 0 && atLocation)
            {
                if (!playerSeen)
                {
                    if (!foundPrints) //this gets the next destination for the enemy from the predefined patrol points
                    {
                        if (!dontLoopPositions)
                        {
                            if (nextPos == patrolPoints.Length - 1)
                            {
                                nextPos = 0;
                            }
                            else
                            {
                                nextPos++;
                            }
                        }
                        else if (dontLoopPositions)
                        {
                            if (nextPos == patrolPoints.Length - 1)
                            {
                                goBack = true;
                            }

                            if (nextPos == 0)
                            {
                                goBack = false;
                            }

                            if (goBack)
                            {
                                nextPos--;
                            }
                            else
                            {
                                nextPos++;
                            }
                        }
                        nextDestination = patrolPoints[nextPos];
                    }
                    else
                    {
                        findNextPrints = true;
                    }
                }
                atLocation = false;
            }


            if (!playerSeen)
            {
                if (!foundPrints || findNextPrints)
                {
                    Vector3 dir = new Vector3(0, 0, 0);
                    if (direction[0] || direction[1]) //changes the direction of the raycassxt depending on the enemies rotation
                    {
                        dir = transform.InverseTransformDirection(Vector2.left);
                        Debug.DrawRay(transform.position, transform.InverseTransformDirection(Vector2.left), Color.red);
                    }
                    else
                    {
                        dir = transform.InverseTransformDirection(Vector2.right);
                        Debug.DrawRay(transform.position, transform.InverseTransformDirection(Vector2.right), Color.green);
                    }
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, Mathf.Infinity, raycastLayer);
                    RaycastHit2D[] hit2 = Physics2D.RaycastAll(transform.position, dir, Mathf.Infinity, raycastLayer);

                    for (int i = 0; i < hit2.Length; i++) //checks if the enemy can see the player
                    {
                        if (hit2[i].transform.gameObject.CompareTag("Player"))
                        {
                            playerDirectionManager.instance.playerSeen = true;
                            playerSeen = true;
                            Debug.Log("Playerseen");
                        }
                    }

                    /* for(int i = 0; i < hit2.Length; i++)
                     {
                         Debug.Log(hit2[i].transform.gameObject.name);
                     }*/
                    if (hit.collider != null && hit.collider.gameObject != previousCrossPiece && !playerSeen)
                    {
                        if (hit.transform.gameObject.CompareTag("center")) //if the enemy can see a crossroad piece of snow it checks if the player has stood on it and which direction they went
                        {
                            snowCrossPiece script = hit.transform.gameObject.GetComponent<snowCrossPiece>();
                            if (script.steppedOn)
                            {
                                for (int i = 0; i < script.direction.Length; i++)
                                {
                                    direction[0] = false;
                                    direction[1] = false;
                                    direction[2] = false;
                                    direction[3] = false;
                                    if (script.direction[i])
                                    {
                                        if (previousCrossPiece != null)
                                        {
                                            previousCrossPiece.layer = 8;
                                        }
                                        previousCrossPiece = hit.transform.gameObject;
                                        previousCrossPiece.layer = 0;
                                        rotation = rotNum[i];
                                        foundPrints = true;
                                        nextDestination = hit.transform;
                                        direction[i] = true;
                                        findNextPrints = false;
                                        break;
                                    }
                                }
                            }
                            else if (findNextPrints) //if there is no footprints for the enemy to see go back to patrolling the predefined patrol route. This get the closest point in the patrol route
                            {
                                float dist = 10000000;
                                Transform nextDest = transform;
                                for (int i = 0; i < patrolPoints.Length; i++)
                                {
                                    float calcDist = Vector2.Distance(transform.position, patrolPoints[i].position);
                                    if (calcDist < dist)
                                    {
                                        dist = calcDist;
                                        nextDest = patrolPoints[i];
                                        nextPos = i;
                                    }
                                }

                                if (transform.localPosition.x == nextDest.localPosition.x || transform.localPosition.y == nextDest.localPosition.y)
                                {
                                    nextDestination = nextDest;
                                    foundPrints = false;
                                    findNextPrints = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        if(stop && resetTimer > 0 && reset) //this is used for when the enemy runs into a rotating wall it acts as if the enemy has been knocked out for a period of time
        {
            resetTimer -= Time.deltaTime;
        }
        else if(stop && reset)
        {
            stop = false;
            reset = false;
            nextDestination = rotateNextDest;
        }
    }

    void rotateEnemy(Transform currentPos, Transform nextPosition) //this rotates the enemies sprite
    {
        direction[0] = false;
        direction[1] = false;
        direction[2] = false;
        direction[3] = false;
        if (currentPos.localPosition.x == nextPosition.localPosition.x)
        {
            if (currentPos.localPosition.y > nextPosition.localPosition.y)
            {
                rotation = 270;
                direction[1] = true;
            }
            else
            {
                rotation = 90;
                direction[0] = true;
            }
        }
        else if(currentPos.position.y == nextPosition.position.y)
        {
            if (currentPos.localPosition.x > nextPosition.localPosition.x)
            {
                rotation = 180;
                direction[3] = true;
            }
            else
            {
                rotation = 0;
                direction[2] = true;
            }
        }
        transform.localRotation = Quaternion.Euler(0, 0, rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) //destroys player and activates lose condition
        {
            stop = true;
            Destroy(collision.gameObject);
            deathScreen.SetActive(true);
        }

        if (collision.gameObject.CompareTag("rotate")) //stops the enemy from looking for footprints or the player
        {
            stop = true;
            reset = true;
            resetTimer = 5f;
            playerSeen = false;
            nextDestination = patrolPoints[rotateDoorClosestPos];
            rotateNextDest = patrolPoints[rotateDoorClosestPos];
        }
    }


}
