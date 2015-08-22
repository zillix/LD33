using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float maxSpeed;
	public float acceleration;
	public float jumpPower;
	private Rigidbody2D rb2d;
	private Animator animator;

	private bool facingRight = true;

	public bool grounded = true;

		
	// Use this for initialization
	void Start ()
	{
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		animator = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (Input.GetAxis ("Horizontal") < -0.1) 
		{
			transform.localScale = new Vector3(-1, 1, 1);
			facingRight = false;
		}
		if (Input.GetAxis ("Horizontal") > 0.1) 
		{
			transform.localScale = new Vector3(1, 1, 1);
			facingRight = true;
		}

		if (Input.GetButtonDown ("Jump") && grounded) {
			grounded = false;
			rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
			rb2d.AddForce(Vector2.up * jumpPower);
		}

		animator.SetFloat ("Speed", Mathf.Abs (rb2d.velocity.x));

	}

	void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");

		rb2d.velocity = new Vector2 (h * maxSpeed, rb2d.velocity.y);

		if (rb2d.velocity.x > maxSpeed) {
			rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
		}
		
		if (rb2d.velocity.x < -maxSpeed) {
			rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
		}
	}
}
