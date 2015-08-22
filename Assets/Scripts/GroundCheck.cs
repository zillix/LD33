using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

	private Player player;

	// Use this for initialization
	void Start () {
		player = gameObject.GetComponentInParent<Player> ();
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (!col.isTrigger) {
			player.grounded = true;
			if (col.CompareTag("DragonFrame"))
			{
				player.groundedFrame = true;
			}
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (!col.isTrigger) {
			player.grounded = true;
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (!col.isTrigger) {
			player.grounded = false;
		}
	}
}
