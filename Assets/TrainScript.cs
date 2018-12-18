using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainScript : MonoBehaviour {

    [SerializeField] float damage = 10;
    private void OnCollisionEnter(Collision collision)
    {
        EnemyHealth target = collision.gameObject.GetComponent<EnemyHealth>();
        if(target != null)
        {
            target.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
