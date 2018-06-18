using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAAI
{
	public class Decalsystem : MonoBehaviour {

		public GameObject[] sprites;
		public Camera cam;
		public LayerMask layerMask;
		private int selected_spr;

		void OnEnable()
		{
			Manager_Static.inputManager.DecalChangeHandler += SetSprite;
			Manager_Static.inputManager.DecalHandler += PlaceDecal;
		}

		void OnDisable()
		{
			Manager_Static.inputManager.DecalChangeHandler -= SetSprite;
			Manager_Static.inputManager.DecalHandler -= PlaceDecal;
		}

		void OnDestroy()
		{
			Manager_Static.inputManager.DecalChangeHandler -= SetSprite;
			Manager_Static.inputManager.DecalHandler -= PlaceDecal;
		}

		public void SetSprite(int _i)
		{
			selected_spr += _i;
			if (selected_spr >= sprites.Length)
				selected_spr = 0;
			if (selected_spr < 0)
				selected_spr = sprites.Length - 1;
		}

		public void PlaceDecal(int _i)
		{
			RaycastHit hit;
			if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, 5.0f, layerMask))
			{
				GameObject Graffitti = Instantiate (sprites [selected_spr], (hit.point) + (hit.normal * 0.01f), Quaternion.LookRotation(hit.normal));
				StartCoroutine (FadeAway (Graffitti, 1.0f));
				Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.forward) * hit.distance, Color.red);
				Debug.Log ("Hit " + hit.collider.gameObject.name);
			}
			else
			{
				Debug.Log ("Didn't Hit Shit Boi");
			}
		}

		IEnumerator FadeAway(GameObject graf, float _alpha)
		{
			graf.GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255, _alpha);
			if (_alpha > 0)
			{
				yield return new WaitForSeconds (0.1f);
				_alpha -= 0.01f;
				StartCoroutine (FadeAway (graf, _alpha));
			}
			else
			{
				Destroy (graf);
			}
		}
	}
}
