using UnityEngine;
using System.Collections;

public class SoundBank : MonoBehaviour {

	public AudioSource player;

	public AudioClip playerLand;
	public AudioClip powerOn;
	public AudioClip powerOff;
	public AudioClip adjustTail;
	public AudioClip buttonPress;
	public AudioClip firebreathing;
	public AudioClip houseBurst;
	public AudioClip pump;
	public AudioClip wheel;

	// Use this for initialization
	void Start () {
		player = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
