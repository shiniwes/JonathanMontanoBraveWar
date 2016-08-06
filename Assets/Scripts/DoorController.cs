using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {
	public AudioSource audioSource;
	public AudioClip doorAudio;

	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	public void OpenDoor () {
		Debug.Log ("Abre");
		StartCoroutine ("PlayDoorSound");
	}
	
	// Update is called once per frame
	public void CloseDoor () {
		Debug.Log ("Cierra");
		//StartCoroutine ("PlayDoorSound");
	}

	IEnumerator PlayDoorSound(){
		audioSource.PlayOneShot (doorAudio);
		yield return new WaitForSeconds (1.5f);
		audioSource.Stop ();
	}

}
