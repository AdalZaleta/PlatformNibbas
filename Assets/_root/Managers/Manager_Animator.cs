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

		public void setJump(Vector2 _velocity)
		{
			Pj_Principal.SetFloat ("Jump", _velocity.y);
		}

		public void setAtacck()
		{
			Pj_Principal.SetTrigger ("Mele");
		}

		public void setThrow()
		{
			Pj_Principal.SetTrigger ("Throw");
		}

		public void setPlay(bool _isPlaying)
		{
			Pj_Principal.SetBool ("Playing", _isPlaying);
		}

		public void TakeDamage()
		{
			Pj_Principal.SetTrigger ("Damage");
		}

		public void setGrab(bool _isGrab)
		{
			Pj_Principal.SetBool ("Grab", _isGrab);
		}
	}
}