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
	public float Threshold;
	public bool followAll;

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

		if (target.transform.position.y > Threshold && !followAll)
		{
			followAll = true;
		}
		if (followAll)
		{
			Vector3 smoothedPos = Vector3.Lerp (transform.position, finalPos, smoothSpeed * Time.deltaTime);
			transform.position = finalPos;

			if (target.transform.position.y < Threshold && target.GetComponent<Temp_Movement>().canJump && (Mathf.Abs(transform.position.y - smoothedPos.y)) < 0.1f)
			{
				followAll = false;
			}
		}
		else
		{
			Vector3 smoothedPos = Vector3.Lerp (transform.position, finalPos, smoothSpeed * Time.deltaTime);
			transform.position = new Vector3 (smoothedPos.x, transform.position.y, finalPos.z);
		}

		Debug.DrawLine (new Vector3 (XRange_Start.x, Threshold, 0), new Vector3 (XRange_End.x, Threshold, 0), Color.red);
	}
}
