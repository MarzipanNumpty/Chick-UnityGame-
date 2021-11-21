using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowBallScript : MonoBehaviour
{
    public bool startRolling;
    [SerializeField]
    Transform endPos;
    [SerializeField]
    float speed;
    float rot;
    [SerializeField]
    GameObject loseScreen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startRolling) //makes the snowball constantly nmove in one direction
        {
            transform.position = Vector2.MoveTowards(transform.position, endPos.position, speed * Time.deltaTime);
            rot +=  1;
            transform.localRotation = Quaternion.Euler(0, 0, rot);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("rotate")) //if it hits a rotating wall destroy this snowball
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.CompareTag("Player")) //if it hits the player destroy the player and activate lose condition
        {
            Destroy(collision.gameObject);
            loseScreen.SetActive(true);
        }
    }
}
