using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {
	public AudioSource audiosource;
	public AudioClip audioclip;
	// Use this for initialization
	void Start () {
		audiosource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
