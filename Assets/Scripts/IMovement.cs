using System;
using UnityEngine;
public interface IMovement
{
	void onHorizontalInput(float value);
	void onJump();
	void FixedUpdate();
	Vector3 velocity{ get;}
}

