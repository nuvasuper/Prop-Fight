using UnityEngine;
using System.Collections;

public class characterSelect : MonoBehaviour {
	public PhotonManager pm;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		if (GUI.Button (new Rect (50, 50, 100, 50), "Chair"))
			pm.pickPrefab ("Chair");
		if (GUI.Button (new Rect (50, 100, 100, 50), "Barrel"))
			pm.pickPrefab ("Barrel");
		if (GUI.Button (new Rect (50, 150, 100, 50), "Bucket"))
			pm.pickPrefab ("Bucket");
		if (GUI.Button (new Rect (50, 200, 100, 50), "Crate"))
			pm.pickPrefab ("Crate");
		//if (GUI.Button (new Rect (50, 250, 100, 50), "Fire Hydrant"))
		//    pm.pickPrefab ("Fire Hydrant");
		if (GUI.Button (new Rect (50, 250, 100, 50), "Present Box"))
			pm.pickPrefab ("Present Box");
		if (GUI.Button (new Rect (50, 300, 100, 50), "Revolver"))
			pm.pickPrefab ("Revolver");
		if (GUI.Button (new Rect (50, 350, 100, 50), "Road Block"))
			pm.pickPrefab ("Road Block");
		if (GUI.Button (new Rect (50, 400, 100, 50), "Table"))
			pm.pickPrefab ("Table");


	}
}
