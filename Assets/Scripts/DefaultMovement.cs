using System;
using UnityEngine;
public class DefaultMovement : IMovement
{
	private float maxSpeed;
	private float acceleration;
	private float jumpPower;

	private Rigidbody2D rb2d;

	private float horizontalInput;

	public DefaultMovement (float MaxSpeed, float Acceleration, float JumpPower, Rigidbody2D RB2D)
	{
		maxSpeed = MaxSpeed;
		acceleration = Acceleration;
		jumpPower = JumpPower;
		rb2d = RB2D;
	}

	public void onHorizontalInput(float value)
	{
		horizontalInput = value;
	}

	public void onJump()
	{
		rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
		rb2d.AddForce(Vector2.up * jumpPower);
	}

	public void FixedUpdate()
	{
		rb2d.velocity = new Vector2 (horizontalInput * maxSpeed, rb2d.velocity.y);
		
		if (rb2d.velocity.x > maxSpeed) {
			rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
		}
		
		if (rb2d.velocity.x < -maxSpeed) {
			rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
		}
	}

	public Vector3 velocity
	{
		get { return rb2d.velocity; }
	}
}

