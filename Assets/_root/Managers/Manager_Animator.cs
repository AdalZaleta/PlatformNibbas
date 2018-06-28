using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAAI
{
	public class Manager_Animator : MonoBehaviour {

		public Animator Pj_Principal;

		void Awake()
		{
			Manager_Static.animatorManager = this;
		}

		public void setVelocity(float _x, float _y)
		{
			Pj_Principal.SetFloat ("Walking", _x);
		}
	}
}