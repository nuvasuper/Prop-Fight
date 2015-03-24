using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crate : Photon.MonoBehaviour {

	private List<Rigidbody> nearbyRigids = new List<Rigidbody>();
	public PhotonView pv;
	public float chunkSize = 1;
	public Rigidbody r;
	public Move m;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftControl)) {
			eat ();
		}
	}

	void eat() {
		//List<PhotonView> victims = new List<PhotonView>();
		float gain = 0;


		foreach (Rigidbody r in nearbyRigids) {
			gain++;
			Player p = r.GetComponent<Player>();
			if (p!=null) {
				r.gameObject.GetPhotonView().RPC ("eat", PhotonTargets.All, chunkSize);
			} else {
				if (r.mass<=chunkSize) {
					if (r.gameObject.GetPhotonView()!=null) {
						PhotonNetwork.Destroy(r.gameObject);
					} else {
						r.mass-=chunkSize;
					}
				}
			}
		}
		this.gameObject.GetPhotonView().RPC("grow",PhotonTargets.All, gain);
	}

	[RPC]
	void grow(float gain) {
		m.moveForce += (gain / r.mass) * m.moveForce;
		r.mass += gain;

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
	//almost impossible to force other rigids into crate
	//replace with vaccum powers?
}
