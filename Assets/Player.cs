using UnityEngine;
using System.Collections;

public class Player : Photon.MonoBehaviour {
	//private int status;
	//private string username;
	//private string team;
	public string type;
	public Rigidbody last;
	public bool grabbing;
	public PhotonView pv;
	public GameObject myCamera;
	public Rigidbody r;



	// Use this for initialization
	void Start () {
		if (type==null)
			type = "default";
		pv = this.GetComponent<PhotonView> ();
	}

	// Update is called once per frame
	void Update () {
		if (pv.isMine) {
			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				if (!grabbing && last != null) {
					pv.RPC ("grab", PhotonTargets.All);
				} else {
					pv.RPC ("release",PhotonTargets.All);
				}
			}
		}
	}

	void OnJointBreak(float breakForce) {
		print ("joint just broke");
		grabbing = false;
	}

	void OnCollisionEnter(Collision other) {//note: other.gameobject gets the ROOT object, 
		//										not the object that this component is attached to
		/*if (other.gameObject.tag.Equals("SubCollider")) {
			//print("hit player");
			last = other.transform.parent.parent.GetComponent<Rigidbody>();
		} else*/
		if (other.rigidbody!=null) {
			print ("hit " + other.gameObject);
			last = other.rigidbody;
		}
	}

	[RPC]
	void grab() {
		if (last.gameObject.GetPhotonView()!=null&&last.gameObject.GetPhotonView().isMine) {
			last.gameObject.GetComponent<Player>().OnGrabbed(this.gameObject);
		}
		grabbing = true;
		print ("grabbing " + last);
		FixedJoint j = this.gameObject.AddComponent<FixedJoint>();
		j.connectedBody = last;
		j.breakForce=100;
		j.breakTorque=100;
		//j.enablePreprocessing = false;
	}

	[RPC]
	void release() {
		if (last.gameObject.GetPhotonView()!=null&&last.gameObject.GetPhotonView().isMine) {
			last.gameObject.GetComponent<Player>().OnReleased(this.gameObject);
		}
	 	FixedJoint j = this.GetComponent<FixedJoint> ();
		if (j != null) {
			Destroy(j);
			grabbing=false;
		}
	}

	public void die() {
		if (last != null) {
			print (last + " destroyed " + this);
		} else {
			print(this + "destroyed themself");
		}
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

	[RPC]
	void blind(float blindtime) {
		if (pv.isMine) {
			Transform baseCamera =  myCamera.transform.GetChild(0);
			GameObject blindScreen = Resources.Load("Resources/Blind",typeof(GameObject)) as GameObject;
			Instantiate(blindScreen,new Vector3(0,-10,0),Quaternion.identity);
			StartCoroutine(blindScreen.GetComponent<Blindness>().setVictim(baseCamera, blindtime));
		}
	}

	[RPC]
	void eat (float chunkSize) {
		if (r.mass <= chunkSize) {
			if (pv.isMine) {
				PhotonNetwork.Destroy (this.gameObject);
			}
		} else {
			r.mass -= chunkSize;
		}
	}

	void OnGrabbed(GameObject other) {
		if (this.type.Equals ("Table")) {
			this.GetComponent<Table>().OnGrabbed(other);
		}
	}

	void OnReleased(GameObject other) {
		if (this.type.Equals ("Table")) {
			this.GetComponent<Table>().OnReleased(other);
		}
	}
}
