using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    PlayerControls controls;
    Vector2 move;
    Rigidbody2D rb;
    [SerializeField]
    float speed;
    Vector2 moveDirection;
    bool canInteract;
    public GameObject currentRotateDoor;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Controls.Jump.performed += ctx => jump();
        controls.Controls.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Controls.Move.canceled += ctx => move = Vector2.zero;
        controls.Controls.Interact.performed += ctx => interact();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void jump()
    {
        Debug.Log("Jump");
    }

    void interact()
    {
        canInteract = true;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void FixedUpdate() 
    {
        if (move.x > 0) //this is done so the enmy can know which direction the player went
        {
            playerDirectionManager.instance.goingRight = true;
            playerDirectionManager.instance.goingLeft = false;
        }
        else if (move.x < 0)
        {
            playerDirectionManager.instance.goingRight = false;
            playerDirectionManager.instance.goingLeft = true;
        }
        else
        {
            playerDirectionManager.instance.goingRight = false;
            playerDirectionManager.instance.goingLeft = false;
        }

        if(move.y > 0)
        {
            playerDirectionManager.instance.goingUp = true;
            playerDirectionManager.instance.goingDown = false;
        }
        else if(move.y < 0)
        {
            playerDirectionManager.instance.goingDown = true;
            playerDirectionManager.instance.goingUp = false;
        }
        else
        {
            playerDirectionManager.instance.goingDown = false;
            playerDirectionManager.instance.goingUp = false;
        }
        rb.velocity = new Vector2(move.x * speed, move.y * speed);

        if(currentRotateDoor != null && canInteract) //this rotates the rotateable wall
        {
            canInteract = false;
            Animator anim = currentRotateDoor.GetComponent<Animator>();
            anim.SetBool("rotateRight", true);
            anim.SetBool("rotateLeft", true);
            /*if (currentRotateDoor.GetComponent<rotateScript>().left)
            {
                currentRotateDoor.GetComponent<rotateScript>().left = false;
                anim.SetBool("rotateRight", true);
            }
            else
            {
                currentRotateDoor.GetComponent<rotateScript>().left = true;
                anim.SetBool("rotateLeft", true);
            }*/
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag("lever") && canInteract) //activates the lever that opens the a door
        {
            canInteract = false;
            collision.gameObject.GetComponent<leverScript>().openDoor();
            collision.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (collision.gameObject.CompareTag("snowLever") && canInteract) //activates the lever that drops the snow on the path
        {
            canInteract = false;
            collision.gameObject.GetComponent<snowLeverScript>().dropSnow();
            collision.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canInteract) //makes sure that caninteract isnt active
        {
            canInteract = false;
        }
        if(collision.gameObject.CompareTag("rotate")) //makes a reference to the current rotating wall
        {
            currentRotateDoor = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("rotate")) //removes the reference to the rotating wall
        {
            currentRotateDoor = null;
        }
    }
}
