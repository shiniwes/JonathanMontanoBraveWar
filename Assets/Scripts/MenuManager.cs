using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
	public static MenuManager instance;
	public Button continueButton;
	public Button newGameButton;
	public Button vsButton;
	public Button optionsButton;
	// Use this for initialization
	void Awake () {
		instance = this;
	}
	void Start () {
		if (GameManager.instance.haveSave)
			continueButton.interactable = true;
		else
			continueButton.interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
