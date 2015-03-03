using UnityEngine;
using System.Collections;

public class Chair : MonoBehaviour {
	public GameObject ghostChair;
	private Transform t;
	public PhotonView pv;

	// Use this for initialization
	void Start () {
		GetComponent<Player>().type = "chair";
		t = this.transform;
		//pv.RPC ("ignoreCollisions",PhotonTargets.All);
		//ignore collisions with chair
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftControl)) {
			ability();
		}
	}

	void ability() {
		if (pv != null) {
			pv.RPC("warpTo", PhotonTargets.All, t.position, t.rotation);
		} else {
			print ("pv missing on ghostchair");
		}
	}
}
