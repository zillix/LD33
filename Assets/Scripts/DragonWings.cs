using UnityEngine;
using System.Collections;

public class DragonWings : MonoBehaviour, ITriggerable {

	private Animator anim;

	private float startSpeed;
	public float fastSpeed = 2f;
	private float currentSpeed = 0;

	private Dragon dragon;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
		startSpeed = anim.speed;
		dragon = GameObject.FindGameObjectWithTag ("Dragon").GetComponent<Dragon> ();
		currentSpeed = startSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (!dragon.activated) {
			anim.speed = 0;
		} else {
			anim.speed = currentSpeed;
		}
	}

	public void startTrigger(float value)
	{
		if (!value.Equals (0)) {
			currentSpeed = fastSpeed;
		} else {
			currentSpeed = startSpeed;
		}

		anim.speed = currentSpeed;
	}

	public void stopTrigger(float value)
	{
		currentSpeed = startSpeed;
	}
}
