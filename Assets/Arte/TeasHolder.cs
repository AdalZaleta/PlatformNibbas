using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAAI
{
	public class TeasHolder : MonoBehaviour {

		public bool canAtack = true;

		public Sprite[] cosos;
		public SpriteRenderer who;
		public Rigidbody2D what;
		public Transform Watch;
		public Transform GOSalto;

		private Vector3 originJump;

		[SerializeField]
		private int Health;
		[SerializeField]
		private LayerMask layerJump;
		[SerializeField]
		private LayerMask layerCanAtack;
		[SerializeField]
		private float Speed;
		[SerializeField]
		private Vector3 shootDirection;
		[SerializeField]
		private Transform Weapon;

		public Vector3[] offsetMusic;
		public Vector3 PosWacher = new Vector3(2.5f, 0.0f, 0.0f);
		public AnimationCurve curvaDeLanzar;

		public float PotenciaSalto;
		public float PotenciaCaida;

		public float valueOfTime;
		public float distanceShoot;
		public float DurationShoot;
		public float LenghtShoot;

		private RaycastHit2D hitInfo;

		void Awake()
		{
			GOSalto = gameObject.GetComponentInChildren<Transform> ().GetChild (0);
		}


		void Update()
		{
			if (what.velocity.y < 0) {
				who.sprite = cosos [0];
			}
			if (what.velocity.y > 0.1) {
				who.sprite = cosos [2];
			}
			if (what.velocity.y == 0 && what.velocity.x == 0) {
				who.sprite = cosos [1];
			}
			if (what.velocity.x < 0) {
				Watch.transform.localPosition = -PosWacher;
				shootDirection = Vector3.left;
				who.flipX = true;
			}
			if (what.velocity.x > 0) {
				Watch.transform.localPosition = PosWacher;
				shootDirection = Vector3.right;
				who.flipX = false;
			}
			for (int i = 0; i < offsetMusic.Length; i++) {
				Debug.DrawRay (transform.position, offsetMusic [i], Color.red);
			}
		}

		public void Atack()
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

		public void Move(float _x)
		{
			gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (_x * Speed, gameObject.GetComponent<Rigidbody2D> ().velocity.y);
		}

		public void Jump()
		{
			originJump = transform.position - new Vector3 (0.1f, 1.15f, 0.0f);
			if (Physics2D.Raycast (originJump, Vector2.right, 0.3f, layerJump)) {
				Debug.DrawRay (originJump, Vector2.right * 0.5f, Color.black, 2);
				Debug.Log("Toque");
				gameObject.GetComponent <Rigidbody2D> ().velocity = Vector2.up * PotenciaSalto;
			}
		}

		public void ResetPosition()
		{
			if (Physics2D.OverlapPoint (GOSalto.position,layerJump)) {
				gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			}
		}

		public void Smash()
		{
			gameObject.GetComponent <Rigidbody2D> ().velocity = Vector2.down * PotenciaCaida;
		}

		public void TakeDamage()
		{
			Health -= 1;
			if (Health <= 0) {
				Dead ();
			}
		}

		private void Dead()
		{
			Manager_Static.appManager.currentState = AppState.end_game;
			Health = 0;
			Manager_Static.sceneManager.LoadSceneName ("Creditos");
		}


		IEnumerator Trow(float _duration)
		{
			Debug.Log ("Entre a la corrutina");
			canAtack = false;
			float currentTime = Time.time;
			Vector3 positionShoot = transform.position;
			Vector3 currentShootDirection = shootDirection;

			while (Time.time < (currentTime + _duration)) {
				valueOfTime = Mathf.InverseLerp (currentTime, currentTime + _duration, Time.time);
				distanceShoot = curvaDeLanzar.Evaluate (valueOfTime);
				distanceShoot *= LenghtShoot;
				hitInfo = Physics2D.Raycast (positionShoot, currentShootDirection, distanceShoot, layerCanAtack);
				if (hitInfo) {
					Debug.DrawLine (positionShoot, hitInfo.point, Color.green);
				} else {
					Debug.DrawRay (positionShoot, currentShootDirection, Color.blue);
				}
				Weapon.position = positionShoot + currentShootDirection * distanceShoot;
				Weapon.localPosition = Vector3.zero;
				yield return null;
			}
			canAtack = true;
			Debug.Log ("Sale de la corrutina");
		}
	}
}
