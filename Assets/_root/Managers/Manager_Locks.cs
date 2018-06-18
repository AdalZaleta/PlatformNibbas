using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAAI
{
	public class Manager_Locks : MonoBehaviour {

		public GameObject[] doors;

		void Awake()
		{
			Manager_Static.locksManager = this;
		}

		public void UseKey(string door_name, string key)
		{
			for (int i = 0; i < doors.Length; i++)
			{
				if (doors[i].name == door_name)
				{
					doors [i].GetComponent<LockSystem> ().unLock (key);
				}
			}
		}
	}
}