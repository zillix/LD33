using UnityEngine;
using System.Collections;

public class DragonWings : MonoBehaviour, ITriggerable {

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startTrigger(float value)
	{
	}

	public void stopTrigger(float value)
	{
	}
}
