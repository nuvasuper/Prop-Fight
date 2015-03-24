using UnityEngine;
using System.Collections;

public class Bucket : MonoBehaviour {

	// Use this for initialization
	public Player player;
	public bool canBlind = true;
	public float blindTime= 5;
	//public PhotonView pv;
	//public GameObject blindness;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftControl)&&player.grabbing&&canBlind) {
			StartCoroutine(blind(player.last.gameObject));
		}
	}
	
	IEnumerator blind(GameObject other) {
		canBlind = false;
		Player otherPlayer = other.GetComponent<Player> ();

		if (otherPlayer != null) {
			other.GetPhotonView().RPC("blind",PhotonTargets.All,blindTime);
		}
		yield return new WaitForSeconds(blindTime);
		canBlind=true;
	}
}
