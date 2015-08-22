using UnityEngine;
using System.Collections;

public class DragonTail : MonoBehaviour, ITriggerable {

	private int state;
	private Rigidbody2D rb2d;
	private Dragon dragon;

	private static float tailAngle = 30f;

	private static Quaternion upRotation = Quaternion.Euler(0, 0, -tailAngle);
	private static Quaternion downRotation = Quaternion.Euler(0, 0, tailAngle);

	private Quaternion startRotation;
	private Quaternion targetRotation;

	private static float rotateSpeed = 10f;


	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		dragon = GameObject.FindGameObjectWithTag ("Dragon").GetComponent<Dragon> ();
		startRotation = rb2d.transform.rotation;
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
		setState ((int)value);
	}

	public void stopTrigger(float value)
	{
		setState ((int)value);
	}

	private void setState(int value)
	{
		if (value != state)
		{
			state = value;
			if (state == 0)
			{
				targetRotation = startRotation;
			}
			else if (state < 0)
			{
				targetRotation = downRotation;
			}
			else
			{
				targetRotation = upRotation;
			}


		}

	}
}
