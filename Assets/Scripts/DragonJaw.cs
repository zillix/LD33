using UnityEngine;
using System.Collections;

public class DragonJaw : MonoBehaviour, ITriggerable  {

	private static float rotateSpeed = 10f;
	private static Quaternion maxRotation = Quaternion.Euler(0, 0, -45);
	private Quaternion startRotation;

	private Quaternion targetRotation;

	private Rigidbody2D rb2d;

	private Dragon dragon;


	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		startRotation = rb2d.transform.rotation;
		targetRotation = startRotation;

		dragon = GameObject.FindGameObjectWithTag ("Dragon").GetComponent<Dragon> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		rb2d.transform.rotation = Quaternion.Lerp (rb2d.transform.rotation, dragon.gameObject.transform.rotation * targetRotation, 
		                                          rotateSpeed * Time.deltaTime);
		                                       
	}

	public void startTrigger(float value)
	{
			targetRotation = maxRotation;
	}

	public void stopTrigger(float value)
	{
			targetRotation = startRotation;
	}
}
