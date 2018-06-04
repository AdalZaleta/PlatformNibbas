using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAAI
{
	public class TeasHolder : MonoBehaviour {

		public Sprite[] cosos;
		public SpriteRenderer who;
		public Rigidbody2D what;


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
				who.flipX = true;
			}
			if (what.velocity.x > 0) {
				who.flipX = false;
			}
		}
	}
}