using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullets : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
        {
            DestroyObject();
        }
        if (collider.gameObject.CompareTag("Wall"))
        {
            DestroyObject();
        }

    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
