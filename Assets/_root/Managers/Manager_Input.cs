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
			if (Manager_Static.appManager.currentState == AppState.pause_menu) {
				if (Input.GetKeyUp (KeyCode.JoystickButton7)) {
					Manager_Static.appManager.setPlay ();
				}
			}
			else if (Manager_Static.appManager.currentState == AppState.main_menu) {
			}
			else if (Manager_Static.appManager.currentState == AppState.gameplay) {
				if (Input.GetAxis ("Horizontal") != 0.0f) {
					//Debug.Log (Input.GetAxis ("Horizontal"));
					Manager_Static.controllerManager.MoveCharacter (Input.GetAxisRaw ("Horizontal"), 0.0f);
					Manager_Static.animatorManager.setVelocity (Input.GetAxisRaw ("Horizontal"), 0.0f);
					Manager_Static.animatorManager.setPlay (false);
				}
				if (Input.GetAxis ("Vertical") <= -0.3f) {
					//Debug.Log (Input.GetAxis ("Horizontal"));
					Manager_Static.controllerManager.SmashCharacter();
				}
				if (Input.GetButton ("Fire1")) {
					Manager_Static.controllerManager.JumpCharacter ();
					Manager_Static.animatorManager.setPlay (false);
				}
				if (Input.GetButtonDown ("Fire3")) {
					Manager_Static.controllerManager.AtackCharacter ();
					Manager_Static.animatorManager.setPlay (false);
				}
				if (Input.GetButtonDown ("Fire2")) {
					Manager_Static.controllerManager.AtackMele ();
					Manager_Static.animatorManager.setPlay (false);
					Manager_Static.animatorManager.setAtacck ();
				}
				if (Input.GetAxisRaw ("Horizontal") == 0.0f && !Input.GetButton ("Fire1")) {
					Manager_Static.controllerManager.ResetPosition ();
					Manager_Static.animatorManager.setVelocity (Input.GetAxisRaw ("Horizontal"), 0.0f);
				}
				if (Input.GetAxisRaw ("D_Pad_X") == -1)
				{
					Manager_Static.animatorManager.setPlay (true);
					Manager_Static.controllerManager.PlayNote (0);
				}
				if (Input.GetAxisRaw ("D_Pad_X") == 1)
				{
					Manager_Static.animatorManager.setPlay (true);
					Manager_Static.controllerManager.PlayNote (2);
				}
				if (Input.GetAxisRaw ("D_Pad_Y") == 1)
				{
					Manager_Static.animatorManager.setPlay (true);
					Manager_Static.controllerManager.PlayNote (1);
				}
				if (Input.GetKeyUp (KeyCode.JoystickButton7)) {
					Manager_Static.appManager.SetPause ();
				}
			}
			else if (Manager_Static.appManager.currentState == AppState.end_game) {
			}
		}

		public delegate void InputTemplate (int _id);

		public InputTemplate AchievementHandler;
		public InputTemplate DecalChangeHandler;
		public InputTemplate DecalHandler;
	}
}
