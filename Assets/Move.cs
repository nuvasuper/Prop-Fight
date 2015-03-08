using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	public float moveForce = 10;
	public float rotForce = 1;
	public Rigidbody r;
	
	void Start () {
	}
	void Update() {
	}

	// Update is called once per frame
	void FixedUpdate () {
			float h = Input.GetAxis ("Horizontal");
			float v = Input.GetAxis ("Vertical");
			Vector3 translation = new Vector3 (h, 0, v);
			translation = translation * moveForce;
				
			float z = Input.GetAxis ("RotZ");
			float y = Input.GetAxis ("RotY");
			float x = -1*Input.GetAxis ("RotX");
			Vector3 rotation = new Vector3(x,y,z)*rotForce;
			rotation = rotation * rotForce;
			applyForces(translation, rotation);
	}


	void applyForces(Vector3 translate,Vector3 rotate) {
		r.AddForce (translate);
		r.AddTorque(rotate);
	}
}
