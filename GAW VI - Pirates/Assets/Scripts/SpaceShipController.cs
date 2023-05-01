using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public float movementSpeed = 10f;
    public float gravity = 9.81f;
    public float drag = 1f;
    public GameObject player;
    public GameObject bullet;
    private Rigidbody rb;
    private bool isMoving = false;
    public Transform leftShot;
    public Transform rightShot;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.drag = drag;
    }

    void Update(){
        // Shoot from left gun on left click
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, leftShot.position, leftShot.rotation);
        }

        // Shoot from right gun on right click
        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(bullet, rightShot.position, rightShot.rotation);
        }
    }
    void FixedUpdate()
    {

        // Decrease Y rotation on press of Q
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
        }

        // Increase Y rotation on press of E
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }

        // Increase X rotation on press of A
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(rotationSpeed * Time.deltaTime, 0f, 0f);
        }

        // Decrease X rotation on press of D
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-rotationSpeed * Time.deltaTime, 0f, 0f);
        }

        // Increase Z rotation on press of S
        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }

        // Decrease Z rotation on press of W
        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
        }

        // Move forward on press of SPACE
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.right * movementSpeed);
        }

        
    }
}
