﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextManager : MonoBehaviour {

	public List<PlayText> textQueue;

	private PlayText currentText;
	private float currentDuration;

	private ITextBox textBox;
	public GameObject textBoxObject;

	void Awake()
	{
		textQueue = new List<PlayText> ();

	}

	// Use this for initialization
	void Start () {
		textBox = (ITextBox)textBoxObject.GetComponent (typeof(ITextBox));
		textBoxObject.SetActive (true);
		textBox.hide ();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentText != null) {
			if (currentDuration < currentText.duration)
			{
				currentDuration += Time.deltaTime;
				if (currentDuration >= currentText.duration)
				{
					advanceText();
				}

			}
		}
		else if (textQueue.Count > 0)
		{
			advanceText();
		}
	}
	
	public void enqueue(List<PlayText> text, bool force = false)
	{
		if (text.Count == 0) {
			return;
		}

		if (force) {
			textQueue.Clear ();
			currentText = null;
			currentDuration = 0;
		}
		textQueue.AddRange (text);
	}

	public void enqueue(string text, GameObject speaker, float duration = -1, PlayText.Callback callback = null)
	{
		textQueue.Add (new PlayText (text, speaker, duration, callback));
	}

	private void advanceText()
	{
		currentDuration = 0;

		if (currentText != null) {
			if (currentText.callback != null)
			{
				currentText.callback();
			}
		}

		if (textQueue.Count > 0) {
			textBox.show();
			currentText = textQueue [0];
			textQueue.RemoveAt (0);
			textBox.text = currentText.text;
			textBox.target = currentText.speaker;
		} else {
			reset();
		}
	}

	private void reset()
	{
		currentText = null;
		currentDuration = 0;
		textBox.hide();
	}

	public bool isBusy
	{
		get {
			return currentText != null;
		}
	}


}
