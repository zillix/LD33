using UnityEngine;
using System.Collections;

public class House : MonoBehaviour {

	public string reporterName;
	private bool hasReported;

	private Camera myCamera;
	private GameController game;

	// Use this for initialization
	void Start () {
		myCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		game = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasReported)
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
}
