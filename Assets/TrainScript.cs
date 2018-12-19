using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainScript : MonoBehaviour {

    [SerializeField] float damage = 10;
    private void OnCollisionEnter(Collision collision)
    {
        
        EnemyHealth target = collision.gameObject.GetComponent<EnemyHealth>();
        buildingDestruction Buildingtarget = collision.gameObject.GetComponent<buildingDestruction>();
        if(target != null)
        {
            target.TakeDamage(damage);
        }
        if(Buildingtarget != null)
        {
            Buildingtarget.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
