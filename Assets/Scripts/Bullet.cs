using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public GameObject explosion;
    public GameObject decal;
    public GameObject tempdecal;

    void OnCollisionEnter(Collision bulletCollision)
    {
        if(bulletCollision.transform.tag != "Player") {
            Debug.Log("Choca:"+ bulletCollision.transform.name);
			if (bulletCollision.transform.tag == "Enemy")
				//Destroy (bulletCollision.gameObject);
				bulletCollision.gameObject.SendMessage("Damage");
            //Quaternion colRotation = Quaternion.FromToRotation(decal.transform.up, bulletCollision.contacts[0].normal);
            //tempdecal = (GameObject)Instantiate(decal, bulletCollision.contacts[0].point+(bulletCollision.contacts[0].normal)*(0.001f), colRotation);
            //tempdecal = (GameObject)Instantiate(decal, bulletCollision.contacts[0].point+(bulletCollision.contacts[0].normal)*(0.001f), Quaternion.identity);
            
			//tempdecal.transform.forward = -bulletCollision.contacts[0].normal;
            //tempdecal.transform.SetParent(bulletCollision.transform);
            //Destroy(Instantiate(explosion, bulletCollision.contacts[0].point, Quaternion.identity),2f);
            Destroy(this.gameObject);
        }
    }
}
