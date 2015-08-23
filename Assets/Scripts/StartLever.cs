using UnityEngine;
using System.Collections;

public class StartLever : MonoBehaviour {
	
	private bool enabled;
	private Animator anim;
	private Dragon dragon;
	
	// Use this for initialization
	void Start () {
		
		anim = gameObject.GetComponent<Animator> ();
		dragon = GameObject.FindGameObjectWithTag ("Dragon").GetComponent<Dragon> ();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetInteger ("state", enabled ? 1 : -1);
	}
	
	
	
	
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			int state = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
			
			if (state == 1)
			{
				enabled = true;
				dragon.activate ();
			}
			else if (state == -1)
			{
				enabled = false;
				dragon.deactivate();
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
	}
}
