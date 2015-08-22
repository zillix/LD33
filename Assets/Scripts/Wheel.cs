using UnityEngine;
using System.Collections;

public class Wheel : MonoBehaviour, IMovement {

	public GameObject triggerObject;
	public GameObject wheelSprite;
	
	ITriggerable triggerable;

	private Player player;
	private GameObject playerFeet;

	private float horizontalInput = 0f;

	private static float rotateSpeed = 30f;

	private bool hasCapturedMovement = false;
	private bool canCapture = false;

	private Dragon dragon;

	private Vector3 captureOffset;


	// Use this for initialization
	void Start () {
		triggerable = (ITriggerable)triggerObject.GetComponent (typeof(ITriggerable));
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		dragon = GameObject.FindGameObjectWithTag ("Dragon").GetComponent<Dragon> ();
		playerFeet = GameObject.FindGameObjectWithTag("PlayerFeet");
	}
	
	// Update is called once per frame
	void Update () {
	
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
			if (player.grounded && !hasCapturedMovement && canCapture && player.transform.position.y > transform.position.y)
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
			}
			else
			{
				triggerable.startTrigger(value);
			}
		}
		horizontalInput = value;
	}

	public void onJump()
	{
		triggerable.stopTrigger (0);

		// Release the player, give him a new movement and give it the jump
		IMovement movement = player.getDefaultMovement ();
		movement.onJump ();
		player.onJump ();
		player.captureMovement (movement);
		horizontalInput = 0;
		hasCapturedMovement = false;
	}

	public void FixedUpdate()
	{
		wheelSprite.transform.Rotate(new Vector3(0, 0, -horizontalInput * rotateSpeed * Time.fixedDeltaTime));

		if (!horizontalInput.Equals (0)) {
			dragon.startMoving (horizontalInput, 0);
		} else {
			dragon.stopMoving();
		}

		if (hasCapturedMovement) {
			player.transform.position = transform.position + captureOffset;
		}
	
	}

	public Vector3 velocity
	{
		// Echo out the horizontal input
		get { return new Vector3(horizontalInput * dragon.xSpeed, 0, 0); }
	}
}
