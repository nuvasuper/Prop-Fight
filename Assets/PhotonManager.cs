using UnityEngine;
using System.Collections;

public class PhotonManager : MonoBehaviour {

	public string playerPrefab;
	public GameObject standbyCamera;
	Spawnpoint[] spawnpoints;
	public GameObject cS;
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
		chooseCharacter ();

	}

	void chooseCharacter() {
		cS.SetActive (true);
	}

	public void pickPrefab(string newPrefab) {
		playerPrefab = newPrefab;
		cS.SetActive (false);
		SpawnMyPlayer ();
	}

	void SpawnMyPlayer() {
		Spawnpoint mySpawnpoint = spawnpoints [Random.Range(0, spawnpoints.Length)];
		GameObject myPlayerGO;
		if (playerPrefab.Equals("Chair")||playerPrefab.Equals("Crate")||playerPrefab.Equals("Table")) {
			myPlayerGO = (GameObject)PhotonNetwork.Instantiate (playerPrefab, mySpawnpoint.transform.position, Quaternion.Euler(-90,0,0), 0);
		} else {
			myPlayerGO = (GameObject)PhotonNetwork.Instantiate (playerPrefab, mySpawnpoint.transform.position, Quaternion.Euler(0,0,0), 0);
		}
		standbyCamera.SetActive (false);
		myPlayerGO.GetComponent<Move> ().enabled = true;
		myPlayerGO.GetComponent<Player> ().enabled = true;
		//myPlayerGO.GetComponent<Chair> ().enabled = true;
		myPlayerGO.transform.FindChild("Dummy").gameObject.SetActive (true);
	}
}
