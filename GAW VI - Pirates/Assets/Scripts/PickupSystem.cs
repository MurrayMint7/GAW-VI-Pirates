using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PickupSystem : MonoBehaviour
{
    public TextMeshProUGUI collectionText;


    public float startCount = 0;

    public bool PlayerRange = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
            collectionText.text = startCount.ToString();
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Change the tag to whatever you tagged the pickup object as
        if (collision.gameObject.CompareTag("Fuel"))
        {
            startCount = startCount + 1;
            collision.gameObject.SetActive(false);
        }

    }

}
