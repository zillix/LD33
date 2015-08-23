using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public GameObject jawTriggerObject;
	public GameObject emitterTriggerObject;
	public GameObject furnaceObject;
	private Dragon dragon;
	private GameController game;

	Furnace furnace;

	ITriggerable jawTriggerable;
	ITriggerable emitterTriggerable;

	private bool pressed = false;

	private Animator anim;

	public GameObject buttonObject;

	void Start()
	{
		jawTriggerable = (ITriggerable)jawTriggerObject.GetComponent (typeof(ITriggerable));
		emitterTriggerable = (ITriggerable)emitterTriggerObject.GetComponent (typeof(ITriggerable));
		furnace = furnaceObject.GetComponent<Furnace> ();
		dragon = GameObject.FindGameObjectWithTag ("Dragon").GetComponent<Dragon> ();
		anim = buttonObject.GetComponent<Animator> ();
		game = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}

	void Update()
	{
		anim.SetBool ("pressed", pressed);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) {
				
			if (dragon.activated) {

				jawTriggerable.startTrigger (1);
				emitterTriggerable.startTrigger (1);
			}

			pressed = true;

			game.onButtonUsed();
		}
	}
	
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag("Player")) {
		}
	}
	
	void OnTriggerExit2D(Collider2D col)
	{

		if (col.CompareTag ("Player")) {
			if (dragon.activated)
			{
				jawTriggerable.stopTrigger (1);
				emitterTriggerable.stopTrigger (1);
			}
				pressed = false;

		}
	}
}
