using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_Movement : MonoBehaviour {

	public float Xvel;
	public float Yvel;
	public bool canJump = true;
	public Rigidbody2D rig;

	// Use this for initialization
	void Start () {
		
	}

	void OnCollisionEnter2D(Collision2D _col)
	{
		Debug.Log ("Collision");
		if (_col.gameObject.CompareTag("floor"))
		{
			Debug.Log ("floor");
			canJump = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.D))
		{
			transform.Translate (Vector2.right * Xvel);
		}	
		if (Input.GetKey(KeyCode.A))
		{
			transform.Translate (Vector2.left * Xvel);
		}
		if (canJump)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				rig.AddForce (Vector2.up * Yvel);
				canJump = false;
			}
		}
	}
}
