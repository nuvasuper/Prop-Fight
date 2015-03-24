using UnityEngine;
using System.Collections;

public class ImpulseMove : MonoBehaviour {
	private float lastTime;
	public float moveForce = 10;
	public float rotForce = 1;
	public Rigidbody r;
	public float dragForce;
	public float mass;
	// Use this for initialization
	void Start () {
		mass = r.mass;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (Time.time - lastTime >= 1) {
			float h = Input.GetAxis ("Horizontal");
			float v = Input.GetAxis ("Vertical");
			Vector3 translation = new Vector3 (h, 0, v);
			translation = translation*moveForce;
			float z = Input.GetAxis ("RotZ");
			float y = Input.GetAxis ("RotY");
			float x = -1*Input.GetAxis ("RotX");
			Vector3 rotation = new Vector3(x,y,z)*rotForce;
			applyForces(translation, rotation);
		}
		Vector3 drag;
		if (dragForce <= 1) {
			drag = mass * r.velocity.magnitude * r.velocity.magnitude * r.velocity.normalized * -1 * dragForce * .4f;//drag = velocity * -1 * dragForce
		} else {
			drag = r.velocity*-.75f*mass;
		}
		drag = new Vector3 (drag.x, 0, drag.z);
		applyForces (drag, Vector3.zero);
	}

	void resetTime() {
		lastTime = Time.time;
	}

	void applyForces(Vector3 translate, Vector3 rotate) {
		r.AddForce (translate, ForceMode.Impulse);
		r.AddForce (rotate, ForceMode.Impulse);
	}
}
