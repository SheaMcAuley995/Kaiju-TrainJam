using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatAbility : MonoBehaviour {

    [SerializeField] float jumpSpeed = 10;
    static float floatFuelMax = 100;
    float floatFuelCur;
    Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        floatFuelCur = floatFuelMax;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButton("Jump") && floatFuelCur > 0)
        {
            jumpFloatAbility();
            floatFuelCur -= Time.time;
        }
        else
        {
            floatFuelCur += Time.time;
        }
	}


    void jumpFloatAbility()
    {
        rb.AddForce(Vector3.up * jumpSpeed);
    }
}
