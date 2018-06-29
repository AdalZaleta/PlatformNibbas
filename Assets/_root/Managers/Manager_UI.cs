using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TAAI
{
	public class Manager_UI : MonoBehaviour {

		public GameObject Pausa;
		public Image[] Hearts;

		public Sprite[] heart_spr;

		void Awake()
		{
			Manager_Static.uiManager = this;
		}

		public void isInPause(bool _isIt)
		{
			Pausa.gameObject.SetActive (_isIt);
		}

		public void UpdateHPGraphic(int newVal)
		{
			for (int i = 0; i < Hearts.Length; i++)
			{
				if (i < newVal)
				{
					Hearts [i].sprite = heart_spr [1];
				}
				else
				{
					Hearts [i].sprite = heart_spr [0];
				}
			}
		}
	}
}