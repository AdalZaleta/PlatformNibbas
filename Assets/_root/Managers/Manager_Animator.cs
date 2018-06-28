using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAAI
{
	public class Manager_Animator : MonoBehaviour {

		void Awake()
		{
			Manager_Static.animatorManager = this;
		}

		void setVelocity(float _x, float _y)
		{
			
		}
	}
}