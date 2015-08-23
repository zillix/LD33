using UnityEngine;
using System.Collections;
using System;

public class HousePiece : MonoBehaviour {
	
	Collider2D collider2d;

	public int health = 2;

	GameController game;
	SpriteRenderer sprite;

	
	int parentID;

	// Use this for initialization
	void Start () {
		collider2d = gameObject.GetComponent<BoxCollider2D> ();
		game = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		parentID = gameObject.transform.parent.GetInstanceID();
		sprite = gameObject.GetComponent<SpriteRenderer> ();

		
	}
	
	// Update is called once per frame
	void Update () {
		if (sprite.color.a <= 0) {
			Destroy(gameObject);
		}
	}

	public IEnumerator fadeOut(float fadeDur)
	{
		for (float timer = fadeDur; timer >= 0; timer-= Time.deltaTime) {
			Color color = sprite.color;
			color.a = timer / fadeDur;
			sprite.color = color;
			yield return null;
		}
		
		Color color2= sprite.color;
		color2.a = 0f;
		sprite.color = color2;
		
		
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

					StartCoroutine(fadeOut(1.0f));

				}
			}
		}
	}
}