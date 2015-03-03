using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private int status;
	private string username;
	private string team;
	public string type;
	private Collider last;
	private bool grabbing;


	// Use this for initialization
	void Start () {
		this.type = "player";
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			if (!grabbing&&last!=null) {
				grabbing = true;
				grab(last.rigidbody);
			} else {
				Destroy(this.gameObject.GetComponent<FixedJoint>());
				grabbing = false;
			}
		}
	}

	void OnCollisionEnter(Collision other) {
		if (other.rigidbody != null) {
			last = other.collider;
		}
	}
	
	void grab(Rigidbody r) {
		FixedJoint j = this.gameObject.AddComponent<FixedJoint>();
		j.connectedBody=r;
		j.breakForce=1;
		j.breakTorque=1;
		print ("grab: "+r);
	}
}
