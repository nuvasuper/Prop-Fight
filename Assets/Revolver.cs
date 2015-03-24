using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Revolver : MonoBehaviour {
	public List<Rigidbody> bullets = new List<Rigidbody> ();
	public Vector3 shootPoint;
	public float firingForce=100f;
	public Rigidbody r;
	public float bulletMass;
	public float bulletMassPercent=.5f;//must be less than 1
	// Use this for initialization
	void Start () {
		bulletMass = r.mass * bulletMassPercent/5;
		foreach (Rigidbody bullet in bullets) {
			bullet.mass = bulletMass;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftControl)&&bullets.Count>0) {
			this.gameObject.GetPhotonView().RPC ("fire",PhotonTargets.All);
		}
	}

	[RPC]
	void fire() {
		print ("bam. count = " + bullets.Count);
		Rigidbody bullet = bullets [0];
		bullets.RemoveAt (0);
		bullet.transform.parent = null;
		bullet.position = shootPoint;
		bullet.isKinematic = false;
		bullet.AddForce (Vector3.forward * firingForce);
		r.mass -= bulletMass;
		r.AddForce(Vector3.back*firingForce);
		//directions aren't local
		//this is bad
		//bullets aren't networked


	}
}


