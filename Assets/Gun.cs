using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float fireSpeed = 50f;

    public Camera fpsCam;
    public Transform spawnPosition;

    [SerializeField] GameObject trainPrefab;
    // Update is called once per frame
    void Update () {
		if(Input.GetButtonDown("Fire1"))
        {
            spawnTrain();
        }
	}


    void spawnTrain()
    {
        Vector3 dir =  fpsCam.transform.forward - spawnPosition.position;
        Quaternion fireAtRotation = Quaternion.LookRotation(dir);
        GameObject train = Instantiate(trainPrefab, spawnPosition.position, Quaternion.identity);

        train.GetComponent<Rigidbody>().AddForce(dir * fireSpeed);
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
            Debug.DrawLine(transform.position, transform.position + transform.forward);
            return spawnPosition.forward * 2;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, ShootPosition());
    }

}
