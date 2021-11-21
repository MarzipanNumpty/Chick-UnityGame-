using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowBallTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject snowBall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) //starts the snowball rolling
        {
            snowBall.GetComponent<snowBallScript>().startRolling = true;
            Destroy(gameObject);
        }
    }
}
