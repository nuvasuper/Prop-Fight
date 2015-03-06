using UnityEngine;
using System.Collections;

public class PhotonManager : MonoBehaviour {

	public string playerPrefab;
	public GameObject standbyCamera;
	Spawnpoint[] spawnpoints;
	public GameObject cS;
	public bool playerLiving;
	GameObject myPlayerGO;
	// Use this for initialization
	void Start () {
		Connect ();
		playerLiving = false;
	}
	

	void Connect() {
		PhotonNetwork.ConnectUsingSettings ("0.0.1");
	}

	void OnGUI()
	{
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString());
		if (playerLiving) {
			if (GUI.Button (new Rect (400, 50, 100, 50), "Respawn")) {
				standbyCamera.SetActive(true);
				playerLiving=false;
				myPlayerGO.GetComponent<Player>().die ();
				GameObject.Find ("_Photon Manager").GetComponent<PhotonManager> ().spawnMyPlayer ();


			}
			if (GUI.Button (new Rect (400, 100, 100, 50), "Choose Again")) {
				standbyCamera.SetActive(true);
				playerLiving=false;
				myPlayerGO.GetComponent<Player>().die ();
				GameObject.Find ("_Photon Manager").GetComponent<PhotonManager> ().chooseCharacter ();


			}
		}
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

	public void chooseCharacter() {
		cS.SetActive (true);
	}

	public void pickPrefab(string newPrefab) {
		playerPrefab = newPrefab;
		cS.SetActive (false);
		spawnMyPlayer ();
	}

	public void spawnMyPlayer() {
		Spawnpoint mySpawnpoint = spawnpoints [Random.Range(0, spawnpoints.Length)];
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
		playerLiving=true;
	}
}
