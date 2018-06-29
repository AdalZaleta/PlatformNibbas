using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAAI
{
	public class TeasHolder : MonoBehaviour {

		public AudioClip[] SonidosPj;
		public GameObject[] Notas;

		public bool canAtack = true;
		public bool canplay = true;

		public Sprite[] cosos;
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

		void Start()
		{
			PoolManager.MakePool (Notas [0], 3, 3, true);
			PoolManager.MakePool (Notas [1], 3, 3, true);
			PoolManager.MakePool (Notas [2], 3, 3, true);
		}

		void OnDestroy()
		{
			PoolManager.ClearPools ();
		}

		void Update()
		{
			if (what.velocity.x < 0) {
				Watch.transform.localPosition = -PosWacher;
				shootDirection = Vector3.left;
				transform.GetComponent<SpriteRenderer> ().flipX = true;
			}
			if (what.velocity.x > 0) {
				Watch.transform.localPosition = PosWacher;
				shootDirection = Vector3.right;
				transform.GetComponent<SpriteRenderer> ().flipX = false;
			}
		
			Manager_Static.animatorManager.setJump (gameObject.GetComponent<Rigidbody2D> ().velocity);

			if (Physics2D.Raycast (transform.position + new Vector3 (-0.5f, 0.6f, 0), Vector2.down, 1.4f, layerJump)) {
				Manager_Static.animatorManager.setGrab (true);
			}
			else if (Physics2D.Raycast (transform.position + new Vector3 (0.5f, 0.6f, 0), Vector2.down, 1.4f, layerJump)) {
				Manager_Static.animatorManager.setGrab (true);
			} else {
				Debug.Log ("No estoy tocando");
				Manager_Static.animatorManager.setGrab (false);
			}
		}

		public void Atack()
		{
			if (canAtack) 
			{
				Manager_Static.audioManager.playSoundGlobal (SonidosPj [2]);
				Manager_Static.animatorManager.setThrow ();
				Debug.Log ("Input Ataque");
				StartCoroutine (Trow (DurationShoot));
			}
			else
			{
				Debug.Log ("Ya esta atacando, Ignora Input");
			}
		}

		public void AtackMele()
		{
			Manager_Static.audioManager.playSoundGlobal (SonidosPj [3]);
			hitInfo = Physics2D.Raycast (transform.position, shootDirection, 1.0f, layerCanAtack);
			if (hitInfo.transform.CompareTag("Enemy")) {
				hitInfo.transform.SendMessage ("TakeDamage", SendMessageOptions.DontRequireReceiver);
			}
		}

		public void PlayNote(int _note)
		{
			if (canplay) {
				StartCoroutine (PlayMusic (_note));
			} else {
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
				Manager_Static.audioManager.playSoundGlobal (SonidosPj [4]);
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
			Manager_Static.audioManager.playSoundGlobal (SonidosPj[0]);
			Manager_Static.animatorManager.TakeDamage ();
			Health -= 1;
			if (Health <= 0) {
				Dead ();
			}
		}

		private void Dead()
		{
			Manager_Static.audioManager.playSoundGlobal (SonidosPj[1]);
			Manager_Static.appManager.currentState = AppState.end_game;
			Health = 0;
			Manager_Static.sceneManager.LoadSceneName ("Creditos");
		}

		IEnumerator PlayMusic(int _note)
		{
			int index = Random.Range (5, SonidosPj.Length);
			Manager_Static.audioManager.playSoundAT (transform.position, SonidosPj[index]);
			canplay = false;
			PoolManager.Spawn (Notas [_note], transform.position + offsetMusic [_note], Quaternion.identity);
			yield return new WaitForSeconds (0.3f);
			canplay = true;
		}


		IEnumerator Trow(float _duration)
		{
			Debug.Log ("Entre a la corrutina");
			Manager_Static.animatorManager.setThrow ();
			canAtack = false;
			float currentTime = Time.time;
			Vector3 positionShoot = transform.position;
			Vector3 currentShootDirection = shootDirection;

			while (Time.time < (currentTime + _duration - 0.01f)) {
				valueOfTime = Mathf.InverseLerp (currentTime, currentTime + _duration, Time.time);	
				distanceShoot = curvaDeLanzar.Evaluate (valueOfTime);
				distanceShoot *= LenghtShoot;
				hitInfo = Physics2D.Raycast (positionShoot, currentShootDirection, distanceShoot, layerCanAtack);
				if (hitInfo) {
					hitInfo.transform.SendMessage ("TakeDamage", SendMessageOptions.DontRequireReceiver);
					Debug.DrawLine (positionShoot, hitInfo.point, Color.green);
				}
				Weapon.position = positionShoot + currentShootDirection * distanceShoot;
				yield return null;
			}
			Weapon.localPosition = Vector3.zero;
			canAtack = true;
			Debug.Log ("Sale de la corrutina");
		}
	}
}