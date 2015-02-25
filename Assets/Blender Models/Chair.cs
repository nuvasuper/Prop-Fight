using UnityEngine;
using System.Collections;

public class Chair : MonoBehaviour {
	public GameObject ghostChair;
	private Transform t;
	private Transform gt;
	private Collider last;
	private bool grabbing;

	// Use this for initialization
	void Start () {
		Vector3 objectPool = new Vector3 (100, 0, 0);
		ghostChair = (GameObject)Instantiate(ghostChair, objectPool, Quaternion.Euler(-90,0,0));

		foreach (Collider i in ghostChair.GetComponentsInChildren<Collider>()) {
			foreach (Collider j in this.GetComponentsInChildren<Collider>()) {
				Physics.IgnoreCollision (i, j);
			}
		}

		//ignore collisions with chair
		t = this.transform;
		gt = ghostChair.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftControl)) {
			ability();
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
	
	void ability() {
		gt.position=t.position;
		gt.rotation = t.rotation;
	}

	void grab(Rigidbody r) {
		FixedJoint j = this.gameObject.AddComponent<FixedJoint>();
		j.connectedBody=r;
		j.breakForce=1;
		j.breakTorque=1;
		print ("grab: "+r);
	}
}
