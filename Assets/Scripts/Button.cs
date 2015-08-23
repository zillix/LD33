﻿using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public GameObject jawTriggerObject;
	public GameObject emitterTriggerObject;
	public GameObject furnaceObject;
	private Dragon dragon;

	Furnace furnace;

	ITriggerable jawTriggerable;
	ITriggerable emitterTriggerable;

	void Start()
	{
		jawTriggerable = (ITriggerable)jawTriggerObject.GetComponent (typeof(ITriggerable));
		emitterTriggerable = (ITriggerable)emitterTriggerObject.GetComponent (typeof(ITriggerable));
		furnace = furnaceObject.GetComponent<Furnace> ();
		dragon = GameObject.FindGameObjectWithTag ("Dragon").GetComponent<Dragon> ();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (dragon.activated) {
			if (col.CompareTag ("Player")) {
				jawTriggerable.startTrigger (1);
				emitterTriggerable.startTrigger (1);
			}
		}
	}
	
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag("Player")) {
		}
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
		if (dragon.activated) {

			if (col.CompareTag ("Player")) {
				jawTriggerable.stopTrigger (1);
				emitterTriggerable.stopTrigger (1);
			}
		}
	}
}
