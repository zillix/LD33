using UnityEngine;
using System.Collections;

public class Pump : MonoBehaviour {
	
	private bool isUp = true;
	private Animator anim;
	public GameObject triggerObject;
	private ITriggerable triggerable;
	private Dragon dragon;

	private bool overlapping = false;
	
	// Use this for initialization
	void Start () {
		triggerable = (ITriggerable)triggerObject.GetComponent (typeof(ITriggerable));
		anim = gameObject.GetComponent<Animator> ();
		dragon = GameObject.FindGameObjectWithTag ("Dragon").GetComponent<Dragon> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (overlapping) {
			float input = Input.GetAxisRaw("Vertical");
			int state = 0;
			if (input < 0)
			{
				state = -1;
			}
			else if (input > 0)
			{
				state = 1;
			}

			if ((state != -1) != isUp)
			{
				isUp = state != -1;
				triggerable.startTrigger(state);
				anim.SetBool ("up", isUp);
			}
		}
	}
	
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			overlapping = true;
		}
	}
	
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			overlapping = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			overlapping = false;
			isUp = true;
			triggerable.stopTrigger(0);
		}
	}
}
