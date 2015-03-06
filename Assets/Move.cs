using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	public float moveForce = 10;
	public float rotForce = 1;
	private Rigidbody r;
	private int mode;
	
	void Start () {
		r = this.GetComponent<Rigidbody>();
		if (r.constraints==(RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationZ)) {
				mode = 0;
		} else {
			mode = 1;
		}
	}
	void Update() {
	}

	// Update is called once per frame
	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		Vector3 moveDirection = new Vector3 (h, 0, v);
		r.AddForce (moveDirection * moveForce);


		if (mode ==1) {//If we're not locked in Z and X rotation, then allow rotation forces
			float z = Input.GetAxis ("RotZ");
			float y = Input.GetAxis ("RotY");
			float x = -1*Input.GetAxis ("RotX");
			Vector3 rotation = new Vector3(x,y,z)*rotForce;
			r.AddTorque(rotation);
		}
	}
}
