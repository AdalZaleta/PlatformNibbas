using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAAI
{
	public class Camera_SmoothFollow : MonoBehaviour {

		public GameObject target;
		public Camera cam;
		public float smoothSpeed;
		public Vector3 offset;
		public float Y_Threshold;
		public GameObject bg;

		float ImgX;
		float ImgY;
		float CenterX;
		float CenterY;
		bool followAll;
		float CamX;
		float CamY;
		Vector2 camXlim;
		Vector2 camYlim;
		Vector2 bgXlim;
		Vector2 bgYlim;

		void Start()
		{
			ImgX = bg.GetComponent<SpriteRenderer> ().size.x * bg.transform.localScale.x;
			ImgY = bg.GetComponent<SpriteRenderer> ().size.y * bg.transform.localScale.y;
			CenterX = bg.transform.position.x;
			CenterY = bg.transform.position.y;
			CamY = 2.0f * cam.orthographicSize;
			CamX = CamY * cam.aspect;

			bgXlim.x = CenterX - ImgX / 2;
			bgXlim.y = CenterX + ImgX / 2;
			bgYlim.x = CenterY - ImgY / 2;
			bgYlim.y = CenterY + ImgY / 2;
		}

		void FixedUpdate()
		{
			// Update Camera Limits
			camXlim.x = transform.position.x - CamX / 2;
			camXlim.y = transform.position.x + CamX / 2;
			camYlim.x = transform.position.y - CamY / 2;
			camYlim.y = transform.position.y + CamY / 2;

			Vector3 finalPos = target.transform.position + offset;

			if (camXlim.x <= bgXlim.x)
			{
				if (target.transform.position.x < transform.position.x)
					finalPos.x = transform.position.x;
			}

			if (camXlim.y >= bgXlim.y)
			{
				if (target.transform.position.x > transform.position.x)
					finalPos.x = transform.position.x;
			}

			if (camYlim.x <= bgYlim.x)
			{
				if (target.transform.position.y < transform.position.y)
					finalPos.y = transform.position.y;
			}

			if (camYlim.y >= bgYlim.y)
			{
				if (target.transform.position.y > transform.position.y)
					finalPos.y = transform.position.y;
			}

			if (target.transform.position.y > Y_Threshold && !followAll)
			{
				followAll = true;
			}
			if (followAll)
			{
				Vector3 smoothedPos = Vector3.Lerp (transform.position, finalPos, smoothSpeed * Time.deltaTime);
				transform.position = new Vector3 (smoothedPos.x, smoothedPos.y, finalPos.z);

				if ((target.transform.position.y < Y_Threshold && (Mathf.Abs(transform.position.y - smoothedPos.y)) < 0.1f && camYlim.x <= bgYlim.x) /*|| (target.transform.position.y < Threshold && camYlim.x <= bgYlim.x && (Mathf.Abs(transform.position.y - smoothedPos.y)) < 0.1f)*/)
				{
					followAll = false;
				}
			}
			else
			{
				Vector3 smoothedPos = Vector3.Lerp (transform.position, finalPos, smoothSpeed * Time.deltaTime);
				transform.position = new Vector3 (smoothedPos.x, transform.position.y, finalPos.z);
			}

			Debug.DrawLine (new Vector3 (bgXlim.x, transform.position.y, transform.position.z), new Vector3 (bgXlim.y, transform.position.y, transform.position.z), Color.red);
			Debug.DrawLine (new Vector3 (transform.position.x, bgYlim.x, transform.position.z), new Vector3 (transform.position.x, bgYlim.y, transform.position.z), Color.red);

			Debug.DrawLine (new Vector3 (transform.position.x - CamX/2, transform.position.y, transform.position.z), new Vector3 (transform.position.x + CamX / 2, transform.position.y, transform.position.z), Color.magenta);
			Debug.DrawLine (new Vector3 (transform.position.x, transform.position.y - CamY / 2, transform.position.z), new Vector3 (transform.position.x, transform.position.y + CamY / 2, transform.position.z), Color.green);
		}
	}
}