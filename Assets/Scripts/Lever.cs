﻿using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {

	private int state;
	private Animator anim;
	public GameObject tailTriggerObject;
	private ITriggerable tailTriggerable;
	public GameObject wingsTriggerObject;
	private ITriggerable wingsTriggerable;
	private Dragon dragon;
	private GameController game;

	// Use this for initialization
	void Start () {

		tailTriggerable = (ITriggerable)tailTriggerObject.GetComponent (typeof(ITriggerable));
		wingsTriggerable = (ITriggerable)wingsTriggerObject.GetComponent (typeof(ITriggerable));
		anim = gameObject.GetComponent<Animator> ();
		dragon = GameObject.FindGameObjectWithTag ("Dragon").GetComponent<Dragon> ();
		game = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetInteger ("state", state);
	}




	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			state = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));

			tailTriggerable.startTrigger(state);
				wingsTriggerable.startTrigger(state);
				if (state == 0)
				{
					dragon.stopMoving();
				}
				else
				{
					dragon.startMoving(0, state);
				}

			if (state != 0)
			{
				game.onLeverUsed();
			}
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			state = 0;
			tailTriggerable.stopTrigger(0);
			wingsTriggerable.stopTrigger(0);
			dragon.stopMoving();
		}
	}
}
