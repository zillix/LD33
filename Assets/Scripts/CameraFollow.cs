using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	private Vector2 velocity;
	
	public float smoothTimeY;
	public float smoothTimeX;
	
	public GameObject target;
	
	public bool usesBounds;
	
	public GameObject minPoint;
	public GameObject maxPoint;
	public GameObject cameraOffset;

	private Vector2 offset;

	void Start () {
		target = GameObject.FindGameObjectWithTag ("Dragon");
		offset = new Vector2 (transform.position.x - cameraOffset.transform.position.x, transform.position.y - cameraOffset.transform.position.y);
		//offset = new Vector2 ();            
	}
	
	void FixedUpdate()
	{
		float posX = Mathf.SmoothDamp (transform.position.x, target.transform.position.x, ref velocity.x, smoothTimeX);
		float posY = Mathf.SmoothDamp (transform.position.y, target.transform.position.y, ref velocity.y, smoothTimeY);
		
		transform.position = new Vector3 (posX + offset.x, posY + offset.y, transform.position.z);
		
		if (usesBounds) {
			transform.position = new Vector3 (
				Mathf.Clamp (transform.position.x, minPoint.transform.position.x, maxPoint.transform.position.x),
				Mathf.Clamp(transform.position.y, minPoint.transform.position.y, maxPoint.transform.position.y),
				transform.position.z);
		}
		
	}
}
