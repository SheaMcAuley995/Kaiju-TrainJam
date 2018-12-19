using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour {

    [SerializeField] float damage = 10;
    
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision collision)
    {
       EnemyHealth target = collision.gameObject.GetComponent<EnemyHealth>();
       if (target != null)
       {
           target.TakeDamage(damage);
       }
        
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
    }
}
