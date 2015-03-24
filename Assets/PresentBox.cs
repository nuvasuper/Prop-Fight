using UnityEngine;
using System.Collections;

public class PresentBox : MonoBehaviour {

	public Player player;
	//private bool full = false;
	private GameObject inTheBox =null;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			if (inTheBox==null) {
				this.gameObject.GetPhotonView().RPC("box",PhotonTargets.All);
				print("box RPC");
			} else {
				this.gameObject.GetPhotonView().RPC("unbox",PhotonTargets.All);
				print("unboxing RPC");
			}
		}
	}

	[RPC]
	void box() {
		print ("recieving RPC");
		inTheBox = player.last.gameObject; 
		inTheBox.transform.parent = transform;
		inTheBox.SetActive (false);//find way to preserve position sync, or send RPC sync when unboxing
	}

	[RPC]
	void unbox() {
		inTheBox.transform.parent = null;
		inTheBox.SetActive (true);
		inTheBox = null;

	}
}
