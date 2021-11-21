using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camScript : MonoBehaviour
{
    PlayerControls camControls;
    Vector2 move;
    Rigidbody2D rb;
    [SerializeField]
    float speed;
    [SerializeField]
    Transform playerPos;
    bool lockOnPlayer = true;
    float scrollWheel;
    [SerializeField]
    Camera cam;
    void Awake()
    {
        camControls = new PlayerControls();
        camControls.Controls.CameraMove.performed += ctx => move = ctx.ReadValue<Vector2>();
        camControls.Controls.CameraMove.canceled += ctx => move = Vector2.zero;
        camControls.Controls.unlockCam.performed += ctx => unlockCam();
        camControls.Controls.scroll.performed += ctx => scrollWheel = ctx.ReadValue<float>();
        camControls.Controls.scroll.canceled += ctx => scrollWheel = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    void unlockCam()
    {
        lockOnPlayer = !lockOnPlayer;
        cam.orthographicSize = 5;
    }

    private void OnEnable()
    {
        camControls.Enable();
    }

    private void OnDisable()
    {
        camControls.Disable();
    }


    void FixedUpdate() //moves camera
    {
        if(lockOnPlayer)
        {
            transform.localPosition = new Vector3(playerPos.localPosition.x, playerPos.localPosition.y, -10);
        }
        else
        {
            rb.velocity = new Vector2(move.x * speed, move.y * speed);
        }
    }

    private void Update() //zooms camera in and out
    {
        if (!lockOnPlayer)
        {
            if (scrollWheel > 0 && cam.orthographicSize > 5)
            {
                cam.orthographicSize /= 1.1f;
            }
            else if (scrollWheel < 0 && cam.orthographicSize < 15)
            {
                cam.orthographicSize /= .9f;
            }
        }
    }
}
