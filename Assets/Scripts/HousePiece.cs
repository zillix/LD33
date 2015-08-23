using UnityEngine;
using System.Collections;
using System;

public class HousePiece : MonoBehaviour {
	
	Collider2D collider2d;

	public int health = 2;

	GameController game;

	int parentID;

	// Use this for initialization
	void Start () {
		collider2d = gameObject.GetComponent<BoxCollider2D> ();
		game = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		parentID = gameObject.transform.parent.GetInstanceID();


	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D( Collider2D col) {

		if (health > 0) {
			if (col.CompareTag("Fireball")) {
				health--;

				
				// Break apart!
				if (health <= 0) {

					if (gameObject.transform.parent != null)
					{
						gameObject.SendMessageUpwards ("pieceBroken", SendMessageOptions.DontRequireReceiver);

					}

				}
			}
		}
	}
}