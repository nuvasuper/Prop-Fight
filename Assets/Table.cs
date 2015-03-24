using UnityEngine;
using System.Collections;

public class Table : MonoBehaviour {
	public Move m;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnGrabbed(GameObject other) { 
		//increase stuff
		Rigidbody otherRigid = other.GetComponent<Rigidbody>();
		float extraForce = otherRigid.mass;
		this.gameObject.GetPhotonView ().RPC ("changeVars", PhotonTargets.All, extraForce, 0, 0, 0);
	}

	public void OnReleased(GameObject other) {
		//decrease stuff
		Rigidbody otherRigid = other.GetComponent<Rigidbody>();
		float lessForce = -1*otherRigid.mass;
		this.gameObject.GetPhotonView ().RPC ("changeVars", PhotonTargets.All, lessForce, 0, 0, 0);
	}
}
