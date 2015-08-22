using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

	Rigidbody2D rb2d;
	SpriteRenderer sprite;
	public float lifetime = 1f;

	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		sprite = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (lifetime > 0) {
			lifetime -= Time.deltaTime;
			if (lifetime <= 0)
			{
				StartCoroutine (fadeOut (.1f));
			}
		}


		if (sprite.color.a <= 0) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (!col.gameObject.CompareTag ("DragonFrame") && !col.gameObject.CompareTag("Dragon") 
		    && !col.gameObject.CompareTag("Fireball")) {
			StartCoroutine (fadeOut (.5f));
			rb2d.velocity = new Vector2();
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
}
