using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public GameObject triggerObject;

	ITriggerable triggerable;

	void Start()
	{
		triggerable = (ITriggerable)triggerObject.GetComponent (typeof(ITriggerable));
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player")) {
			triggerable.startTrigger(1);
		}
	}
	
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag("Player")) {
		}
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag("Player")) {
			triggerable.stopTrigger(1);
		}
	}
}
