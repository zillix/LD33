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

				
				child.gameObject.AddComponent<Rigidbody2D> ();
				
				child.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(70, 150), Random.Range(-50, 50), 0));

			}

			StartCoroutine(orb.GetComponent<Orb>().fadeOut(1));

			game.onWonderDestroyed();
		}

	}
}
