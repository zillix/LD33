using UnityEngine;
using System.Collections;

public class Pump : MonoBehaviour {
	
	private bool isUp = true;
	private Animator anim;
	public GameObject triggerObject;
	private ITriggerable triggerable;
	private Dragon dragon;
	
	// Use this for initialization
	void Start () {
		triggerable = (ITriggerable)triggerObject.GetComponent (typeof(ITriggerable));
		anim = gameObject.GetComponent<Animator> ();
		dragon = GameObject.FindGameObjectWithTag ("Dragon").GetComponent<Dragon> ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	
	
	
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			int state = Mathf.RoundToInt(Input.GetAxis("Vertical"));
			isUp = state != -1;
			triggerable.startTrigger(state);
			anim.SetBool ("up", isUp);
		}
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			isUp = true;
			triggerable.stopTrigger(0);
		}
	}
}
