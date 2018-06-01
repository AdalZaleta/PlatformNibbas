using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_SmoothFollow : MonoBehaviour {

	public GameObject target;

	public float smoothSpeed;
	public Vector3 offset;

	void FixedUpdate()
	{
		Vector3 finalPos = target.transform.position + offset;
		Vector3 smoothedPos = Vector3.Lerp (transform.position, finalPos, smoothSpeed*Time.deltaTime);
		if (finalPos.y <= 10)
		{
			Vector3 currentPos = new Vector3 (smoothedPos.x, finalPos.y, finalPos.z);
			transform.position = currentPos;
		}
		if (finalPos.y > 10)
		{
			Vector3 currentPos = new Vector3 (smoothedPos.x, smoothedPos.y, finalPos.z);
			transform.position = currentPos;
		}
	}
}
