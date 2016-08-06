using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GlobalAudioManager : MonoBehaviour {

	public AudioMixer globalMixer;
	public Slider globalSlider;
	public Slider musicSlider;
	public Slider fxSlider;

	// Use this for initialization
	public void SetGlobalVolume () {
		globalMixer.SetFloat ("masterVolume",globalSlider.value);
	}
	// Use this for initialization
	public void SetFXVolume () {
		globalMixer.SetFloat ("soundFXVolume",fxSlider.value);
	}
	// Use this for initialization
	public void SetMusicVolume () {
		globalMixer.SetFloat ("musicVolume",musicSlider.value);
	}

}
