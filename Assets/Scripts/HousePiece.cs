using UnityEngine;
using System.Collections;

public class HousePiece : MonoBehaviour {
	
	Collider2D collider2d;

	public int health = 2;

	// Use this for initialization
	void Start () {
		collider2d = gameObject.GetComponent<BoxCollider2D> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter2D( Collision2D collision) {

		if (health > 0) {
			if (collision.gameObject.tag == "Fireball") {
				health--;

				// Break apart!
				if (health <= 0) {
					gameObject.transform.parent = null;
					gameObject.AddComponent<Rigidbody2D> ();
				}
				//collider2d.attachedRigidbody.SendMessage ("OnCollisionEnter2D", collision);
			}
		}
	}
}