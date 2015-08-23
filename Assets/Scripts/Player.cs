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
	public bool groundedFrame = true;

	private IMovement movement;

	private Dragon dragon;

	private TextManager textManager;

		
	// Use this for initialization
	void Start ()
	{
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		animator = gameObject.GetComponent<Animator> ();
		movement = getDefaultMovement ();
		dragon = GameObject.FindGameObjectWithTag ("Dragon").GetComponent<Dragon> ();
		textManager = GameObject.FindGameObjectWithTag ("TextManager").GetComponent<TextManager> ();
		textManager.enqueue ("ok...", gameObject);
		textManager.enqueue ("how do I work this thing?", gameObject);
	}

	public IMovement getDefaultMovement()
	{
		return new DefaultMovement (maxSpeed, acceleration, jumpPower, rb2d);
	}
	
	// Update is called once per frame
	void Update () 
	{
		float horizontalInput = Input.GetAxis ("Horizontal");
		
		if (horizontalInput < -0.1) 
		{
			transform.localScale = new Vector3(-1, 1, 1);
			facingRight = false;
		}
		if (horizontalInput > 0.1) 
		{
			transform.localScale = new Vector3(1, 1, 1);
			facingRight = true;
		}

		movement.onHorizontalInput (horizontalInput);

		if (Input.GetButtonDown ("Jump") && grounded) {
			movement.onJump();
			onJump ();
		}

		animator.SetFloat ("Speed", Mathf.Abs (movement.velocity.x));



	}

	public void onJump()
	{
		grounded = false;
	}

	void FixedUpdate()
	{
		transform.rotation = dragon.gameObject.transform.rotation;

		movement.FixedUpdate ();
	}

	public void captureMovement(IMovement newMovement)
	{
		movement = newMovement;
	}

	public void stopMoving() 
	{
		rb2d.velocity = new Vector3 (0, 0, 0);
	}

	public Rigidbody2D rigidbody
	{
		get
		{
			return rb2d;
		}
	}
}
