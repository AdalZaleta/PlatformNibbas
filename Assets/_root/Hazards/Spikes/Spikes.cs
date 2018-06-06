using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

	public float dmg;

	void OnCollisionEnter(Collision _col)
	{
		if (_col.gameObject.CompareTag("Player"))
		{
			_col.gameObject.SendMessage ("receiveDamage", dmg, SendMessageOptions.DontRequireReceiver);
		}
	}
}
