using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAAI
{
	public class LockSystem : MonoBehaviour {

		public string[] keys;
		public bool[] locks;
		public GameObject door;
		bool doorState = false;
		bool canTry = false;

		void OnTriggerEnter2D(Collider2D _col)
		{
			Debug.Log ("In range to open");
			canTry = true;
			if (_col.gameObject.CompareTag("Player"))
			{
				_col.gameObject.SendMessage ("SetDoorID", name, SendMessageOptions.DontRequireReceiver);
			}
		}

		void OnTriggerExit2D(Collider2D _col)
		{
			Debug.Log ("Exited opening range");
			canTry = false;
		}

		public void unLock(string lockname)
		{
			if (canTry) 
			{
				Debug.Log ("Unlocking " + lockname);
				for (int i = 0; i < locks.Length; i++) 
				{
					Debug.Log ("Obj: " + lockname + " | key[" + i + "]: " + keys [i]);
					if (keys [i] == lockname) {
						locks [i] = true;
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
				door.GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255, 0.5f);
			}
		}
	}
}