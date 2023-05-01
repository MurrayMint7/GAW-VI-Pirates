using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //For Movement
    public static float moveSpeed = 30;
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    public float groundDrag = 5f;
    Vector3 moveDirection;
    Rigidbody rb;
    public bool onGround = true;
    public float jumpForce = 5f;
    public float wallRunSpeed;
    public bool wallrunning;
    public static float health = 100;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        //Makes mouse invisible
        Cursor.visible = false;

        wallRunSpeed = moveSpeed;


    }

    void FixedUpdate()
    {
        MovePlayer();

    }

    void Update()
    {

        //All Movement
        MyInput();
        SpeedControl();

        //checks if can jump
        if (onGround == true)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                onGround = false;

            }
        }

        if (onGround == true)
        {
            rb.drag = groundDrag;
        }

        if (onGround == false)
        {
            rb.drag = 0f;
        }

    }

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

    }

    //movement
    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }


    //Jump
    void OnCollisionEnter(Collision collision)
    {
        //For Jump
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;

        }



    }
    void OnTriggerEnter(Collider collision)
    {


        if (collision.gameObject.CompareTag("Projectile"))
        {
      
            health = health - 25f;
        }


    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.drag = 0f;
            onGround = false;
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}