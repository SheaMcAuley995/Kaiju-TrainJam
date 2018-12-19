using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingDestruction : MonoBehaviour {

    public ParticleSystem particleSystem;
    public Transform playPosition;
    public Transform endPosition;
    public float health = 100f;

    private void Start()
    {
        if(playPosition == null)
        {
            playPosition = transform;
        }
        endPosition.position = -transform.up * 10;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            StartCoroutine("Die");
        }
    }

    IEnumerator Die()
    {
        while(true)
        {
            particleSystem.transform.position = transform.position;
            particleSystem.Play();
            transform.position = Vector3.Lerp(transform.position, endPosition.position, Time.deltaTime);
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
    }
}
