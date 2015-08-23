using UnityEngine;
using System.Collections;

public class Wonder : MonoBehaviour {

	public GameObject orb;

	private bool shattered = false;
	private GameController game;
	// Use this for initialization
	void Start () {
		game = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void pieceBroken()
	{
		if (!shattered) {
			shattered = true;

			foreach (Transform child in transform) {
				child.parent = null;
			}

			StartCoroutine(orb.GetComponent<Orb>().fadeOut(1));

			game.onWonderDestroyed();
		}

	}
}
