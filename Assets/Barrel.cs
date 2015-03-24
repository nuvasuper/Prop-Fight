using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Barrel : MonoBehaviour {
	public Move movement;
	public Rigidbody myRigid;
	public float explosionForce = 5000;
	public float explosionRadius=7;
	public SphereCollider explosionZone;
	private List<Rigidbody> nearbyRigids = new List<Rigidbody>();
	public bool blown = false;
	// Use this for initialization
	void Start () {
		explosionZone.radius = explosionRadius;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftControl)&&!blown) {
			explode();
		}
	}

	void OnTriggerEnter(Collider other) {
		Rigidbody otherRigid = other.attachedRigidbody;
		if (otherRigid != null) {
			nearbyRigids.Add (otherRigid);
		}
	}

	void OnTriggerExit(Collider other) {
		Rigidbody otherRigid = other.attachedRigidbody;
		if (otherRigid != null) {
			nearbyRigids.Remove (otherRigid);
		}

	}

	void OnCollisionEnter(Collision other) {
		if (!blown&&other.rigidbody != null && other.relativeVelocity.magnitude > 50f) {
			explode ();
		}
	}

	void explode() {
		print ("boom.");
		myRigid.velocity = Vector3.zero;
		foreach (Rigidbody r in nearbyRigids) {
			r.AddExplosionForce(explosionForce,myRigid.position,explosionRadius);
			print("blowing up "+r);
		}
		this.gameObject.GetPhotonView ().RPC ("setVars", PhotonTargets.All, 1, 1, 1, 1);
		//movement.changeVars (1, 1, 1, 1);
		myRigid.velocity = Vector3.zero;
		blown = true;
		//explosion forces aren't networked.
	}
}
