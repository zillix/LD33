using UnityEngine;
using System.Collections;

public class Dragon : MonoBehaviour {

	private Rigidbody2D rb2d;

	private Quaternion startAngle;
	private float bobRotationAngle = 0f;
	private static float bobSpeed = 2f;
	private static float bobMagnitude = 5f;

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
		rb2d.transform.rotation = Quaternion.Euler(0, 0, bobMagnitude * Mathf.Sin (bobRotationAngle));
	}
}
