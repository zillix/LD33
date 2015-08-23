using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class House : MonoBehaviour {

	public string reporterName;
	private bool hasReported;

	private Camera myCamera;
	private GameController game;

	private bool shattered = false;

	private SoundBank sounds;

	// Use this for initialization
	void Start () {
		myCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		game = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		sounds = GameObject.FindGameObjectWithTag ("SoundBank").GetComponent<SoundBank> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasReported && reporterName != null && reporterName != "")
		{
			float cameraRadius = Camera.main.orthographicSize * Screen.width / Screen.height;
			float cameraX = myCamera.transform.position.x;
			float myX = gameObject.transform.position.x + 2;
			if ((cameraX - cameraRadius) < myX && (cameraX + cameraRadius) >  myX)
			{
				hasReported = true;
				game.reportSighting(reporterName, gameObject);
			}
		}
	}

	public void pieceBroken()
	{
		if (!shattered) {
			shattered = true;
			gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
			sounds.player.PlayOneShot(sounds.houseBurst);

			List<Transform> children = new List<Transform>();

			foreach (Transform child in transform) {
				children.Add (child);
			}

			foreach (Transform child in children)
			{
				
				child.parent = null;
				
				child.gameObject.AddComponent<Rigidbody2D> ();
				
				child.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(70, 150), Random.Range(-50, 50), 0));

			}

			game.onHousePieceBroken(GetInstanceID());
		}
	}
}
