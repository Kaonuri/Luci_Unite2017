using TC;
using UnityEngine;
using System.Collections;

public class ForcesCam : MonoBehaviour
{
	public Transform target;
	public float distance = 5.0f;
	public float xSpeed = 120.0f;
	public float ySpeed = 120.0f;

	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;

	public float distanceMin = .5f;
	public float distanceMax = 15f;

	float x = 0.0f;
	float y = 0.0f;

	public TCForce forceTarget;

	// Use this for initialization
	void Start()
	{
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;

		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;

		if (forceTarget != null) {
			forceTarget.enabled = false;
		}
	}

	void LateUpdate()
	{
		//TODO: To seperate function
		if (forceTarget != null) {

			if (Input.GetMouseButton(0)) {
				forceTarget.enabled = true;
				Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
				Plane plane = new Plane(Vector3.Cross(transform.up, transform.right), target.transform.position);

				float dist = 0.0f;
				if (plane.Raycast(ray, out dist))
					forceTarget.transform.position = transform.position + ray.direction * dist;
			} else {
				forceTarget.enabled = false;
			}

			if (Input.GetKey(KeyCode.Alpha1)) {
				forceTarget.forceType = ForceType.Radial;
				forceTarget.power = 60.0f;
			} else if (Input.GetKey(KeyCode.Alpha2)) {
				forceTarget.forceType = ForceType.Radial;
				forceTarget.power = -60.0f;
			} else if (Input.GetKey(KeyCode.Alpha3)) {
				forceTarget.forceType = ForceType.Vortex;
				forceTarget.power = 60.0f;
			} else if (Input.GetKey(KeyCode.Alpha4)) {
				forceTarget.forceType = ForceType.Turbulence;
				forceTarget.power = 60.0f;
			} else if (Input.GetKey(KeyCode.Alpha5)) {
				forceTarget.forceType = ForceType.Vector;
				forceTarget.power = 60.0f;
			} else if (Input.GetKey(KeyCode.Alpha6)) {
				forceTarget.forceType = ForceType.Drag;
				forceTarget.power = 60.0f;
			}
		}

		if (target && (Input.GetKey(KeyCode.LeftControl) || forceTarget == null))
		{
			x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
			y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

			y = ClampAngle(y, yMinLimit, yMaxLimit);
		}

		Quaternion rotation = Quaternion.Euler(y, x, 0);

		distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 25, distanceMin, distanceMax);


		Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
		Vector3 position = rotation * negDistance + target.position;

		transform.rotation = rotation;
		transform.position = position;

	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}


}