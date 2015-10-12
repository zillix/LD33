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

	public bool kidMode = true; // Toggles dialog that was built for oujevipo contest 2 - KIDS. Intentionally makes the text more adult-themed

	public bool french = true; // Toggles language! Overrides kidMode

	private string version = "v1.51";


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

		if (kidMode) {
			version += "k";
		}
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

			if (french)
			{
				textManager.enqueue ("soldat Mei, au rapport!", player.gameObject);
				textManager.enqueue ("c'est ma première mission pour le Conseil", player.gameObject);
				textManager.enqueue ("j'espère pouvoir les impressionner!", player.gameObject);
				textManager.enqueue ("maintenant, comment activer cette chose-là?", player.gameObject);
			}
			else if (kidMode)
			{
				textManager.enqueue ("private Mei, reporting for duty!", player.gameObject);
				textManager.enqueue ("this is my first mission for the council...", player.gameObject);
				textManager.enqueue ("I hope I can impress them!", player.gameObject);
				textManager.enqueue ("now, how do I turn this thing on?", player.gameObject);
			}
			else
			{
				textManager.enqueue ("how do I turn this thing on?", player.gameObject);
			}
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
		possibleMessages = getDestructionMessage (kidMode);
		
		
		int index = Random.Range (0, possibleMessages.Count);
		List<PlayText> messagesToSend = possibleMessages [index];
		textManager.enqueue(messagesToSend);
	}

	private List<List<PlayText>> getDestructionMessage(bool isKidMode)
	{
		List<List<PlayText>> possibleMessages = new List<List<PlayText>> ();
		List<PlayText> message = new List<PlayText> ();
		if (!isKidMode) {
			if (houseDestroyedCount == 1) {
				PlayText.addText (message, "whoa!", player.gameObject);
				PlayText.addText (message, "this thing has some power!", player.gameObject);

				possibleMessages.Add (message);
				message = new List<PlayText> ();
			} else if (houseDestroyedCount == 2) {
				PlayText.addText (message, "where's the target?", player.gameObject);
				PlayText.addText (message, "it's gotta be around somewhere...", player.gameObject);

				possibleMessages.Add (message);

				message = new List<PlayText> ();

			} else if (houseDestroyedCount < 5) {
				PlayText.addText (message, "nope, wasn't that one either", player.gameObject);
				PlayText.addText (message, "but I'll find it!", player.gameObject);
				possibleMessages.Add (message);
				message = new List<PlayText> ();
				
				PlayText.addText (message, "another one down!", player.gameObject);
				possibleMessages.Add (message);
				message = new List<PlayText> ();
				
				PlayText.addText (message, "boom!", player.gameObject);
				possibleMessages.Add (message);
				message = new List<PlayText> ();
			
			} else if (houseDestroyedCount < 8) {
				PlayText.addText (message, "they had it coming", player.gameObject);
				PlayText.addText (message, "...so I assume", player.gameObject);
				possibleMessages.Add (message);
				message = new List<PlayText> ();
				PlayText.addText (message, "I'll find the right one eventually...", player.gameObject);
				possibleMessages.Add (message);
				message = new List<PlayText> ();
				PlayText.addText (message, "get out of my way!", player.gameObject);
				possibleMessages.Add (message);
				message = new List<PlayText> ();
				PlayText.addText (message, "feel the heat!", player.gameObject);
				possibleMessages.Add (message);
				message = new List<PlayText> ();

			} else {
				PlayText.addText (message, "this is the best part of the job", player.gameObject);
				possibleMessages.Add (message);
				message = new List<PlayText> ();
				PlayText.addText (message, "this thing is amazing!", player.gameObject);
				possibleMessages.Add (message);
				message = new List<PlayText> ();
				PlayText.addText (message, "I love this job", player.gameObject);
				possibleMessages.Add (message);
				message = new List<PlayText> ();

			}
		} else {
			if (french)
			{
				switch (houseDestroyedCount)
				{
				case 1:
					PlayText.addText (message, "wahou!", player.gameObject);
					PlayText.addText (message, "ce dragon est puissant!", player.gameObject);
					break;

				case 2:
					PlayText.addText (message, "ce n'était pas le bon bâtiment", player.gameObject);
					PlayText.addText (message, "mais je le trouverai éventuellement!", player.gameObject);
					break;

				case 3:
					PlayText.addText (message, "les villageois ne semblent pas heureux...", player.gameObject);
					PlayText.addText (message, "mais j'ai une mission à accomplir!", player.gameObject);break;
					break;

				case 4:
					PlayText.addText (message, "un de plus!", player.gameObject);
					break;

				case 5:
					PlayText.addText (message, "je me demande pourquoi le Conseil m'a envoyé ici?", player.gameObject);
					PlayText.addText (message, "est-ce que ce village a fait quelque chose de mal?", player.gameObject);
					break;
					
				case 6:
					PlayText.addText (message, "pourquoi est-ce que je ne peux pas trouver le bon bâtiment?", player.gameObject); 
					break;

				case 7:
					PlayText.addText (message, "c'est plutôt amusant!", player.gameObject);
					break;
					
				case 8:
					PlayText.addText (message, "cette machine est incroyable!", player.gameObject);


					break;

				case 9:
					PlayText.addText (message, "boum!", player.gameObject);
					break;
					
				case 10:
					
					PlayText.addText (message, "c'est la meilleure partie de la mission!", player.gameObject);
					break;

				case 11:
					PlayText.addText (message, "brule! brule!", player.gameObject);
					break;
				}
			}
			else
			{
				switch (houseDestroyedCount)
				{
				case 1:
					PlayText.addText (message, "whoa!", player.gameObject);
					PlayText.addText (message, "this dragon has some power!", player.gameObject);
					break;
					
				case 2:
					PlayText.addText (message, "that wasn't the right building", player.gameObject);
					PlayText.addText (message, "but I'll find it eventually!", player.gameObject);
					break;
					
				case 3:
					PlayText.addText (message, "the villagers don't seem happy...", player.gameObject);
					PlayText.addText (message, "but, I have a job to do!", player.gameObject);break;
					break;
					
				case 4:
					PlayText.addText (message, "another one down!", player.gameObject);
					break;
					
				case 5:
					PlayText.addText (message, "I wonder why the council sent me here?", player.gameObject);
					PlayText.addText (message, "did this village do something wrong...?", player.gameObject);
					break;
					
				case 6:
					PlayText.addText (message, "why can't I find the right building?", player.gameObject);
					break;
					
				case 7:
					PlayText.addText (message, "this is kinda fun!", player.gameObject);
					break;
					
				case 8:
					PlayText.addText (message, "this thing is amazing!", player.gameObject);
					
					
					break;
					
				case 9:
					PlayText.addText (message, "ka-boom!", player.gameObject);
					break;
					
				case 10:
					
					PlayText.addText (message, "this is the best part of the job!", player.gameObject);
					break;
					
				case 11:
					PlayText.addText (message, "burn!!!", player.gameObject);
					break;
				}
			}

			possibleMessages.Add (message);
		}

		return possibleMessages;
	}

	public void onDragonActivated()
	{
		powerUpCount++;

		
		List<PlayText> message = new List<PlayText> ();
		bool force = true;

		if (french)
		{
			if (powerUpCount == 1) {
				PlayText.addText (message, "nous y voilà!", player.gameObject, 2f);
				PlayText.addText (message, "maintenant, j'ai une mission à faire", player.gameObject);
				PlayText.addText (message, "trouvons notre cible!", player.gameObject, 2f);
			} else 
			{
				PlayText.addText (message, "de nouveau dans les airs!", player.gameObject);
			}
		}
		else if (kidMode) {
			if (powerUpCount == 1) {
				PlayText.addText (message, "ha! there we go!", player.gameObject, 2f);
				PlayText.addText (message, "now, I'm supposed to destroy a particular building...", player.gameObject);
				PlayText.addText (message, "but I don't know what it looks like!", player.gameObject, 2f);
				PlayText.addText (message, "I'm sure I'll figure it out", player.gameObject, 2f);
			} else 
			{
				PlayText.addText (message, "...and we're back!", player.gameObject);
			}
		} else {
			if (powerUpCount == 1) {
				PlayText.addText (message, "ha! there we go!", player.gameObject, 2f);
				PlayText.addText (message, "now, I've got a job to do", player.gameObject, 2f);
				PlayText.addText (message, "let's find the target", player.gameObject, 2f);
			} else if (powerUpCount == 2) {
				PlayText.addText (message, "...and we're back!", player.gameObject);
			} else if (powerUpCount == 3) {
				
				PlayText.addText (message, "back in the air!", player.gameObject);
			} else if (powerUpCount == 4) {
				PlayText.addText (message, "this seems like the most productive use of my time", player.gameObject);
			} else if (powerUpCount == 5) {
				// do nothing?
			}
		}

		textManager.enqueue (message, true);

	}

	public void onDragonDeactivated()
	{
		powerDownCount++;
		
		List<PlayText> message = new List<PlayText> ();
		if (french) {
			if (powerDownCount == 1) {
				PlayText.addText (message, "oups!", player.gameObject);
			} else if (powerDownCount == 2) {
				PlayText.addText (message, "zut, pas encore!", player.gameObject);
			} else if (powerDownCount == 3) {
				
				PlayText.addText (message, "c'est pas comme si j'avais autre chose à faire", player.gameObject);
			} else 
			{// do nothing?
			}
		} else {
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
		}
		
		textManager.enqueue (message, true);
	}

	public void onEmitTime(float emitTime)
	{
		List<PlayText> message = new List<PlayText> ();
		if (french) {
			if (emitTime > 3f && lastEmitMilestone < 3f) {
				PlayText.addText (message, "looks like the furnace isn't very hot", player.gameObject);
				lastEmitMilestone = 3f;
			}
		} else {
			if (emitTime > 3f && lastEmitMilestone < 3f) {
				PlayText.addText (message, "cette fournaise ne me paraît pas très chaude", player.gameObject);
				lastEmitMilestone = 3f;
			}
		}

		textManager.enqueue (message, true);
	}

	public void reportSighting(string reporterName, GameObject spotter)
	{
		if (!reporters.ContainsKey (reporterName)) {
			reporters.Add(reporterName, true);

			List<PlayText> message = new List<PlayText> ();

			if (french)
			{
				switch (reporterName)
				{
				case "first":
					PlayText.addText (message, "c'est pas vrai!", spotter, 1.5f);
					PlayText.addText (message, "nous avons payé le tribut!", spotter, 1.5f);
					PlayText.addText (message, "sonnez l'alarme!", spotter, 1.5f);
					break;					
				case "side":
					
					PlayText.addText (message, "il a atteint le portail", spotter, 1.5f);
					
					if (houseDestroyedCount == 0)
					{
						PlayText.addText (message, "peut-être va-t-il reculer?", spotter, 1.5f);
					}
					else if (houseDestroyedCount < 3)
					{
						PlayText.addText (message, "préparez-vous!", spotter, 1.5f);
					}
					else
					{
						PlayText.addText (message, "nous sommes perdus!", spotter, 1.5f);
					}
					
					break;
					
					
				case "suspend":
					
					if (houseDestroyedCount <= 1)
					{
						PlayText.addText (message, "il est dans la grotte", spotter, 1.5f);
						PlayText.addText (message, "que veut-il de nous?", spotter, 1.5f);
					}
					else if (houseDestroyedCount < 4)
					{
						PlayText.addText (message, "il l'a percé!", spotter, 1.5f);
						PlayText.addText (message, "sonnez la retraite!", spotter, 1.5f);
					}
					else
					{
						PlayText.addText (message, "pourquoi ici, pourquoi maintenant?", spotter, 1.5f);
					}
					
					
					break;
					
				case "defend":
					
					PlayText.addText (message, "défendez la Relique!", spotter, 1.5f);
					
					if (houseDestroyedCount <= 1)
					{
						PlayText.addText (message, "peut-être n'a-t-il pas faim?", spotter, 1.5f);
					}
					else if (houseDestroyedCount < 6)
					{
						PlayText.addText (message, "c'est notre dermière chance!", spotter, 1.5f);
					}
					else
					{
						PlayText.addText (message, "non!", spotter, 1.5f);
						PlayText.addText (message, "nous étions si prêts du but!", spotter, 1.5f);
					}
					
					
					break;
					
					
				case "plea":
					
					if (houseDestroyedCount <= 1)
					{
						PlayText.addText (message, "nous sommes presque sains et saufs", spotter, 1.5f);
						PlayText.addText (message, "peut-être va-t-il ignorer la Relique?", spotter, 1.5f);
					}
					else if (houseDestroyedCount < 6)
					{
						PlayText.addText (message, "s'il vous plaît! ayez pitié de nous!", spotter, 1.5f);
						PlayText.addText (message, "epargnez la Relique", spotter, 1.5f);
					}
					else
					{
						PlayText.addText (message, "tous à la Relique!!", spotter, 1.5f);
						PlayText.addText (message, "défendez la à tout prix!!", spotter, 1.5f);
					}
					
					
					break;
					
					
					
					
				}
			}
			else
			{
				switch (reporterName)
				{
				case "first":
					if (kidMode)
					{
						PlayText.addText (message, "I don't believe it!", spotter, 1.5f);
						PlayText.addText (message, "we paid the tribute!", spotter, 1.5f);
						PlayText.addText (message, "sound the alarm!", spotter, 1.5f);
					}
					else
					{
						PlayText.addText (message, "I don't believe it!", spotter, 1.5f);
						PlayText.addText (message, "we paid the tribute!", spotter, 1.5f);
						PlayText.addText (message, "sound the alarm!", spotter, 1.5f);
					}
					break;

				case "side":

					PlayText.addText (message, "it has reached the gate", spotter, 1.5f);

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
						PlayText.addText (message, "what does it want from us?", spotter, 1.5f);
					}
					else if (houseDestroyedCount < 4)
					{
						PlayText.addText (message, "it has broken through!", spotter, 1.5f);
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

			if (french)
			{
				if (wonderDestroyed)
				{
					PlayText.addText (message, "mon devoir est accompli");
					
					if (houseDestroyedCount <= 1)
					{
						PlayText.addText (message, "un travail propre et net");
					}
					else if (houseDestroyedCount < 5)
					{
						PlayText.addText (message, "il y avait quelques malheuresuses victimes");
					}
					else if (houseDestroyedCount < 10)
					{
						PlayText.addText (message, "il n'y aura plus de résistance");
					}
					else
					{
						PlayText.addText (message, "la résistance est détruite");
					}
					PlayText.addText (message, "le Conseil sera satisfait");
					PlayText.addText (message, "une autre malencontreuse attaque de dragon!");
				}
				else
				{
					PlayText.addText (message, "la Relique est intacte");
					
					if (houseDestroyedCount <= 1)
					{
						PlayText.addText (message, "je ne serai pas le bourreau du Conseil");
					}
					else if (houseDestroyedCount < 5)
					{
						PlayText.addText (message, "quel sorte de mission était-ce, de toute façon?");
					}
					else if (houseDestroyedCount < 10)
					{
						PlayText.addText (message, "...je ne peux pas dire la même chose du village");
					}
					else
					{
						PlayText.addText (message, "elle est seule parmi les ruines");
					}
					
					PlayText.addText (message, "le Conseil ne sera pas satisfait");
					PlayText.addText (message, "mais je ne suis l'esclave de personne");
				}
			}
			else
			{
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
						PlayText.addText (message, "the resistance has been shattered");
					}
					PlayText.addText (message, "the council will be pleased");
					PlayText.addText (message, "another 'unfortunate' dragon attack successful!");
				}
				else
				{
					PlayText.addText (message, "the wonder remains intact");
					
					if (houseDestroyedCount <= 1)
					{
						PlayText.addText (message, "I won't be the council's executioner");
					}
					else if (houseDestroyedCount < 5)
					{
						PlayText.addText (message, "what kind of job was this, anyway?");
					}
					else if (houseDestroyedCount < 10)
					{
						PlayText.addText (message, "...I can't say the same for the village");
					}
					else
					{
						PlayText.addText (message, "it stands alone among the ruins");
					}
					
					PlayText.addText (message, "the council won't be pleased...");
					PlayText.addText (message, "...but I answer to no one");
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
		if (french) {
			if (kidMode) {
				PlayText.addText (message, "c'était la cible");
			}
			if (houseDestroyedCount <= 1)
			{
				PlayText.addText (message, "...c'est fait");
			}
			else if (houseDestroyedCount < 5)
			{
				PlayText.addText (message, "je l'ai fait!");
			}
			else if (houseDestroyedCount < 10)
			{
				PlayText.addText (message, "pourquoi se souciaient-ils tant de cette chose?");
			}
			else
			{
				PlayText.addText (message, "tout est détruit");
				
			}
		} else {
			if (kidMode) {
				PlayText.addText (message, "that was the target");
			}
			if (houseDestroyedCount <= 1)
			{
				PlayText.addText (message, "...it is done");
			}
			else if (houseDestroyedCount < 5)
			{
				PlayText.addText (message, "I did it!");
			}
			else if (houseDestroyedCount < 10)
			{
				PlayText.addText (message, "why do they care so much about this thing, anyway?");
			}
			else
			{
				PlayText.addText (message, "everything is destroyed!");
				
			}
		}
	
		textManager.enqueue (message);
	}

	public void onPumpUsed()
	{
		if (!notifiedPumpUsed && !dragon.activated && !textManager.isBusy) {
			notifiedPumpUsed = true;

			if (french)
			{
				textManager.enqueue("il semble attiser le feu", player.gameObject, 2.0f); // ???
			}
			else
			{
				textManager.enqueue("looks like it stokes the fire", player.gameObject, 2.0f);
			}
		}
	}

	public void onWheelUsed()
	{
		if (!notifiedWheelUsed && !dragon.activated&& !textManager.isBusy) {
			notifiedWheelUsed = true;

			if (french)
			{
				textManager.enqueue("tourne inutilement...", player.gameObject, 2.0f); // ???
			}
			else
			{	
				textManager.enqueue("spins uselessly...", player.gameObject, 2.0f);
			}
		}
	}

	public void onLeverUsed()
	{
		if (!notifiedLeverUsed && !dragon.activated&& !textManager.isBusy) {
			notifiedLeverUsed = true;

			if (french)
			{
				textManager.enqueue("semble manquer de puissance", player.gameObject, 2.0f); // ???
			}
			else
			{
				textManager.enqueue("seems to be missing power", player.gameObject, 2.0f);
			}
		}
	}

	public void onButtonUsed()
	{
		if (!notifiedButtonUsed && !dragon.activated) {
			notifiedButtonUsed = true;

			if (french)
			{
				textManager.enqueue("...pas de chance", player.gameObject, 2.0f); // ???
			}
			else
			{
				textManager.enqueue("...no luck", player.gameObject, 2.0f);
			}
		}
	}

	public void onRespawn()
	{
		if (french) {
			textManager.enqueue ("euh... prétendez que vous ne le voyez pas!", player.gameObject, 2.0f); // ???
		} else {
			textManager.enqueue ("um... pretend you didn't see that!", player.gameObject, 2.0f);
		}
	}
	
}
