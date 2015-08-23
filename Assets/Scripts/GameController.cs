using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

	private Dragon dragon;
	private Player player;
	private TextManager textManager;

	private Dictionary<int, bool> housesDestroyed;
	private Dictionary<string, bool> reporters;

	public int houseDestroyedCount = 0;

	private int powerDownCount = 0;
	private int powerUpCount = 0;

	public GameObject dragonPrefab;

	private float lastEmitMilestone = 0f;

	private bool startedEnding = false;

	public GameObject introMask;

	private bool wonderDestroyed = false;
	private bool gameStarted = false;

	private GameObject endPoint;
	public GameObject blackScreen;

	private bool notifiedPumpUsed = false;
	private bool notifiedButtonUsed = false;
	private bool notifiedWheelUsed = false;
	private bool notifiedLeverUsed = false;

	private SoundBank sounds;

	private string version = "v1.01";

	void Awake()
	{
		housesDestroyed = new Dictionary<int, bool> ();
		reporters = new Dictionary<string, bool> ();

		GameObject dragonSpawn = GameObject.Find ("DragonSpawn");
		GameObject newDrago = Instantiate (dragonPrefab) as GameObject;
		newDrago.transform.position = dragonSpawn.transform.position;
		endPoint = GameObject.Find ("EndGamePoint");
		sounds = GameObject.FindGameObjectWithTag ("SoundBank").GetComponent<SoundBank> ();
		GameObject.Find("versionText").GetComponent<Text>().text = version;
	}

	// Use this for initialization
	void Start () {
		dragon = GameObject.FindGameObjectWithTag ("Dragon").GetComponent<Dragon> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		textManager = GameObject.FindGameObjectWithTag ("TextManager").GetComponent<TextManager> ();
	
	}

	void onKeyPressed()
	{
		if (!gameStarted) {
			gameStarted = true;
			GameObject.Find("zillixText").SetActive(false);
			GameObject.Find ("startText").SetActive(false);
			GameObject.Find ("versionText").SetActive(false);

			
		//	textManager.enqueue ("looks like this hasn't been used in a long time", player.gameObject);
			textManager.enqueue ("how do I turn this thing on?", player.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (dragon.transform.position.x > endPoint.transform.position.x) {
			endGame();
		}

		if (Input.anyKeyDown) {
			onKeyPressed();
		}
	}

	public void onHousePieceBroken(int parentID)
	{
		bool value = false;
		bool success = housesDestroyed.TryGetValue (parentID, out value);
		if (!success && !value) {
			incrementDestruction();
		}

		if (!success) {
			housesDestroyed.Add (parentID, true);
		}
	}

	private void incrementDestruction()
	{
		houseDestroyedCount++;

		List<List<PlayText>> possibleMessages = new List<List<PlayText>> ();

		List<PlayText> message = new List<PlayText> ();
		if (houseDestroyedCount == 1) {
			PlayText.addText (message, "whoa!", player.gameObject);
			PlayText.addText (message, "hope that wasn't important...", player.gameObject);
			possibleMessages.Add (message);
			message = new List<PlayText> ();
		} else if (houseDestroyedCount == 2) {
			PlayText.addText(message, "hm, that wasn't the right one", player.gameObject);
			possibleMessages.Add (message);
			PlayText.addText(message, "where is it?", player.gameObject);
			possibleMessages.Add (message);

			message = new List<PlayText>();

		} else if (houseDestroyedCount < 5) {
			PlayText.addText(message, "they said it would be here...", player.gameObject);
			possibleMessages.Add (message);
			message = new List<PlayText>();

			PlayText.addText(message, "this wasn't part of the job...", player.gameObject);
			possibleMessages.Add (message);
			message = new List<PlayText>();

			PlayText.addText(message, "the council didn't mention anything about inhabitants...", player.gameObject);
			possibleMessages.Add (message);
			message = new List<PlayText>();

		} else if (houseDestroyedCount < 8) {
			PlayText.addText(message, "they had it coming", player.gameObject);
			possibleMessages.Add (message);
			message = new List<PlayText>();;
			PlayText.addText(message, "I'll find the right one eventually...", player.gameObject);
			possibleMessages.Add (message);
			message = new List<PlayText>();
			PlayText.addText(message, "get out of my way!", player.gameObject);
			possibleMessages.Add (message);
			message = new List<PlayText>();
			PlayText.addText(message, "feel the heat!", player.gameObject);
			possibleMessages.Add (message);
			message = new List<PlayText>();

		} else {
			PlayText.addText(message, "this is the best part of the job", player.gameObject);
			possibleMessages.Add (message);
			message = new List<PlayText>();
			PlayText.addText(message, "this thing is amazing!", player.gameObject);
			possibleMessages.Add (message);
			message = new List<PlayText>();
			PlayText.addText(message, "I love this job", player.gameObject);
			possibleMessages.Add (message);
			message = new List<PlayText>();

		}

		int index = Random.Range (0, possibleMessages.Count);
		List<PlayText> messagesToSend = possibleMessages [index];
		textManager.enqueue(messagesToSend);
	}

	public void onDragonActivated()
	{
		powerUpCount++;

		
		List<PlayText> message = new List<PlayText> ();
		bool force = true;

		if (powerUpCount == 1) {
			PlayText.addText (message, "ha! there we go!", player.gameObject);
			PlayText.addText (message, "it works, just liked they said!", player.gameObject);
		} else if (powerUpCount == 2) {
			PlayText.addText (message, "...and we're back!", player.gameObject);
		} else if (powerUpCount == 3) {
			
			PlayText.addText (message, "back in the air!", player.gameObject);
		} else if (powerUpCount == 4) {
			PlayText.addText (message, "this seems like the most productive use of my time", player.gameObject);
		} else if (powerUpCount == 5) {
			// do nothing?
		}

		textManager.enqueue (message, true);

	}

	public void onDragonDeactivated()
	{
		powerDownCount++;
		
		List<PlayText> message = new List<PlayText> ();
		
		if (powerDownCount == 1) {
			PlayText.addText (message, "whoops!", player.gameObject);
		} else if (powerDownCount == 2) {
			PlayText.addText (message, "argh, not again!", player.gameObject);
		} else if (powerDownCount == 3) {
			
			PlayText.addText (message, "activate stealth mode!", player.gameObject);
		} else if (powerDownCount == 4) {
			PlayText.addText (message, "it's not like I have a deadline...", player.gameObject);
		} else if (powerDownCount == 5) {
			// do nothing?
		}
		
		textManager.enqueue (message, true);
	}

	public void onEmitTime(float emitTime)
	{
		List<PlayText> message = new List<PlayText> ();

		if (emitTime > 5f && lastEmitMilestone < 5f) {
			//PlayText.addText (message, "hm, not very impressive", player.gameObject);
			//PlayText.addText (message, "looks like the furnace isn't very hot", player.gameObject);
			lastEmitMilestone = 5f;
		}

		//textManager.enqueue (message, true);
	}

	public void reportSighting(string reporterName, GameObject spotter)
	{
		if (!reporters.ContainsKey (reporterName)) {
			reporters.Add(reporterName, true);

			List<PlayText> message = new List<PlayText> ();

			switch (reporterName)
			{
			case "first":
				PlayText.addText (message, "I don't believe it!", spotter, 1.5f);
				PlayText.addText (message, "we paid the tribute!", spotter, 1.5f);
				PlayText.addText (message, "sound the alarm!", spotter, 1.5f);
				break;

			case "side":

				PlayText.addText (message, "it's reached the gate", spotter, 1.5f);

				if (houseDestroyedCount == 0)
				{
					PlayText.addText (message, "maybe it'll turn back...?", spotter, 1.5f);
				}
				else if (houseDestroyedCount < 3)
				{
					PlayText.addText (message, "brace yourselves!", spotter, 1.5f);
				}
				else
				{
					PlayText.addText (message, "we're done for!", spotter, 1.5f);
				}

				break;


			case "suspend":

				if (houseDestroyedCount <= 1)
				{
					PlayText.addText (message, "it's in the cave now", spotter, 1.5f);
					PlayText.addText (message, "what does it want?", spotter, 1.5f);
				}
				else if (houseDestroyedCount < 4)
				{
					PlayText.addText (message, "it's broken through!", spotter, 1.5f);
					PlayText.addText (message, "fall back! fall back!", spotter, 1.5f);
				}
				else
				{
					PlayText.addText (message, "why here? why now?", spotter, 1.5f);
				}


				break;

			case "defend":

				PlayText.addText (message, "defend the wonder!", spotter, 1.5f);
				
				if (houseDestroyedCount <= 1)
				{
					PlayText.addText (message, "maybe it's not hungry...", spotter, 1.5f);
				}
				else if (houseDestroyedCount < 6)
				{
					PlayText.addText (message, "this is our last chance!", spotter, 1.5f);
				}
				else
				{
					PlayText.addText (message, "this can't be!", spotter, 1.5f);
					PlayText.addText (message, "we were so close!", spotter, 1.5f);
				}
				
				
				break;


			case "plea":
				
				if (houseDestroyedCount <= 1)
				{
					PlayText.addText (message, "we're almost in the clear...", spotter, 1.5f);
					PlayText.addText (message, "maybe it will ignore the wonder", spotter, 1.5f);
				}
				else if (houseDestroyedCount < 6)
				{
					PlayText.addText (message, "please! have mercy!", spotter, 1.5f);
					PlayText.addText (message, "spare the wonder!", spotter, 1.5f);
				}
				else
				{
					PlayText.addText (message, "all hands to the wonder!", spotter, 1.5f);
					PlayText.addText (message, "defend it at all costs!", spotter, 1.5f);
				}
				
				
				break;




			}

			textManager.enqueue (message);
		}

	}

	public void endGame()
	{
		if (!startedEnding) {
			startedEnding = true;
			StartCoroutine(fadeOut(2));
			List<PlayText> message = new List<PlayText> ();

			if (wonderDestroyed)
			{
				PlayText.addText (message, "my duty has been fulfilled");

				if (houseDestroyedCount <= 1)
				{
					PlayText.addText (message, "in and out. a clean job");
				}
				else if (houseDestroyedCount < 5)
				{
					PlayText.addText (message, "there were a few unfortunate casualties");
				}
				else if (houseDestroyedCount < 10)
				{
					PlayText.addText (message, "there will be no more resistance");
				}
				else
				{
					PlayText.addText (message, "the council will be pleased");
				}
				PlayText.addText (message, "another successful 'unfortunate' dragon attack");
			}
			else
			{
				PlayText.addText (message, "the wonder remains intact");

				if (houseDestroyedCount <= 1)
				{
					PlayText.addText (message, "the council won't be pleased");
					PlayText.addText (message, "...but I won't be their executioner");
				}
				else if (houseDestroyedCount < 5)
				{
					PlayText.addText (message, "what kind of job was, this, anyway?");

				}
				else if (houseDestroyedCount < 10)
				{
					PlayText.addText (message, "...I can't say the same for the village");
				}
				else
				{
					PlayText.addText (message, "it stands alone among the ruins");
					PlayText.addText (message, "let the council do what they may");	
					PlayText.addText (message, "I answer to no one");

				}
			}

			PlayText.addText(message, "", null, .1f, queueRestartGame);

			textManager.enqueue (message);
		}
	}

	private void queueRestartGame()
	{
		StartCoroutine (restartGame());
	}


	private IEnumerator restartGame()
	{
		yield return new WaitForSeconds(3f);

		Application.LoadLevel (Application.loadedLevel);
	}

	
	public IEnumerator fadeOut(float fadeDur)
	{
		SpriteRenderer sprite = blackScreen.GetComponent<SpriteRenderer> ();
		blackScreen.SetActive (true);

		for (float timer = fadeDur; timer >= 0; timer-= Time.deltaTime) {
			Color color = sprite.color;
			color.a = 1f - timer / fadeDur;
			sprite.color = color;
			yield return null;
		}
		
		Color color2= sprite.color;
		color2.a = 1f;
		sprite.color = color2;
		
		
	}

	public void onWonderDestroyed()
	{
		wonderDestroyed = true;

		List<PlayText> message = new List<PlayText> ();
		if (houseDestroyedCount <= 1)
		{
			PlayText.addText (message, "...it is done");
		}
		else if (houseDestroyedCount < 5)
		{
			PlayText.addText (message, "looks like I got it!");
		}
		else if (houseDestroyedCount < 10)
		{
			PlayText.addText (message, "why do they care so much about this thing, anyway?");
		}
		else
		{
			PlayText.addText (message, "a wonderful finish to a successful mission");
			
		}
		textManager.enqueue (message);
	}

	public void onPumpUsed()
	{
		if (!notifiedPumpUsed && !dragon.activated) {
			notifiedPumpUsed = true;

			textManager.enqueue("looks like it stokes the fire", player.gameObject, 2.0f);
		}
	}

	public void onWheelUsed()
	{
		if (!notifiedWheelUsed && !dragon.activated) {
			notifiedWheelUsed = true;
			
			textManager.enqueue("spins uselessly", player.gameObject, 2.0f);
		}
	}

	public void onLeverUsed()
	{
		if (!notifiedLeverUsed && !dragon.activated) {
			notifiedLeverUsed = true;
			
			textManager.enqueue("seems to be missing power", player.gameObject, 2.0f);
		}
	}

	public void onButtonUsed()
	{
		if (!notifiedLeverUsed && !dragon.activated) {
			notifiedLeverUsed = true;
			
			textManager.enqueue("no luck", player.gameObject, 2.0f);
		}
	}
	
}
