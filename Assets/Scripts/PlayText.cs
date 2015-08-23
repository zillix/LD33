using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayText {

	public string text;
	public float duration;
	public delegate void Callback(); 
	public Callback callback;
	public GameObject speaker;

	public static float DEFAULT_DURATION = 3.5f;

	public PlayText(string Text = "", GameObject Speaker = null, float Duration = -1, Callback callbackFn = null)
	{
		if (Duration < 0) {
			Duration = DEFAULT_DURATION;
		}
		callback = callbackFn;
		text = Text;
		duration = Duration;
		speaker = Speaker;
	}

	public static void addText(List<PlayText> textList, string text, GameObject speaker = null, float duration = -1, Callback callback = null)
	{
		if (duration < 0) {
			duration = DEFAULT_DURATION;
		}
		textList.Add (new PlayText(text, speaker, duration, callback));
	}
}
