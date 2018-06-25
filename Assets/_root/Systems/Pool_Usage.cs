using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAAI
{
	public class Pool_Usage : MonoBehaviour {

		public GameObject prefab;
		public GameObject aim;
		bool canshoot = true;
		public int weapon = 1;
		float downTime;
		public int dmg;
		public Light lit;

		// Use this for initialization
		void Start () {
			for (int i = 0; i < 3; i++)
			{
				PoolManager.PreSpawn (prefab, 8, false);
				PoolManager.SetPoolLimit (prefab, 10);
			}
		}

		void OnDestroy()
		{
			PoolManager.ClearPools ();
		}

		// Methods to spawn notes
		public void SpawnNote(string note)
		{
			Debug.Log ("Spawned note " + note);
			// USE PoolManager.Spawn(); to spawn notes in pool
		}
	}
}