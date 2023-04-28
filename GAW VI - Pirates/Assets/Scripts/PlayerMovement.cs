using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //For Movement
    public float moveSpeed;
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


    //For Camera
    public Transform PlayerCamera;
    public Vector2 sens;

    private Vector2 XYRotation;

    public GameObject projectile;

    public float health = 100;
    public GameObject panel;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        //Makes mouse invisible
        //Cursor.visible = false;

        wallRunSpeed = moveSpeed;


        panel.SetActive(false);
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

        //All For Camera

        Vector2 MouseInput = new Vector2
        {
            x = Input.GetAxisRaw("Mouse X"),
            y = Input.GetAxisRaw("Mouse Y")

        };


        XYRotation.x -= MouseInput.y * sens.y;
        XYRotation.y += MouseInput.x * sens.x;

        XYRotation.x = Mathf.Clamp(XYRotation.x, -90f, 90f);
        transform.eulerAngles = new Vector3(0f, XYRotation.y, 0f);
        PlayerCamera.localEulerAngles = new Vector3(XYRotation.x, 0f, 0f);

        if (health <= 0)
        {


            panel.SetActive(true);
            Cursor.visible = true;
            sens = sens - sens;
            moveSpeed = moveSpeed - moveSpeed;

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

  





 

        if (collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("Ow");
            health = health - 25f;
        }


    }
    void OnTriggerEnter(Collider collision)
    {


        if (collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("Ow");
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
