using UnityEngine;
using System.Collections;

public class shoot : MonoBehaviour
{
    public GameObject bulletObj;
    public GameObject tempbulletObj;
    public float force;
    
	public void Shoot(){
		tempbulletObj = (GameObject)Instantiate(bulletObj,transform.position,bulletObj.transform.rotation);
		tempbulletObj.transform.up = transform.forward;
		tempbulletObj.GetComponent<Rigidbody>().AddForce(transform.forward*force, ForceMode.Impulse);
	}
}
