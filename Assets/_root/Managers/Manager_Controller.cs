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
		private Vector3 originJump;
		[SerializeField]
		private Vector3 shootDirection;
		[SerializeField]
		private Transform Weapon;

		public Vector3 tepPosArma;


		public float valueOfTime;
		public float distanceShoot;
		public float DurationShoot;
		public float LenghtShoot;

		public bool canAtack = true;

		private RaycastHit2D hitInfo;


		public float PotenciaSalto;
		public float PotenciaCaida;

		void Awake()
		{
			Manager_Static.controllerManager = this;
			Principal_PJ = GameObject.FindGameObjectWithTag ("Player");
			GOSalto = Principal_PJ.GetComponentInChildren<Transform> ().GetChild (0);
		}


		public void MoveCharacter(float _x, float _y)
		{
			Principal_PJ.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (_x * Speed,
				Principal_PJ.gameObject.GetComponent<Rigidbody2D> ().velocity.y);
			shootDirection = new Vector3 (1, 0, 0) * (Mathf.Abs (_x) / _x);
			Debug.Log ("Valor de shootDirection: " + shootDirection);
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
			if (Physics2D.OverlapPoint (GOSalto.position,layer)) {
				Principal_PJ.gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			}
		}

		public void AtackCharacter()
		{
			if (canAtack) 
			{
				Debug.Log ("Input Ataque");
				StartCoroutine (Trow (DurationShoot));
			}
			else
			{
				Debug.Log ("Ya esta atacando, Ignora Input");
			}
		}

		public void PlayNote(int _i)
		{
			
		}

		IEnumerator Trow(float _duration)
		{
			Debug.Log ("Entre a la corrutina");
			canAtack = false;
			float currentTime = Time.time;
			Vector3 positionShoot = Principal_PJ.transform.position;
			Vector3 currentShootDirection = shootDirection;

			while (Time.time < (currentTime + _duration)) {
				valueOfTime = Mathf.InverseLerp (currentTime, currentTime + _duration, Time.time);	
				distanceShoot = Principal_PJ.GetComponent<TeasHolder> ().curvaDeLanzar.Evaluate (valueOfTime);
				distanceShoot *= LenghtShoot;
				hitInfo = Physics2D.Raycast (positionShoot, currentShootDirection, distanceShoot, layer);
				if (hitInfo) {
					Debug.DrawLine (positionShoot, hitInfo.point, Color.green);
				} else {
					Debug.DrawRay (positionShoot, currentShootDirection, Color.blue);
				}
				Weapon.position = positionShoot + currentShootDirection * distanceShoot;
				tepPosArma = Weapon.position;
				yield return null;
				Weapon.localPosition = Vector3.zero;
			}
			canAtack = true;
			Debug.Log ("Sale de la corrutina");
		}
	}
}
