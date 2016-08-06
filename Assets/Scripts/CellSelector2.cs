using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CellSelector2 : MonoBehaviour {

	public float speedTranslationApply = 200f;
	public GameObject mark;
	public GameObject cellTemp;
	public GameObject cellIni;
    public GameObject cellFin;
    public GameObject characterTemp;
	public SphereCollider colliderTemp;
    private CellController cellControllerTemp;
	public GameObject[] cellArray;
	public static CellSelector2 instance;
	public bool settingMovement = false;
	public bool settingEnemy = false;
    public bool movement = false;
    public Text statusText;

    // Use this for initialization
    void Awake () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameManager.instance.finishCurrentGame && !GameManager.instance.isPaused && !movement) 
		{
			transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speedTranslationApply, 0,Input.GetAxis("Vertical") * Time.deltaTime * speedTranslationApply);
			if (Input.GetKeyDown (KeyCode.M)) 
			{
				if (settingMovement) {
					cellControllerTemp.character = null;
					cellControllerTemp = cellTemp.GetComponent<CellController> ();
					cellControllerTemp.character = characterTemp;
					colliderTemp.radius = 3f;
					colliderTemp.enabled = false;
					colliderTemp = null;
					characterTemp.transform.position.Set (cellTemp.transform.position.x, characterTemp.transform.position.y, cellTemp.transform.position.z);
					characterTemp.transform.FindChild ("battlecollider").GetComponent<SphereCollider> ().enabled = true;
                    //characterTemp.GetComponent<CharacterController>().moveTo(cellTemp);
                    GameManager.instance.arrowToFight.transform.position = cellTemp.transform.position;
                    GameManager.instance.UpdateTurn ();
					settingMovement = false;
				} else if (settingEnemy) {
                    cellControllerTemp = cellTemp.GetComponent<CellController>();
                    characterTemp.transform.LookAt(cellControllerTemp.character.transform.position);
                    cellControllerTemp.character.transform.LookAt(characterTemp.transform.position);
					characterTemp.SendMessage ("BeginAttack");
					cellControllerTemp.character.SendMessage ("BeginAttack");
                    //Debug.Log ("Sparta!!"+cellTemp.GetComponent<CellController> ().character);
                    if (characterTemp.tag == "Character")
                        GameManager.instance.character1 = characterTemp;
                    else
                        GameManager.instance.character2 = characterTemp;
                    if (cellControllerTemp.character.tag == "Character")
                        GameManager.instance.character1 = characterTemp;
                    else
                        GameManager.instance.character2 = characterTemp;
					GameManager.instance.LoadLevel ("BattleGround");
                }
                else
                {
				    cellControllerTemp = cellTemp.GetComponent<CellController> ();
                    if(cellControllerTemp.character != null) 
				    {
						if((cellControllerTemp.character.tag.Length == 9 && this.gameObject.tag.Length == 12)||(cellControllerTemp.character.tag.Length == 10 && this.gameObject.tag.Length == 13)){
						settingMovement = true;
                        characterTemp = cellControllerTemp.character;
						colliderTemp = cellTemp.transform.parent.GetComponent<SphereCollider> (); 
						colliderTemp.radius = colliderTemp.radius * characterTemp.GetComponent<CharacterController> ().cells;
						colliderTemp.enabled = true;
						}
					}
                }
			}
		}
        if (settingMovement)
        {
            statusText.text = "Mover";
        }
        else if (settingEnemy)
        {
            statusText.text = "Atacar";
        }
        else
        {
            statusText.text = "Seleccionar";
        }

    }

}
