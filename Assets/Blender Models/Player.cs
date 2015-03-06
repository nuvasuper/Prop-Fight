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
				grab(last.GetComponent<Rigidbody>());
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

	public void die() {
		Destroy(GameObject.Find("Dummy"));
		PhotonNetwork.Destroy(this.gameObject);
	}

	void OnGUI() {
		if (GUI.Button (new Rect (400, 50, 100, 50), "Respawn")) {
			GameObject.Find("_Photon Manager").GetComponent<PhotonManager>().spawnMyPlayer();
			die ();
		}
		if (GUI.Button (new Rect (400, 100, 100, 50), "Choose Again")) {
			GameObject.Find("_Photon Manager").GetComponent<PhotonManager>().chooseCharacter();
			die();
		}
	}
}
