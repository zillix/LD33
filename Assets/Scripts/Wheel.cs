using UnityEngine;
using System.Collections;

public class Wheel : MonoBehaviour, IMovement {

	public GameObject triggerObject;
	public GameObject wheelSprite;

	BoxCollider2D collider;
	
	ITriggerable triggerable;

	private Player player;
	private GameObject playerFeet;

	private float horizontalInput = 0f;

	private static float rotateSpeed = 40f;

	private bool hasCapturedMovement = false;
	private bool canCapture = false;

	private Dragon dragon;

	private Vector3 captureOffset;

	private GameController game;

	private SoundBank sounds;
	private AudioSource wheelSounds;

	void Awake()
	{
		wheelSounds = gameObject.AddComponent<AudioSource> ();
		wheelSounds.loop = true;
		sounds = GameObject.FindGameObjectWithTag ("SoundBank").GetComponent<SoundBank> ();
		wheelSounds.clip = sounds.wheel;
	}
	
	// Use this for initialization
	void Start () {
		triggerable = (ITriggerable)triggerObject.GetComponent (typeof(ITriggerable));
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		dragon = GameObject.FindGameObjectWithTag ("Dragon").GetComponent<Dragon> ();
		playerFeet = GameObject.FindGameObjectWithTag("PlayerFeet");
		collider = gameObject.GetComponent<BoxCollider2D> ();
		game = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetAxis ("Vertical") < 0 || !player.groundedFrame) {
			canCapture = false;
			collider.enabled = false;
			if (hasCapturedMovement) {
				// Release the player, give him a new movement and give it the jump
				IMovement movement = player.getDefaultMovement ();
				releasePlayer (movement);
	
			}
		} else {
			collider.enabled = true;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("PlayerFeet")) {
			canCapture = true;
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag ("PlayerFeet")) {
			if (player.grounded && 
			   player.groundedFrame && !hasCapturedMovement && canCapture && player.transform.position.y > transform.position.y)
			{
				player.stopMoving();
				player.captureMovement(this);
				hasCapturedMovement = true;
				canCapture = false;
				captureOffset = transform.position - player.transform.position;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag ("PlayerFeet")) {
			canCapture = false;
		}
	}

	public void onHorizontalInput(float value)
	{
		if (value != horizontalInput) {
			if (value == 0)
			{
				triggerable.stopTrigger(0);
				wheelSounds.Stop();
			}
			else
			{
				triggerable.startTrigger(value);
				if (!wheelSounds.isPlaying)
				{
					wheelSounds.Play ();
				}
			}
		}
		horizontalInput = value;

		game.onWheelUsed ();


	}

	public void onJump()
	{
		player.groundedFrame = false;

		// Release the player, give him a new movement and give it the jump
		IMovement movement = player.getDefaultMovement ();
		movement.onJump ();
		player.onJump ();

		releasePlayer (movement);
	}

	void releasePlayer(IMovement movement)
	{
		
		
		triggerable.stopTrigger (0);
		
		player.captureMovement (movement);
		horizontalInput = 0;
		hasCapturedMovement = false;
		dragon.stopMoving ();
		
		wheelSounds.Stop ();
	}

	public void FixedUpdate()
	{
		wheelSprite.transform.Rotate(new Vector3(0, 0, -horizontalInput * rotateSpeed * Time.fixedDeltaTime));

		if (hasCapturedMovement) {
			if (!horizontalInput.Equals (0)) {
				dragon.startMoving (horizontalInput, 0);
			} else {
				dragon.stopMoving ();
			}
		}

		if (hasCapturedMovement) {
			player.transform.position = transform.position - captureOffset;
			player.rigidbody.velocity = dragon.rigidbody.velocity;
		}
	
	}

	public Vector3 velocity
	{
		// Echo out the horizontal input
		get { return new Vector3(horizontalInput * dragon.xSpeed, 0, 0); }
	}
}
