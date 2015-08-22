using UnityEngine;
using System.Collections;

public class DragonWings : MonoBehaviour, ITriggerable {

	private Animator anim;

	private float startSpeed;
	public float fastSpeed = 2f;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
		startSpeed = anim.speed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startTrigger(float value)
	{
		if (!value.Equals (0)) {
			anim.speed = fastSpeed;
		} else {
			anim.speed = startSpeed;
		}
	}

	public void stopTrigger(float value)
	{
		anim.speed = startSpeed;
	}
}
