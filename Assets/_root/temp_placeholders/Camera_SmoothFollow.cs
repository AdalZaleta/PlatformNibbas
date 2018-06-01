using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_SmoothFollow : MonoBehaviour {

	public GameObject target;

	public float smoothSpeed;
	public Vector3 offset;
	public GameObject bg;
	public float ImgX;
	public float ImgY;
	public float CenterX;
	public float CenterY;
	public Vector3 XRange_Start;
	public Vector3 XRange_End;
	public Vector3 YRange_Start;
	public Vector3 YRange_End;

	void Start()
	{
		ImgX = bg.GetComponent<SpriteRenderer> ().size.x * bg.transform.localScale.x;
		ImgY = bg.GetComponent<SpriteRenderer> ().size.y * bg.transform.localScale.y;
		CenterX = bg.transform.position.x;
		CenterY = bg.transform.position.y;
		XRange_Start = new Vector3 ((CenterX - (ImgX / 2)), CenterY, 2);
		XRange_End = new Vector3 ((CenterX + (ImgX / 2)), CenterY, 2);
		YRange_Start = new Vector3 (CenterX, (CenterY - (ImgY / 2)), 2);
		YRange_End = new Vector3 (CenterX, (CenterY + (ImgY / 2)), 2);
	}

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
