using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectile;
    public bool alreadyAttacked = false;
    public float timebetweenAttacks;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (alreadyAttacked == false)
        {
            if (Input.GetMouseButton(0))
            {
                Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 20f, ForceMode.Impulse);
                rb.AddForce(transform.up * 2f, ForceMode.Impulse);

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timebetweenAttacks);
            }
        }
        
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
