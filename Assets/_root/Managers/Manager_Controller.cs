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
		[SerializeField]
		private LayerMask layer;
		[SerializeField]
		private Vector3 originJump;

		public float valueOfTime;
		public float distanceShoot;

		public bool Atack = true;

		private RaycastHit2D hitInfo;


		public float PotenciaSalto;
		public float PotenciaCaida;

		void Awake()
		{
			Manager_Static.controllerManager = this;
			Principal_PJ = GameObject.FindGameObjectWithTag ("Player");
			//GOSalto = Principal_PJ.GetComponentsInChildren<Transform> ();
		}


		public void MoveCharacter(float _x, float _y)
		{
			Principal_PJ.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (_x * Speed,
				Principal_PJ.gameObject.GetComponent<Rigidbody2D> ().velocity.y);
			//Principal_PJ.transform.Translate (new Vector3 (_x, _y, 0.0f) * Time.deltaTime * Speed, Space.World);
		}

		public void JumpCharacter()
		{
			originJump = Principal_PJ.transform.position - new Vector3 (0.1f, 1.15f, 0.0f);
			if (Physics2D.Raycast (originJump, Vector2.right, 0.3f, layer)) {
				Debug.DrawRay (originJump, Vector2.right * 0.5f, Color.black, 2);
				Debug.Log("Toque");
				Principal_PJ.GetComponent <Rigidbody2D> ().velocity = Vector2.up * PotenciaSalto;
			}
		}

		public void SmashCharacter(){
			Principal_PJ.GetComponent <Rigidbody2D> ().velocity = Vector2.down * PotenciaCaida;
		}

		public void ResetPosition()
		{
			if (Physics2D.OverlapPoint (GOSalto.position)) {
				Principal_PJ.gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			}
		}

		public void AtackCharacter()
		{
			if (Atack) 
			{
				Debug.Log ("Input Ataque");
				StartCoroutine (Trow (4.0f));
			}
			else
			{
				Debug.Log ("Ya esta atacando, Ignora Input");
			}
		}

		public void PlayNote(int _i)
		{
			
		}

		public Transform ob;

		IEnumerator Trow(float _duration)
		{
			Debug.Log ("Entre a la corrutina");
			Atack = false;
			float currentTime = Time.time;

			while (Time.time < (currentTime + _duration)) {
				valueOfTime = Mathf.InverseLerp (currentTime, currentTime + _duration, Time.time);	
				distanceShoot = Principal_PJ.GetComponent<TeasHolder> ().curvaDeLanzar.Evaluate (valueOfTime);
				distanceShoot *= 5;
				hitInfo = Physics2D.Raycast (Principal_PJ.transform.position, Principal_PJ.transform.right, distanceShoot, layer);
				if (hitInfo) {
					Debug.DrawLine (Principal_PJ.transform.position, hitInfo.point, Color.green);
				} else {
					Debug.DrawRay (Principal_PJ.transform.position, Principal_PJ.transform.right*distanceShoot, Color.blue);
				}
				ob.transform.position = Principal_PJ.transform.position + Principal_PJ.transform.right * distanceShoot;
				yield return null;
			}
			Atack = true;
			Debug.Log ("Sale de la corrutina");
		}
	}
}
