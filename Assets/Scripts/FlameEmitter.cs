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

	private float timeEmitting = 0f;
	private float heatDrainTime = 0f;

	private float heatDrainInterval = 2.2f;

	private GameController game;

	private SoundBank sounds;
	private AudioSource fireSounds;


	// Use this for initialization

	void Awake()
	{
		fireSounds = gameObject.AddComponent<AudioSource> ();
		fireSounds.loop = true;
	}

	void Start () {
		furnace = furnaceObject.GetComponent<Furnace> ();
		game = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		sounds = GameObject.FindGameObjectWithTag ("SoundBank").GetComponent<SoundBank> ();
		fireSounds.clip = sounds.firebreathing;
	}
	
	// Update is called once per frame
	void Update () {

		if (isEmitting) {
			particleSpawnTimer -= Time.deltaTime;
			if (particleSpawnTimer <= 0) {
				particleSpawnTimer = particleSpawnRate * (1 - (furnace.heat - 1) / 6);
				emit ();
			}

			
			
			heatDrainTime += Time.deltaTime;
			timeEmitting += Time.deltaTime;
			
			if (heatDrainTime > heatDrainInterval) {
				furnace.drainHeat ();
				heatDrainTime = 0;
			}

			if (furnace.heat == 1)
			{
				game.onEmitTime (timeEmitting);
			}

		} else {
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

		fireball.GetComponent<Fireball> ().lifetime = .4f + .2f * (furnace.heat - 1);

		//Debug.Log ("Spawning fireball at " + fireball.transform.position);

	}

	public void startTrigger(float value)
	{
		if (!isEmitting) {
			isEmitting = true;
			fireSounds.Play ();
		}

	}

	public void stopTrigger(float value)
	{
		if (isEmitting) {
			isEmitting = false;
			fireSounds.Stop ();
		}
	}

	public void FixedUpdate()
	{
	}




}
