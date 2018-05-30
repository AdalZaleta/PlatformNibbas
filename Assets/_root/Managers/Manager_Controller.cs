using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAAI
{
	//Player controller (Si fuera una IA, seria un script similar a este, pero que en lugar de usar el input manager, usa su propia logica de IA para mover un character)
	public class Manager_Controller : MonoBehaviour 
	{
		[SerializeField]
		public GameObject Principal_PJ;
		[SerializeField]
		public Transform GOSalto;
		[SerializeField]
		private float Speed;

		public float MutliplicadorSalto;
		public float PotenciaSalto;
		public float MutliplicadorCaida;

		void Awake()
		{
			Manager_Static.controllerManager = this;
			Principal_PJ = GameObject.FindGameObjectWithTag ("Player");
			//GOSalto = Principal_PJ.GetComponentsInChildren<Transform> ();
		}


		public void MoveCharacter(float _x, float _y)
		{
			Principal_PJ.transform.Translate (new Vector3 (_x, _y, 0.0f) * Time.deltaTime * Speed, Space.World);
		}

		public void JumpCharacter()
		{
			if (Physics2D.OverlapPoint (GOSalto.position)) {
				Principal_PJ.GetComponent <Rigidbody2D> ().velocity = Vector2.up * PotenciaSalto;
			}
		}
	}
}
