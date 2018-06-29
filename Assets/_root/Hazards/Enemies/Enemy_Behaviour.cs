using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyType
{
	patrol_horizontal = 0,
	patrol_vertical = 1,
	patrol_static = 2
}

public class Enemy_Behaviour : MonoBehaviour {

	public enemyType type;
	public int Health;
	public float distance;
	public float speed;
	public GameObject sprite;
	public LayerMask layerMask;
	Rigidbody2D rig;
	Vector3 startingPosition;

	float spr_w;

	bool coolDown;

	public void TakeDamage()
	{
		gameObject.GetComponent<Animator> ().SetTrigger ("Hit");
		Health--;
		if(Health <= 0)
		{
			Dead ();
		}
	}

	public void Dead()
	{
		gameObject.GetComponent<Animator> ().SetBool ("Muerto", true);
		StartCoroutine (Desaparecer ());
	}

	IEnumerator Desaparecer()
	{
		yield return new WaitForSeconds (1.0f);
		gameObject.SetActive (false);
	}

	void Start()
	{
		rig = GetComponent<Rigidbody2D> ();
		startingPosition = transform.position - new Vector3((distance / 2), (distance / 2), transform.position.z);

		spr_w = sprite.gameObject.GetComponent<SpriteRenderer> ().sprite.bounds.size.x * sprite.transform.localScale.x;
	}

	void OnCollisionEnter2D(Collision2D _col)
	{
		if (_col.gameObject.CompareTag("Player"))
		{
			gameObject.GetComponent<Animator> ().SetTrigger ("Atack");
			_col.gameObject.SendMessage ("TakeDamage", SendMessageOptions.DontRequireReceiver);
		}
	}

	void Update()
	{
		switch((int)type)
		{
		case 0:
			if (rig.velocity.y > 0)
				rig.velocity = new Vector2 (speed, 0);
			rig.velocity = new Vector2 (speed, rig.velocity.y);
			break;
		case 1:
			if (rig.velocity.x > 0 || rig.velocity.x < 0)
				rig.velocity = new Vector2 (0, speed);
			rig.velocity = new Vector2 (rig.velocity.x, speed);
			break;
		case 2:
			break;
		}
	}

	void FixedUpdate()
	{
		RaycastHit2D hitA;
		RaycastHit2D hitB;
		RaycastHit2D floorCheckL;
		RaycastHit2D floorCheckR;
		switch ((int)type)
		{
		case 0:
			rig.gravityScale = 1;
			if (!coolDown)
			{
				if ((transform.position.x >= startingPosition.x + distance) || (transform.position.x <= startingPosition.x)) 
				{
					StartCoroutine (flip ());
				}
				hitA = Physics2D.Raycast (transform.position, Vector2.right, 1.0f, layerMask);
				if (hitA.collider != null) {
					Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.right) * hitA.distance, Color.red);
					StartCoroutine (flip ());
				}
				hitB = Physics2D.Raycast (transform.position, Vector2.left, 1.0f, layerMask);
				if (hitB.collider != null){
					Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.left) * hitB.distance, Color.red);
					StartCoroutine (flip ());
				}
				floorCheckL = Physics2D.Raycast (transform.position + new Vector3 (-(spr_w/2), 0, 0), Vector2.down, 1.0f, layerMask);
				if (floorCheckL.collider == null)
				{
					Debug.DrawRay (transform.position + new Vector3 (-(spr_w / 2), 0, 0), transform.TransformDirection (Vector3.down) * 1.0f, Color.green);
					StartCoroutine (flip ());
				}
				floorCheckR = Physics2D.Raycast (transform.position + new Vector3 ((spr_w/2), 0, 0), Vector2.down, 1.0f, layerMask);
				if (floorCheckR.collider == null)
				{
					Debug.Log ("A la madre una orilla");
					Debug.DrawRay (transform.position + new Vector3 ((spr_w / 2), 0, 0), transform.TransformDirection (Vector3.down) * 1.0f, Color.green);
					StartCoroutine (flip ());
				}
				Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.right) * 1.0f, Color.green);
				Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.left) * 1.0f, Color.green);
			}
			break;
		case 1:
			rig.gravityScale = 0;
			if (!coolDown)
			{
				if ((transform.position.y >= startingPosition.y + distance) || (transform.position.y <= startingPosition.y))
				{
					StartCoroutine (flip ());
				}
				hitA = Physics2D.Raycast (transform.position, Vector2.up, 1.0f, layerMask);
				if (hitA.collider != null) {
					Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.up) * hitA.distance, Color.red);
					StartCoroutine (flip ());
				}
				hitB = Physics2D.Raycast (transform.position, Vector2.down, 1.0f, layerMask);
				if (hitB.collider != null){
					Debug.Log ("HIT");
					Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.down) * hitB.distance, Color.red);
					StartCoroutine (flip ());
				}
				Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.up) * 1.0f, Color.green);
				Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.down) * 1.0f, Color.green);
			}
			break;
		case 2:
			rig.gravityScale = 1;
			GameObject target = GameObject.FindGameObjectWithTag ("Player");
			if (target)
			{
				if (transform.position.x - target.transform.position.x <= 0)
				{
					sprite.GetComponent<SpriteRenderer> ().flipX = true;
				}
				else
				{
					sprite.GetComponent<SpriteRenderer> ().flipX = false;
				}
			}

			break;
		}
	}

	IEnumerator flip(){
		speed = -speed;
		sprite.GetComponent<SpriteRenderer> ().flipX = !sprite.GetComponent<SpriteRenderer> ().flipX;
		coolDown = true;
		yield return new WaitForSeconds (0.1f);
		coolDown = false;
	}
}