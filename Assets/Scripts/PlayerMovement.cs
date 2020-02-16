﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float moveSpeed = 7f;
	public float jumpForce = 5f;
	public float wallJumpForce = 7f;

	WallTest wallTest;

    // Start is called before the first frame update
    void Start()
    {
		wallTest = GetComponent<WallTest>();
    }

    // Update is called once per frame
    void Update()
    {
		moveHorizontal();
		Jump();
    }
		
	// Move horizontal
	void moveHorizontal() 
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		Vector3 movement = new Vector3(horizontalInput, 0f, 0f);

		if (wallTest.Test(WallType.Floor) && wallTest.Test(WallType.LeftWall) && horizontalInput > 0) {
			transform.position += movement * Time.deltaTime * moveSpeed;
		}
		else if (wallTest.Test(WallType.Floor) && wallTest.Test(WallType.RightWall) && horizontalInput < 0) {
			transform.position += movement * Time.deltaTime * moveSpeed;
		}
		else if (!wallTest.Test(WallType.Horizontal)) {
			transform.position += movement * Time.deltaTime * moveSpeed;
		}
	}

	// Jump
	void Jump()
	{
		if (Input.GetButtonDown("Jump")) {
			if (wallTest.Test(WallType.Floor)) {
				gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
			} else if (wallTest.Test(WallType.LeftWall)) {
				gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(wallJumpForce, jumpForce), ForceMode2D.Impulse);
			} else if (wallTest.Test(WallType.RightWall)) {
				gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-wallJumpForce, jumpForce), ForceMode2D.Impulse);
			} 
		}
	}
}
