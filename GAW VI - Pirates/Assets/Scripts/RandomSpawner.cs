using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject CargoShip;
    public GameObject[] asteroids;
    public float spawnInterval = 5f; 
    public float spawnForce = 300f; 
    public float spawnRange = 500f;
    GameObject spawnedObject;
    private float timer = 0f; 

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update(){

        // Increment the timer
        timer += Time.deltaTime;

        // Check if it's time to spawn
        if (timer >= spawnInterval)
        {
            // Reset the timer
            timer = 0f;

            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRange;
            if(Random.Range(0,100) < 30){
                spawnedObject = Instantiate(asteroids[Random.Range(0,2)], transform.position, Quaternion.identity);

            }
            else{
                spawnedObject = Instantiate(CargoShip, transform.position, Quaternion.identity);
            }
            

            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(randomDirection * spawnForce, ForceMode.Impulse);
            }
        }
    }
    // you could also add if the cargo ship dies spawn the pickup where the cargo ship was
    // the set the cargoship to false 
    // add a counter if the cargoship dies add another 
    //use a while loop for this with an int of the ships 


}
