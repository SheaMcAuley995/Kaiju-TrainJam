using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookat : MonoBehaviour {

    public Transform target;
    public bool freezeY = false;
    public bool freezeX = false;
    public bool freezeZ = false;

    private float xRot;
    private float yRot;
    private float zRot;


    public void LookAt(Transform target)
    {

        if (target == null)
        {

            transform.LookAt(target, Vector3.up);

            xRot = transform.eulerAngles.x;
            yRot = transform.eulerAngles.y;
            zRot = transform.eulerAngles.z;
            if (freezeX)
            {
                transform.eulerAngles = new Vector3(0, yRot, zRot);
            }
            if (freezeY)
            {
                transform.eulerAngles = new Vector3(xRot, 0, zRot);
            }
            if (freezeZ)
            {
                transform.eulerAngles = new Vector3(xRot, yRot, 0);
            }

        }


    }
}
