using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Notes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("Desabilitar", 0.4f);
	}

	void Desabilitar()
	{
		gameObject.SetActive (false);
	}
}
