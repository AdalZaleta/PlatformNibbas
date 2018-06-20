﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

	void OnCollisionEnter(Collision _col)
	{
		if (_col.gameObject.CompareTag("Player"))
		{
			Debug.Log ("Trigger Player Death");
			_col.gameObject.SendMessage ("Death", SendMessageOptions.DontRequireReceiver);
		}
	}
}