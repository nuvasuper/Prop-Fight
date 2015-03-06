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
	public float xMin = 0f;
	public float xMax = 90f;
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
		float predictedX = c.rotation.eulerAngles.x + x;
		if (predictedX > 360)
			predictedX -= 360;

		if (predictedX > 90) {
			c.rotation = Quaternion.Euler (90, c.rotation.eulerAngles.y, c.rotation.eulerAngles.z);
		} else if (predictedX < 0) {
			c.rotation = Quaternion.Euler (0, c.rotation.eulerAngles.y, c.rotation.eulerAngles.z);
		} else {
			c.Rotate (x, 0, 0);
		}
		x = Mathf.Deg2Rad*c.rotation.eulerAngles.x;
		y = -1*Mathf.Deg2Rad*t.rotation.eulerAngles.y;
		//Debug.Log (y);
		float newY = Mathf.Sin (x) * distance + parent.position.y;
		float distance2 = Mathf.Cos (x) * distance;
		float newX = Mathf.Sin (y) * distance2 + parent.position.x;
		float newZ = -1 * distance2 * Mathf.Cos (y) + parent.position.z;
		t.position = new Vector3 (newX, newY, newZ);

	}

	private float restrictX (float x) {
		if (c.rotation.eulerAngles.x > 180) {
			c.rotation = Quaternion.Euler (0, c.rotation.eulerAngles.y, c.rotation.eulerAngles.z);
			return Mathf.Min (0, x);
		}
		if (c.rotation.eulerAngles.x > 90) {
			c.rotation = Quaternion.Euler (90, c.rotation.eulerAngles.y, c.rotation.eulerAngles.z);
			return Mathf.Min (x, 0);
		}
		return x;
	}
}
