using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chMove : MonoBehaviour {
    public float moveSpeed = 5f;

    public float turnSpeed = 200f;


    // Update is called once per frame

    void Update()
    {
		float h = Input.GetAxis("Mouse X");
		float v = Input.GetAxis("Mouse Y");

        //transform.Translate (0f, 0f, h * moveSpeed * Time.deltaTime);

        //Move 

//        transform.Translate(0f, 0f, v * moveSpeed * Time.deltaTime);


        //Turn

//        transform.Rotate(0f, h * turnSpeed * Time.deltaTime, 0f);


		if (Input.touchCount > 0)
		{
			transform.position += new Vector3(Camera.main.transform.forward.x * moveSpeed * Time.deltaTime,0, Camera.main.transform.forward.z * moveSpeed * Time.deltaTime);
		}

    }
   
}
