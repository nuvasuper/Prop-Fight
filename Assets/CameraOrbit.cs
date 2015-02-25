using UnityEngine;
using System.Collections;

public class CameraOrbit : MonoBehaviour {
	float distance = 10.0f;
	private Transform parent;
	private Transform t;

	private float xSpeed = 2.0f;
	private float ySpeed = 2.0f;
	private float x;
	private float y;
	private Transform c;
	// Use this for initialization
	void Start () {
		//Screen.lockCursor = true;
		c = this.transform.GetChild (0);
		t = this.transform;
		this.parent = t.parent;
		t.parent = null;
		t.Translate(0, 0, -1*distance);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		//delta = parent.position - last;
		//t.Translate (delta);
		//last = parent.position;

		//Quaternion rotation = Quaternion.Euler(y, x, 0);
		distance += Input.GetAxis("Mouse ScrollWheel");
		//t.Translate(0,0,distance);

		x = Input.GetAxis("Mouse Y") * ySpeed;
		y = Input.GetAxis("Mouse X") * xSpeed;

		//y = ClampAngle(y, yMinLimit, yMaxLimit);
		t.Rotate (0, y,0);
		c.Rotate (x, 0,0);
		x = Mathf.Deg2Rad*c.rotation.eulerAngles.x;
		y = -1*Mathf.Deg2Rad*t.rotation.eulerAngles.y;
		//Debug.Log (y);
		float newY = Mathf.Sin (x) * distance + parent.position.y;
		float distance2 = Mathf.Cos (x) * distance;
		float newX = Mathf.Sin (y) * distance2 + parent.position.x;
		float newZ = -1 * distance2 * Mathf.Cos (y) + parent.position.z;
		t.position = new Vector3 (newX, newY, newZ);

	}

	private float ClampAngle (float angle, float min,float max) {
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}
}
