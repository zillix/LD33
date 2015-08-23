using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public GameObject introMask;

	void Awake()
	{
		housesDestroyed = new Dictionary<int, bool> ();
		reporters = new Dictionary<string, bool> ();

		GameObject dragonSpawn = GameObject.Find ("DragonSpawn");
		GameObject newDrago = Instantiate (dragonPrefab) as GameObject;
		newDrago.transform.position = dragonSpawn.transform.position;
		Debug.Log ("New dragon spawned at " + newDrago.transform.position);
	}

	// Use this for initialization
	void Start () {
		dragon = GameObject.FindGameObjectWithTag ("Dragon").GetComponent<Dragon> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		textManager = GameObject.FindGameObjectWithTag ("TextManager").GetComponent<TextManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
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
			PlayText.addText(message, "whoa!", player.gameObject);
			PlayText.addText(message, "hope that wasn't important...", player.gameObject);
			possibleMessages.Add (message);
			message = new List<PlayText>();
		} else if (houseDestroyedCount < 5) {
			PlayText.addText(message, "why are there so many of them?", player.gameObject);
			possibleMessages.Add (message);
			message = new List<PlayText>();

			PlayText.addText(message, "I just need to find the right one...", player.gameObject);
			possibleMessages.Add (message);
			message = new List<PlayText>();

			PlayText.addText(message, "they've really built this place up", player.gameObject);
			possibleMessages.Add (message);
			message = new List<PlayText>();

		} else if (houseDestroyedCount < 8) {
			PlayText.addText(message, "take that!", player.gameObject);
			possibleMessages.Add (message);
			message = new List<PlayText>();;
			PlayText.addText(message, "eat fire!", player.gameObject);
			possibleMessages.Add (message);
			message = new List<PlayText>();
			PlayText.addText(message, "watch it burn!", player.gameObject);
			possibleMessages.Add (message);
			message = new List<PlayText>();
			PlayText.addText(message, "feel the heat!", player.gameObject);
			possibleMessages.Add (message);
			message = new List<PlayText>();

		} else {
			PlayText.addText(message, "BURN IT ALL!", player.gameObject);
			possibleMessages.Add (message);
			message = new List<PlayText>();
			PlayText.addText(message, "BURN, PEASANTS!", player.gameObject);
			possibleMessages.Add (message);
			message = new List<PlayText>();
			PlayText.addText(message, "BOW TO THE CLEANSING FLAME!", player.gameObject);
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
			PlayText.addText (message, "and we're off!", player.gameObject);
		} else if (powerUpCount == 2) {
			PlayText.addText (message, "...and we're back!", player.gameObject);
		} else if (powerUpCount == 3) {
			
			PlayText.addText (message, "back in the air!", player.gameObject);
		} else if (powerUpCount == 4) {
			PlayText.addText (message, "wheee?", player.gameObject);
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
			PlayText.addText (message, "power's out!", player.gameObject);
		} else if (powerDownCount == 3) {
			
			PlayText.addText (message, "activate stealth mode!", player.gameObject);
		} else if (powerDownCount == 4) {
			PlayText.addText (message, "aaaahhhhhhhh!!!!!", player.gameObject);
		} else if (powerDownCount == 5) {
			// do nothing?
		}
		
		textManager.enqueue (message, true);
	}

	public void onEmitTime(float emitTime)
	{
		List<PlayText> message = new List<PlayText> ();

		if (emitTime > 5f && lastEmitMilestone < 5f) {
			PlayText.addText (message, "hm, not very impressive", player.gameObject);
			PlayText.addText (message, "looks like the furnace isn't very hot", player.gameObject);
			lastEmitMilestone = 5f;
		}

		textManager.enqueue (message, true);
	}

	public void reportSighting(string reporterName, GameObject spotter)
	{
		if (!reporters.ContainsKey (reporterName)) {
			reporters.Add(reporterName, true);

			List<PlayText> message = new List<PlayText> ();

			switch (reporterName)
			{
			case "first":
				PlayText.addText (message, "I.. I don't believe it!", spotter, 1.5f);
				PlayText.addText (message, "why is it here?", spotter, 1.5f);
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

				if (houseDestroyedCount == 1)
				{
					PlayText.addText (message, "it's in the cave now", spotter, 1.5f);
					PlayText.addText (message, "what does it want?", spotter, 1.5f);
				}
				else if (houseDestroyedCount < 4)
				{
					PlayText.addText (message, "it's broken through!", spotter, 1.5f);
					PlayText.addText (message, "fall back! fallback!", spotter, 1.5f);
				}
				else
				{
					PlayText.addText (message, "why here? why now?", spotter, 1.5f);
				}


				break;



			}

			textManager.enqueue (message);
		}

	}
}
