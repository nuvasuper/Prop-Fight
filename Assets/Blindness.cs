using UnityEngine;
using System.Collections;

public class Blindness : MonoBehaviour {
	public Transform victim;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public IEnumerator setVictim(Transform newVictim, float blindTime) {
		victim = newVictim;
		transform.parent=victim;
		transform.localRotation = Quaternion.identity;
		transform.localPosition = new Vector3(0,.1f,0);
		yield return new WaitForSeconds(blindTime);
		reset();

	}

	public void reset() {
		transform.parent = null;
		transform.position = new Vector3 (0, -10, 0);
	}
}
