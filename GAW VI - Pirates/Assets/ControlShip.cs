using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlShip : MonoBehaviour
{
    public GameObject ship;
    public GameObject player;

    SpaceShipController controllerScript;
    SpaceShipController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        controllerScript = ship.GetComponent<SpaceShipController>();
        playerScript = player.GetComponent<SpaceShipController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            playerScript.enabled = true;
            controllerScript.enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player"){
            other.GetComponent<PlayerMovement>().enabled = false;
            controllerScript.enabled = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player"){
            gameObject.GetComponent<Collider>().enabled = true;
        }
    }
}
