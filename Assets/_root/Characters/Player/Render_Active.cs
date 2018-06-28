using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Render_Active : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (transform.localPosition == Vector3.zero) 
		{
			Debug.Log ("Entre al if");
			gameObject.GetComponent<Renderer> ().enabled = false;
		}
		if(transform.localPosition != Vector3.zero)
		{
			gameObject.GetComponent<Renderer> ().enabled = true;
		}
	}
}
