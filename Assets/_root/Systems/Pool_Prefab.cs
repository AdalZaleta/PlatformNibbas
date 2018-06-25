using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAAI
{
	[RequireComponent(typeof(Rigidbody))]
	public class Pool_Prefab : MonoBehaviour {

		public float speed;
		Rigidbody rig;
		public GameObject spr;
		public ParticleSystem hit;

		void OnTriggerEnter (Collider _col)
		{
			if ((_col.gameObject.CompareTag ("Wall")) || (_col.gameObject.CompareTag("Enemy")))
			{
				StartCoroutine (Suicide ());
			}
		}

		void OnSpawn()
		{
			rig = GetComponent<Rigidbody> ();
			rig.AddRelativeForce (Vector3.up * speed);
			Invoke ("Suicide", 2);
		}

		IEnumerator Suicide()
		{
			hit.Play ();
			spr.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			rig.velocity = Vector3.zero;
			rig.rotation = Quaternion.identity;
			yield return new WaitForSeconds (0.2f);
			spr.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			PoolManager.Despawn (gameObject);
		}
	}
}