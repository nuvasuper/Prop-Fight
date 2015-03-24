using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	public float moveForce = 10;
	public float rotForce = 1;
	public Rigidbody r;
	public float dragForce;
	public float mass;

	void Start () {
		mass = r.mass;
	}
	void Update() {
	}

	// Update is called once per frame
	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		Vector3 translation = new Vector3 (h, 0, v);
		translation = translation.normalized;//so you don't go faster just for going diagonally
		Vector3 drag;
		if (dragForce <= 1) {
			drag = mass * r.velocity.magnitude * r.velocity.magnitude * r.velocity.normalized * -1 * dragForce * .4f;//drag = velocity * -1 * dragForce
		} else {
			drag = r.velocity*-.75f*mass;
		}
		drag = new Vector3 (drag.x, 0, drag.z);
		translation = (translation*moveForce + drag) / Time.deltaTime;
		//if drag>1, things get busted
		float z = Input.GetAxis ("RotZ");
		float y = Input.GetAxis ("RotY");
		float x = -1*Input.GetAxis ("RotX");
		Vector3 rotation = new Vector3(x,y,z)*rotForce;
		rotation = rotation * rotForce/Time.deltaTime;

		applyForces(translation, rotation);
		//print ("velocity="+r.velocity);
	}


	void applyForces(Vector3 translate,Vector3 rotate) {
		r.AddForce (translate);
		r.AddTorque(rotate);
	}

	[RPC]
	public void changeVars(float newMoveForce, float newDragForce, float newRotForce, float newMass) {
		setVars (moveForce + newMoveForce, dragForce + newDragForce, rotForce + newRotForce, mass + newMass);
	}

	[RPC]
	public void setVars(float newMoveForce, float newDragForce, float newRotForce, float newMass) {
		moveForce = newMoveForce;
		dragForce = newDragForce;
		rotForce = newRotForce;
		r.mass = newMass;
		mass = newMass;
	}
}
