using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateScript : MonoBehaviour
{
    Animator anim;
    public bool left;
    public bool rotateClockWise;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void resetBools() //leaves the rotating door in an idle position
    {
        anim.SetBool("rotateRight", false);
        anim.SetBool("rotateLeft", false);
    }
}
