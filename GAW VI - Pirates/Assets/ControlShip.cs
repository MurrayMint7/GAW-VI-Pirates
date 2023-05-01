using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlShip : MonoBehaviour
{
    public GameObject ship;
    public GameObject player;
    public GameObject playerCam;
    public Transform seat;
    public Transform target;
    public bool seated = false;
    SpaceShipController controllerScript;
    PlayerMovement playerScript;

    // Start is called before the first frame update
    void Start()
    {
        controllerScript = ship.GetComponent<SpaceShipController>();
        playerScript = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        ship = GameObject.Find("Space Ship");
        player = GameObject.Find("Player");

        if (Input.GetKey(KeyCode.P))
        {
            ship.transform.rotation = Quaternion.identity;
            playerScript.enabled = true;
            controllerScript.enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
            player.GetComponent<PlayerAttack>().enabled = true;
            player.transform.SetParent(player.transform, false);
            Rigidbody rb = player.GetComponent<Rigidbody>();
            rb.mass = 1;
            rb.useGravity = true;
            seated = false;
        }

        if(seated){
            player.transform.position = seat.transform.position;            
            player.transform.rotation = seat.transform.rotation;                         
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player"){           
            other.transform.parent.GetComponent<PlayerMovement>().enabled = false;
            other.transform.parent.GetComponent<CameraMovement>().enabled = false;
            player.GetComponent<PlayerAttack>().enabled = false;
            controllerScript.enabled = true;
            player.transform.SetParent(ship.transform);
            seated = true;
            Rigidbody rb = player.GetComponent<Rigidbody>();
            rb.mass = 500;
            rb.useGravity = false;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player"){
            gameObject.GetComponent<Collider>().enabled = true;
        }
    }
}
