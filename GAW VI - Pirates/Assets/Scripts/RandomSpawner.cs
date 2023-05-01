using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject CargoShip;
    //Make sure to create a cargoship prefab then just insert it inti this code 
    //you can add this code to the player 
    
    // Start is called before the first frame update
    void Start()
    {
        // this will create a random spawn for what object you like
        //the first is the X Choose from a range of 2 x axis
        //the second is the Y it can be random from a range if you want 
        //the third is a z another random range 
        // you will have to decide the range so they arnt too far away ive just chosen rnadomly 
        Vector3 RandomSpawnPos = new Vector3(Random.Range(-100, 110), 50, Random.Range(-50,60));
        //make a perfab of the cargo ship and replace it with the object prefab
        //Quaternion just changes the rotation to null you cna change this if needed 
        Instantiate(CargoShip, RandomSpawnPos, Quaternion.identity);
    }

    // you could also add if the cargo ship dies spawn the pickup where the cargo ship was
    // the set the cargoship to false 
    // add a counter if the cargoship dies add another 
    //use a while loop for this with an int of the ships 


}
