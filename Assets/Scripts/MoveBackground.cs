using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour {
	
	public float speed;
	private float x;
	private float multiplier = 0.1f;
	private float velocity;
	[SerializeField] private GameObject player;
	
	private void Update()
	{
		velocity = player.GetComponent<Rigidbody2D>().velocity.x;
		if (velocity != 0)
		{
			moveBackground(velocity);
		}
	}


	void moveBackground(float velocity)
	{
		x = transform.position.x;
		x -= (velocity * multiplier) * (speed * Time.deltaTime);
		transform.position = new Vector3 (x, transform.position.y, transform.position.z);
	}
}
