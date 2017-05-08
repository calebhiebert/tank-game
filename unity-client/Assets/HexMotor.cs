using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMotor : MonoBehaviour {

    public Rigidbody2D body;
    public float force = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.A))
        {
            body.AddForce(Vector2.left * force);
        }
        
        if(Input.GetKey(KeyCode.D))
        {
            body.AddForce(Vector2.right * force);
        }
    }
}
