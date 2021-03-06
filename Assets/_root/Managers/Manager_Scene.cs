﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TAAI
{
	public class Manager_Scene : MonoBehaviour {

		public int currentScene;

		void Awake()
		{
			Manager_Static.sceneManager = this;
		}

		public void LoadScene(int _id, bool _isAditive)
		{
			if (_isAditive)
				SceneManager.LoadScene (_id, LoadSceneMode.Additive);
			if (!_isAditive)
				SceneManager.LoadScene (_id);
		}
	}
}
