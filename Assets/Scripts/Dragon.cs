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

	public float xSpeed = 10f;
	public float ySpeed = 10f;

	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		rb2d.gravityScale = 0;

		startAngle = rb2d.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		bobRotationAngle += bobSpeed * Time.deltaTime;

		if (rb2d.velocity.x == 0) {
			rb2d.transform.rotation = Quaternion.Lerp(rb2d.transform.rotation, Quaternion.Euler (0, 0, bobMagnitude * Mathf.Sin (bobRotationAngle)), lerpSpeed);
		} else {
			rb2d.transform.rotation = Quaternion.Lerp(rb2d.transform.rotation, Quaternion.Euler(0, 0, moveRotation * Mathf.Abs(rb2d.velocity.x) / rb2d.velocity.x), lerpSpeed);
		
		}
	}

	public void startMoving(float xDirection, float yDirection)
	{
		rb2d.velocity = new Vector2 (xDirection, yDirection);
	}

	public void stopMoving()
	{
		rb2d.velocity = new Vector2 ();
	}
}
