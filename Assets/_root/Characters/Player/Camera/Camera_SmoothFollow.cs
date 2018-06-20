using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAAI
{
	public class Camera_SmoothFollow : MonoBehaviour {

		public GameObject watcher;
		public float smoothSpeed;
		public float Y_Threshold;
		public GameObject bg;
		Vector3 offset;
		public Camera cam;
		Vector2 target;

		bool defTarget = true;

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

		void ChangeTarget(GameObject _col)
		{
			Debug.Log ("Entered Change with: " + _col.gameObject.name);
			if (_col.gameObject.CompareTag("camFix"))
			{
				defTarget = false;
				target = _col.gameObject.transform.position;
			}
			if (_col.gameObject.CompareTag("camFocus"))
			{
				defTarget = false;
				float watchX = watcher.transform.position.x;
				float watchY = watcher.transform.position.y;
				Vector2 newpos;
				newpos.x = (watchX + (_col.gameObject.transform.position.x - watchX) / 2);
				newpos.y = (watchY + (_col.gameObject.transform.position.y - watchY) / 2);
				target = newpos;
			}
		}

		void ResetPosition()
		{
			defTarget = true;
		}

		void Start()
		{
			
			offset = new Vector3 (0, 0, -10);
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
			if (defTarget)
				target = watcher.transform.position;
			// Update Camera Limits
			camXlim.x = transform.position.x - CamX / 2;
			camXlim.y = transform.position.x + CamX / 2;
			camYlim.x = transform.position.y - CamY / 2;
			camYlim.y = transform.position.y + CamY / 2;

			Vector3 finalPos = new Vector3(target.x, target.y, 0) + offset;

			if (camXlim.x <= bgXlim.x)
			{
				if (target.x < transform.position.x)
					finalPos.x = transform.position.x;
			}

			if (camXlim.y >= bgXlim.y)
			{
				if (target.x > transform.position.x)
					finalPos.x = transform.position.x;
			}

			if (camYlim.x <= bgYlim.x)
			{
				if (target.y < transform.position.y)
					finalPos.y = transform.position.y;
			}

			if (camYlim.y >= bgYlim.y)
			{
				if (target.y > transform.position.y)
					finalPos.y = transform.position.y;
			}

			if (target.y > Y_Threshold && !followAll)
			{
				followAll = true;
			}
			if (followAll)
			{
				Vector3 smoothedPos = Vector3.Lerp (transform.position, finalPos, smoothSpeed * Time.deltaTime);
				transform.position = new Vector3 (smoothedPos.x, smoothedPos.y, finalPos.z);

				if ((target.y < Y_Threshold && (Mathf.Abs(transform.position.y - smoothedPos.y)) < 0.1f && camYlim.x <= bgYlim.x) /*|| (target.transform.position.y < Threshold && camYlim.x <= bgYlim.x && (Mathf.Abs(transform.position.y - smoothedPos.y)) < 0.1f)*/)
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