using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAAI
{
	//Player controller (Si fuera una IA, seria un script similar a este, pero que en lugar de usar el input manager, usa su propia logica de IA para mover un character)
	public class Manager_Controller : MonoBehaviour 
	{
		public GameObject Principal_PJ;

		void Awake()
		{
			Manager_Static.controllerManager = this;
			Principal_PJ = GameObject.FindGameObjectWithTag ("Player");
		}


		public void MoveCharacter(float _x, float _y)
		{
			Principal_PJ.GetComponent<TeasHolder> ().Move (_x);
		}

		public void JumpCharacter()
		{
			Principal_PJ.GetComponent<TeasHolder> ().Jump ();
		}

		public void SmashCharacter()
		{
			Principal_PJ.GetComponent<TeasHolder> ().Smash ();
		}

		public void ResetPosition()
		{
			Principal_PJ.GetComponent <TeasHolder> ().ResetPosition ();
		}

		public void AtackCharacter()
		{
			Principal_PJ.GetComponent <TeasHolder> ().Atack ();
		}

		public void PlayNote(int _i)
		{
			
		}
			
	}
}
