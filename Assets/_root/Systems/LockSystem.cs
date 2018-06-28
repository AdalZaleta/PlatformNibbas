using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAAI
{
	public class LockSystem : MonoBehaviour {

		public string[] keys;
		public bool[] locks;
		public GameObject[] notes;
		public GameObject door;
		bool doorState = false;
		bool canTry = false;

		void OnTriggerEnter2D(Collider2D _col)
		{
			if (_col.gameObject.CompareTag("Player"))
			{
				_col.gameObject.SendMessage ("SetDoorID", name, SendMessageOptions.DontRequireReceiver);
			}
		}

		void OnTriggerStay2D(Collider2D _col)
		{
			canTry = true;
		}

		void OnTriggerExit2D(Collider2D _col)
		{
			Debug.Log ("Exited opening range");
			canTry = false;
		}

		public void unLock(string lockname)
		{
			Debug.Log ("Entered unLock");
			if (canTry) 
			{
				Debug.Log ("Can Try = true");
				for (int i = 0; i < locks.Length; i++) 
				{
					Debug.Log (keys [i] + " = " + lockname);
					if (keys[i] == lockname) {
						Debug.Log ("UNLOCKED : " + keys [i] + " | " + i);
						locks [i] = true;
						notes [i].GetComponent<SpriteRenderer> ().color = Color.black;
					}
				}
				checkLocks ();
			}
		}

		public void checkLocks()
		{
			doorState = true;
			for (int i = 0; i < locks.Length; i++)
			{
				if (!locks[i])
				{
					doorState = false;
				}
			}
			openDoor ();
		}

		public void openDoor()
		{
			if (doorState)
			{
				Debug.Log ("Door is now Opened");
				door.GetComponent<BoxCollider2D> ().enabled = false;
				door.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
				for (int i = 0; i < notes.Length; i++)
				{
					notes [i].SetActive (false);
				}
			}
		}
	}
}