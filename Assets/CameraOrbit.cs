using UnityEngine;
using System.Collections;

public class CameraOrbit : MonoBehaviour {
	float distance = 5.0f;
	private Transform parent;
	private Transform t;
	private Vector3 delta;
	private Vector3 last;

	private float xSpeed = 120.0f;
	private float ySpeed = 120.0f;
	private float x;
	private float y;
	// Use this for initialization
	void Start () {
		//Screen.lockCursor = true;
		t = this.transform;
		this.parent = t.parent;
		t.parent = null;
		last = parent.position;
		t.Translate(0, 0, -1*distance);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		delta = parent.position - last;
		t.Translate (delta);
		last = parent.position;

		//Quaternion rotation = Quaternion.Euler(y, x, 0);
		distance = Input.GetAxis("Mouse ScrollWheel");
		t.Translate(0,0,distance);

		//x = Input.GetAxis("Mouse X") * xSpeed;
		//y = Input.GetAxis("Mouse Y") * ySpeed;

		//y = ClampAngle(y, yMinLimit, yMaxLimit);
		//t.RotateAround (parent.position, Vector3.up, x);
		//t.RotateAround (parent.position, Vector3.left, y);
		//x = 0f;
		//y = 0f;
	}

	private float ClampAngle (float angle, float min,float max) {
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}
}
