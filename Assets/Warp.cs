using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour {
	Transform gt;
	public Transform myPlayer;
	bool isGood = false;

	// Use this for initialization
	void Start () {
		gt = this.transform;
		gt.parent = null;
		Vector3 objectPool = new Vector3 (100, 0, 0);
		gt.position = objectPool;
		ignoreCollisions ();
		print("Initially fixed collisions");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	[RPC]
	void warpTo(Vector3 p, Quaternion r) {
		if (!isGood) {
			ignoreCollisions();
			print("Fixed collisions");
		}
		print ("just warped");
		gt.position=p;
		gt.rotation = r;
	}

	[RPC]
	void ignoreCollisions() {
		foreach (Collider i in myPlayer.GetComponentsInChildren<Collider>()) {
			foreach (Collider j in this.GetComponentsInChildren<Collider>()) {
				Physics.IgnoreCollision (i, j);
			}
		}
		isGood = true;
	}
}
