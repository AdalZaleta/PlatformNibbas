using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAAI
{
	public class Combos : MonoBehaviour {

		string DoorName;

		private KeyCombo falconPunch = new KeyCombo(new string[] {"down", "right","right"});
		private KeyCombo falconKick = new KeyCombo(new string[] {"down", "right","Fire1"});
		private KeyCombo easteregg = new KeyCombo(new string[] {"left", "right", "left", "right", "Right_Trigger", "Right_Trigger", "Control_Y"});
		private KeyCombo OGkonami = new KeyCombo(new string[] {"up", "up", "down", "down", "left", "right", "left", "right", "Control_B", "Control_A", "Control_Start"});
		private KeyCombo key_0 = new KeyCombo(new string[] {"up", "down", "up"});
		private KeyCombo key_1 = new KeyCombo(new string[] {"right", "left", "right"});
		private KeyCombo key_2 = new KeyCombo(new string[] {"up", "down", "right", "left"});

		public void SetDoorID(string _name)
		{
			DoorName = _name;
			Debug.Log ("New doorname: " + DoorName);
		}

		void Update () {
			if (falconPunch.Check())
			{
				// do the falcon punch
				Debug.Log("PUNCH"); 
			}		
			if (falconKick.Check())
			{
				// do the falcon punch
				Debug.Log("KICK"); 
			}
			if (easteregg.Check())
			{
				Debug.Log ("EASTER EGG BOI");
			}
			if (OGkonami.Check())
			{
				Debug.Log ("KONAMI MODE ENGAGED !");
			}
			if (key_0.Check())
			{
				Manager_Static.locksManager.UseKey(DoorName, "red_note");
				//Manager_Static.controllerManager
			}
			if (key_1.Check())
			{
				Manager_Static.locksManager.UseKey (DoorName, "green_note");
			}
			if (key_2.Check())
			{
				Manager_Static.locksManager.UseKey (DoorName, "blue_note");
			}
		}
	}
}