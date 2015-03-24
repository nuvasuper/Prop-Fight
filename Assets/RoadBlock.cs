using UnityEngine;
using System.Collections;

public class RoadBlock : MonoBehaviour {

	public float lastFlux;
	public float fluxRate;
	public bool canFlux=true;
	public Rigidbody r;
	// Use this for initialization
	void Start () {
		lastFlux = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftControl)&&canFlux) {
			float flux = (Time.time-lastFlux)*fluxRate;
			StartCoroutine(useFlux(flux));
		}
	}

	[RPC]
	void updateMass(float mass) {
		r.mass = mass;
	}

	IEnumerator useFlux(float flux) {//seems to be working
		print ("flux: " + flux);
		canFlux = false;
		float mass = r.mass+flux;
		this.gameObject.GetPhotonView().RPC ("updateMass",PhotonTargets.All,mass);
		yield return new WaitForSeconds(5);
		mass-=flux;
		this.gameObject.GetPhotonView().RPC("updateMass",PhotonTargets.All,mass);
		canFlux = true;
		//values are arbitrary
	}
}
