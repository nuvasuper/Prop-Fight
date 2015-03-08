using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour {
	public PhotonManager pm;

	// Use this for initialization
	void OnCollisionEnter(Collision other) {
		GameObject doomed = other.gameObject;
		Player doomedPlayer = doomed.GetComponent<Player> ();
		PhotonView doomedView = doomed.GetPhotonView ();
		if (doomedPlayer != null&&pm.playerLiving) {
			//doomedPlayer.die ();
			pm.killPlayer();
		} else if (doomedView != null) {
			PhotonNetwork.Destroy (doomedView);
		} else {
			Destroy(doomed);
		}
	}
}
