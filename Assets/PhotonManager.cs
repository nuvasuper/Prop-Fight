using UnityEngine;
using System.Collections;

public class PhotonManager : MonoBehaviour {

	public string playerPrefab;
	public GameObject standbyCamera;
	Spawnpoint[] spawnpoints;
	// Use this for initialization
	void Start () {
		Connect ();
	}
	

	void Connect() {
		PhotonNetwork.ConnectUsingSettings ("0.0.1");
	}

	void OnGUI()
	{
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString());
	}

	void OnJoinedLobby() {
		PhotonNetwork.JoinRandomRoom ();	
	}

	void OnPhotonRandomJoinFailed() {
		PhotonNetwork.CreateRoom (null);
	}

	void OnJoinedRoom() {
		spawnpoints = GameObject.FindObjectsOfType<Spawnpoint> ();
		Debug.Log ("OnJoinedRoom");
		SpawnMyPlayer ();
	}
	void SpawnMyPlayer() {
		Spawnpoint mySpawnpoint = spawnpoints [Random.Range(0, spawnpoints.Length)];

		GameObject myPlayerGO = (GameObject)PhotonNetwork.Instantiate (playerPrefab, mySpawnpoint.transform.position, Quaternion.Euler(-90,0,0), 0);
		standbyCamera.SetActive (false);
		myPlayerGO.GetComponent<Move> ().enabled = true;
		myPlayerGO.GetComponent<Player> ().enabled = true;
		myPlayerGO.GetComponent<Chair> ().enabled = true;
		myPlayerGO.transform.FindChild("Dummy").gameObject.SetActive (true);
	}
}
