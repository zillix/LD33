using UnityEngine;
using System.Collections;

public class Orb : MonoBehaviour {

	float desiredAngle = 0;
	float rotationSpeed = 200f;
	bool destroyed = false;

	SpriteRenderer sprite;
	
	// Use this for initialization
	void Start () {
		sprite = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (!destroyed) {
			transform.rotation = Quaternion.Euler (0, 0, desiredAngle);
			desiredAngle += rotationSpeed * Time.deltaTime;
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
