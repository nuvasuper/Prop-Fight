using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private int status;
	private string username;
	private string team;
	private string type;
	private Collider last;
	private bool grabbing;

	/*public Player(string username, string team) {
		this.type = "player";
		this.username = username;
		this.team = team;
		this.addRigid ();
		this.addMove ();
	}*/

	// Use this for initialization
	void Start () {
		this.type = "player";
		this.addRigid ();
		this.addMove ();
	}


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			ability ();
		}
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			if (!grabbing) {
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

	void addRigid() {
		this.gameObject.AddComponent<Rigidbody> ();
		this.gameObject.AddComponent<BoxCollider> ();
	}

	void addMove() {
		this.gameObject.AddComponent<Move> ();
		Move m = this.GetComponent<Move> ();
		m.moveForce = 10f;
		m.rotForce = 10f;
	}

	void ability() {
		print ("ability");
	}
	void grab(Rigidbody r) {
		FixedJoint j = this.gameObject.AddComponent<FixedJoint>();
		j.connectedBody=r;
		j.breakForce=1;
		j.breakTorque=1;
		print ("grab: "+r);
	}
}
