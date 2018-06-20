using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAAI
{
	public class TeasHolder : MonoBehaviour {

		public Sprite[] cosos;
		public SpriteRenderer who;
		public Rigidbody2D what;
		public Transform Watch;

		public Vector3[] offsetMusic;
		public Vector3 PosWacher = new Vector3(2.5f, 0.0f, 0.0f);
		public AnimationCurve curvaDeLanzar;


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
				who.flipX = true;
			}
			if (what.velocity.x > 0) {
				Watch.transform.localPosition = PosWacher;
				who.flipX = false;
			}
			for (int i = 0; i < offsetMusic.Length; i++) {
				Debug.DrawRay (transform.position, offsetMusic [i], Color.red);
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
	}
}