using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace TAAI
{
	public class Manager_Input : MonoBehaviour {

		void Awake()
		{
			Manager_Static.inputManager = this;
		}

		void Update()
		{
			//CODIGO DE LOS INPUTS DEPENDIENDO DEL ESTADO DEL JUEGO
			if (Manager_Static.appManager.currentState == AppState.main_menu) {
			}
			if (Manager_Static.appManager.currentState == AppState.gameplay) {

				Debug.Log ("DpadX: " + Input.GetAxisRaw ("D_Pad_X"));
				Debug.Log ("DpadY: " + Input.GetAxisRaw ("D_Pad_Y"));

				if (Input.GetAxis ("Horizontal") != 0.0f) {
					//Debug.Log (Input.GetAxis ("Horizontal"));
					Manager_Static.controllerManager.MoveCharacter (Input.GetAxisRaw ("Horizontal"), 0.0f);
					Manager_Static.animatorManager.setVelocity (Input.GetAxisRaw ("Horizontal"), 0.0f);
				}
				if (Input.GetAxis ("Vertical") <= -0.3f) {
					//Debug.Log (Input.GetAxis ("Horizontal"));
					Manager_Static.controllerManager.SmashCharacter();
				}
				if (Input.GetButton ("Fire1")) {
					Manager_Static.controllerManager.JumpCharacter ();
				}
				if (Input.GetButtonDown ("Fire3")) {
					Manager_Static.controllerManager.AtackCharacter ();
				}
				if (Input.GetButtonDown ("Fire3")) {
					Manager_Static.controllerManager.AtackMele ();
				}
				if (Input.GetAxisRaw ("Horizontal") == 0.0f && !Input.GetButton ("Fire1")) {
					Manager_Static.controllerManager.ResetPosition ();
					Manager_Static.animatorManager.setVelocity (Input.GetAxisRaw ("Horizontal"), 0.0f);
				}
				if (Input.GetAxisRaw ("D_Pad_X") == -1) 
				{
					Manager_Static.controllerManager.PlayNote (0);
				}
				if (Input.GetAxisRaw ("D_Pad_X") == 1) 
				{
					Manager_Static.controllerManager.PlayNote (2);
				}
				if (Input.GetAxisRaw ("D_Pad_Y") == 1) {
					Manager_Static.controllerManager.PlayNote (1);
				}
			}
			if (Manager_Static.appManager.currentState == AppState.end_game) {
			}
		}

		public delegate void InputTemplate (int _id);

		public InputTemplate AchievementHandler;
		public InputTemplate DecalChangeHandler;
		public InputTemplate DecalHandler;
	}
}
