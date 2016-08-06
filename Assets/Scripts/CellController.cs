using UnityEngine;
using System.Collections;

public class CellController : MonoBehaviour {
	public GameObject character;
	public bool canBeSelected = false;
	public CellSelector2 cellSel;

    void Update()
	{
		if (cellSel != null) {
			if (!cellSel.settingMovement) {
				canBeSelected = false;
			} else if (canBeSelected) {
			
			}
		}
    }
    
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "CellSelector") {
			cellSel = other.GetComponent<CellSelector2> ();
			if (cellSel.settingMovement && canBeSelected && (character == null || character == cellSel.characterTemp)) 
			{
				cellSel.characterTemp.GetComponent<CharacterController>().moveTo(transform.parent.gameObject);
				cellSel.mark.transform.position = transform.position;
				cellSel.cellTemp = this.gameObject;
			}
            else if(!cellSel.settingMovement){
				cellSel.mark.transform.position = transform.position;
				cellSel.cellTemp = this.gameObject;
                if (cellSel.settingEnemy) {
                    GameManager.instance.arrowToFight.transform.LookAt(transform.position);
                    GameManager.instance.arrowToFight.SetActive(true);
                }
			}
		}
		else if (other.transform.tag == "CellSelector2") {
			cellSel = other.GetComponent<CellSelector2> ();
			if (cellSel.settingMovement && canBeSelected && (character == null || character == cellSel.characterTemp)) 
			{
				cellSel.characterTemp.GetComponent<CharacterController>().moveTo(transform.parent.gameObject);
				cellSel.mark.transform.position = transform.position;
				cellSel.cellTemp = this.gameObject;
			}else if(!cellSel.settingMovement){
				cellSel.mark.transform.position = transform.position;
				cellSel.cellTemp = this.gameObject;
                if (cellSel.settingEnemy)
                {
                    GameManager.instance.arrowToFight.transform.LookAt(transform.position);
                    GameManager.instance.arrowToFight.SetActive(true);
                }
            }
		}
		else if(other.transform.tag == "Cell")
		{
			canBeSelected = true;
		}
		if(character != null)
		{
			//Debug.Log (other.transform.tag+" vs "+character.tag);
			if(other.transform.tag == "battlecollider1" && character.tag == "Character2")
			{
				//other.transform.SendMessage ("canAttack");
				GameObject.FindGameObjectWithTag("CellSelector").GetComponent<CellSelector2>().settingEnemy = true;
				//Debug.Log("Puedes atacar");
			}
			else if(other.transform.tag == "battlecollider2" && character.tag == "Character")
			{
				//other.transform.SendMessage ("canAttack");
				GameObject.FindGameObjectWithTag("CellSelector2").GetComponent<CellSelector2>().settingEnemy = true;
				//Debug.Log("Puedes atacar");
			}
			if (other.transform.tag == "battlecollider1") {
				other.transform.GetComponent<SphereCollider> ().enabled = false;
			}
		}
	}
	private void OnTriggerExit(Collider other)
	{
		//Debug.Log("Sale:"+ other.transform.name);
		if(other.transform.tag == "Cell")
		{
			canBeSelected = false;
		}
	}
}
