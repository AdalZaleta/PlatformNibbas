using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAAI
{
	public class Change : MonoBehaviour {

		void Start () {
			Invoke ("EndLoad", 5.0f);
		}

		void EndLoad()
		{
			Manager_Static.sceneManager.LoadSceneName ("cueva");
		}
	}
}
