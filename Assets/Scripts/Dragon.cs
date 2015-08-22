using UnityEngine;
using System.Collections;

public class Dragon : MonoBehaviour {

	private Rigidbody2D rb2d;

	private Quaternion startAngle;
	private float bobRotationAngle = 0f;
	private static float bobSpeed = 2f;
	private static float bobMagnitude = 5f;

	private static float moveRotation = -15f;
	private static float lerpSpeed = .1f;

	private Vector3 targetPoint;
	private Vector3 myVelocity;
	private Quaternion targetRotation;
	private Quaternion myRotation;

	public float xSpeed = 10f;
	public float ySpeed = 10f;

	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		//rb2d.gravityScale = 0;

		startAngle = rb2d.transform.rotation;
		myRotation = rb2d.transform.rotation;
		myVelocity = new Vector3 ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		//targetPoint.x += rb2d.velocity.x * Time.fixedDeltaTime;
		//targetPoint.y += rb2d.velocity.y * Time.fixedDeltaTime;
		//transform.position = targetPoint;

		bobRotationAngle += bobSpeed * Time.deltaTime;

		if (myVelocity.x == 0) {
			//rb2d.transform.rotation = Quaternion.Lerp(rb2d.transform.rotation, Quaternion.Euler (0, 0, bobMagnitude * Mathf.Sin (bobRotationAngle)), lerpSpeed);
			myRotation = Quaternion.Lerp(myRotation, Quaternion.Euler (0, 0, bobMagnitude * Mathf.Sin (bobRotationAngle)), lerpSpeed);

		} else {
			//rb2d.transform.rotation = Quaternion.Lerp(rb2d.transform.rotation, Quaternion.Euler(0, 0, moveRotation * Mathf.Abs(rb2d.velocity.x) / rb2d.velocity.x), lerpSpeed);
			myRotation = Quaternion.Lerp(myRotation, Quaternion.Euler(0, 0, moveRotation * Mathf.Abs(myVelocity.x) / myVelocity.x), lerpSpeed);

		}

		rb2d.transform.rotation = myRotation;
		rb2d.velocity = myVelocity;
	}

	public void startMoving(float xDirection, float yDirection)
	{
		myVelocity = new Vector2 (xDirection * xSpeed, yDirection * ySpeed);
	}

	public void stopMoving()
	{
		myVelocity = new Vector2 ();
	}
}
