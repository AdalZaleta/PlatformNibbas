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
				if (Input.GetAxis ("Horizontal") != 0.0f) {
					//Debug.Log (Input.GetAxis ("Horizontal"));
					Manager_Static.controllerManager.MoveCharacter (Input.GetAxisRaw ("Horizontal"), 0.0f);
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
				if (Input.GetAxisRaw ("Horizontal") == 0.0f && !Input.GetButton ("Fire1")) {
					Manager_Static.controllerManager.ResetPosition ();
				}
			}
			if (Manager_Static.appManager.currentState == AppState.end_game) {
			}
		}
			
	}
}
