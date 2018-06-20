using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Fix : MonoBehaviour {

	public GameObject toSend;
	public GameObject cam;

	void OnTriggerStay2D(Collider2D _col)
	{
		if (_col.gameObject.CompareTag("watcher"))
		{
			cam.gameObject.SendMessage ("ChangeTarget", toSend, SendMessageOptions.DontRequireReceiver);
		}
	}

	void OnTriggerExit2D(Collider2D _col)
	{
		if (_col.gameObject.CompareTag("watcher"))
		{
			cam.gameObject.SendMessage ("ResetPosition", SendMessageOptions.DontRequireReceiver);
		}
	}
}
