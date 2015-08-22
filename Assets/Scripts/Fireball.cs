using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

	Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
