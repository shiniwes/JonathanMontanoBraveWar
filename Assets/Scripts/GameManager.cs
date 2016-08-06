using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public bool haveSave = false;
	public bool finishCurrentGame = false;
	public bool isPaused = false;
    public enum GameMode { MAIN_MENU, STRATEGY, BATTLE };
    public enum StrategyPlayer { P1, P2 };
    public StrategyPlayer strategyPhase;
    public GameMode gameMode;
	public GameObject menuPausa;
	public GameObject menuFinish;
    public int numCharacters1 = 0;
    public int numCharacters2 = 0;
	public int movesLeft;
	public GameObject sel1;
	public GameObject sel2;
	public GameObject part1;
	public GameObject part2;
    public GameObject camera;
    public GameObject arrowToFight;
    public GameObject character1;
    public GameObject character2;
	private string levelName;

    void Awake() {
		DontDestroyOnLoad (transform.gameObject);
		instance = this;
		gameMode = GameMode.MAIN_MENU;
		SceneManager.sceneLoaded += SceneLoaded;
    }

    public void UpdateTurn()
    {
		movesLeft--;
        if (gameMode == GameMode.STRATEGY)
        {
			if (strategyPhase == StrategyPlayer.P1 && movesLeft == 0)
            {
				strategyPhase = StrategyPlayer.P2;
				movesLeft = numCharacters2;
				sel1.SetActive(false);
				part1.SetActive(false);
				sel2.SetActive(true);
				part2.SetActive(true);
				camera.GetComponent<ThirdPersonCamera2> ().targetTransform = sel2.transform;
        	}
			else if(strategyPhase == StrategyPlayer.P2 && movesLeft == 0)
			{
				strategyPhase = StrategyPlayer.P1;
				movesLeft = numCharacters1;
				sel1.SetActive(true);
				part1.SetActive(true);
				sel2.SetActive(false);
				part2.SetActive(false);
				camera.GetComponent<ThirdPersonCamera2> ().targetTransform = sel1.transform;
			}
        }
    }
	void Update () {
		if(!finishCurrentGame)
		{
			if(Input.GetKeyDown(KeyCode.P))
				setMenuPause();
		}
	}
    public void NextLevel(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex+1);
	}

	public void LoadLevel(string levelName){
		if (levelName == "StoryLevel1")
			gameMode = GameMode.STRATEGY;
		else if (levelName == "BattleGround")
			gameMode = GameMode.BATTLE;
		SceneManager.LoadScene (levelName);
		if (levelName == "MainMenu")
			Destroy (this.gameObject);
	}
	void SceneLoaded(Scene scene, LoadSceneMode m)
	{
		Debug.Log ("Escena cargada");
		levelName = SceneManager.GetActiveScene ().name;
		if (levelName == "StoryLevel1")
			gameMode = GameMode.STRATEGY;
		else if (levelName == "BattleGround")
			gameMode = GameMode.BATTLE;
		if (gameMode == GameMode.STRATEGY)
		{
			strategyPhase = StrategyPlayer.P1;
			sel1 = GameObject.FindGameObjectWithTag ("CellSelector");
			sel1.SetActive(true);
			sel2 = GameObject.FindGameObjectWithTag ("CellSelector2");
			sel2.SetActive(false);
			part1 = GameObject.FindGameObjectWithTag ("Particles");
			part1.SetActive(true);
			part2 = GameObject.FindGameObjectWithTag ("Particles2");
			part2.SetActive(false);
			numCharacters1 = GameObject.FindGameObjectsWithTag ("Character").Length;
			numCharacters2 = GameObject.FindGameObjectsWithTag ("Character2").Length;
			movesLeft = numCharacters1;
			camera = GameObject.FindGameObjectWithTag("MainCamera");
			camera.GetComponent<ThirdPersonCamera2> ().targetTransform = sel1.transform;
		}
	}

	public void setMenuPause()
	{
		menuPausa.SetActive (true);
	}

	public void quitMenuPause()
	{
		menuPausa.SetActive (false);
	}

	public void setMenuFinish()
	{
		menuFinish.SetActive (true);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
