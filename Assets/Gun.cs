using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float fireSpeed = 50f;

    public Camera fpsCam;
    public Transform spawnPosition;

    [SerializeField] GameObject trainPrefab;
    [SerializeField] float fireRate = 15f;
    float nextTimeToFire = 0f;

    // Update is called once per frame
    void Update () {
		if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            spawnTrain();
        }
	}


    void spawnTrain()
    {
        Vector3 dir = ShootPosition() - spawnPosition.position;
        Quaternion fireAtRotation = Quaternion.LookRotation(dir);
        GameObject train = Instantiate(trainPrefab, spawnPosition.position, fireAtRotation);

        train.GetComponent<Rigidbody>().AddForce(dir.normalized * fireSpeed);
    }

    Vector3 ShootPosition()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, Mathf.Infinity))
        {
            return hit.point;
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + fpsCam.transform.forward * 10);
            return transform.position + fpsCam.transform.forward * 10;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, ShootPosition());
    }

}
