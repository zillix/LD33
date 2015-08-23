using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

	private Vector3 startPosition;
	public float driftAngle = 0f;
	public float driftSpeed = 2f;
	public float driftMagnitude = 5f;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
	}
	
	void FixedUpdate () {
		driftAngle += driftSpeed * Time.fixedDeltaTime;

		transform.position = startPosition + Quaternion.Euler(0, 0, driftAngle) * new Vector3(driftMagnitude, 0, 0);
	}
}
