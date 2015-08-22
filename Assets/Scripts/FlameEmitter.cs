using UnityEngine;
using System.Collections;

public class FlameEmitter : MonoBehaviour, ITriggerable {

	public GameObject fireballPrefab;

	public float coneAngle;
	public float particleSpeed;
	public float particleSpawnTimer;

	public float particleSpawnRate;

	private bool isEmitting = false;

	public GameObject furnaceObject;
	Furnace furnace;


	// Use this for initialization
	void Start () {
		furnace = furnaceObject.GetComponent<Furnace> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (isEmitting) {
			particleSpawnTimer -= Time.deltaTime;
			if (particleSpawnTimer <= 0) {
				particleSpawnTimer = particleSpawnRate;
				emit ();
			}
		}
	
	}

	void emit()
	{
		GameObject fireball = Instantiate (fireballPrefab, transform.position, transform.rotation) as GameObject;
		fireball.transform.SetParent (GameObject.Find ("Fireballs").transform);

		Quaternion offset = Quaternion.Euler (0, 0, Random.Range (-coneAngle, coneAngle));

		fireball.transform.rotation = transform.rotation * offset;
		Vector2 fireballVelocity = transform.rotation * offset * new Vector2 (particleSpeed, 0);
		fireball.GetComponent<Rigidbody2D> ().velocity = fireballVelocity;

		fireball.GetComponent<Fireball> ().lifetime = .5f * furnace.heat;

		//Debug.Log ("Spawning fireball at " + fireball.transform.position);

	}

	public void startTrigger(float value)
	{
		isEmitting = true;

	}

	public void stopTrigger(float value)
	{
		isEmitting = false;
	}

	public void FixedUpdate()
	{
	}




}
