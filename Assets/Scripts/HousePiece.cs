using UnityEngine;
using System.Collections;

public class HousePiece : MonoBehaviour {
	
	Collider2D collider2d;

	public int health = 2;

	GameController game;

	// Use this for initialization
	void Start () {
		collider2d = gameObject.GetComponent<BoxCollider2D> ();
		game = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		
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

					
					game.onHousePieceBroken(gameObject.transform.parent.GetInstanceID());
					gameObject.GetComponentInParent<Rigidbody2D>().isKinematic = false;
					gameObject.transform.parent = null;
					gameObject.AddComponent<Rigidbody2D> ();

					//gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(1, 0, 0));
				}
				//collider2d.attachedRigidbody.SendMessage ("OnCollisionEnter2D", collision);
			}
		}
	}
}