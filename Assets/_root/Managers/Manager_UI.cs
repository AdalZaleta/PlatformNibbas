using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TAAI
{
	public class Manager_UI : MonoBehaviour {

		public GameObject Pausa;

		void Awake()
		{
			Manager_Static.uiManager = this;
		}

		public void isInPause(bool _isIt)
		{
			Pausa.gameObject.SetActive (_isIt);
		}
	}
}