using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {

	Vector3 realPosition;
	Quaternion realRotation;
	Vector3 realVelocity;
	Vector3 realAngular;

	Rigidbody r;
	float ping = 0.1f;

	// Use this for initialization
	void Start () {
		r = this.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (photonView.isMine) {

		} else {
			r.position =  Vector3.Lerp(r.position,realPosition,ping);
			r.rotation =  Quaternion.Lerp(r.rotation,realRotation,ping);
			r.velocity = Vector3.Lerp(r.velocity,realVelocity,ping);
			r.angularVelocity = Vector3.Lerp(r.angularVelocity,realAngular,ping);
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext (r.position);
			stream.SendNext (r.rotation);
			stream.SendNext(r.velocity);
			stream.SendNext(r.angularVelocity);
		} else {
			realPosition = (Vector3) stream.ReceiveNext();
			realRotation = (Quaternion) stream.ReceiveNext();
			realVelocity = (Vector3) stream.ReceiveNext();
			realAngular = (Vector3) stream.ReceiveNext();
		}
	}
}
