using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

	public enum type
	{
		spikes,
		voidhole
	}

	public type hazardType;

	void OnCollisionEnter(Collision _col)
	{
		switch (hazardType)
		{
		case type.spikes:
			if (_col.gameObject.CompareTag ("Player")) {
				Debug.Log ("Trigger Player Death");
				_col.gameObject.SendMessage ("Dead", SendMessageOptions.DontRequireReceiver);
			}
			break;

		case type.voidhole:
			if (_col.gameObject.CompareTag ("Player")) {
				Debug.Log ("Triggered VOID");
			}
			break;
		}
	}
}
