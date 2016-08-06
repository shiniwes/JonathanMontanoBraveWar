using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public GameObject enemy;
    public GameObject tmpEnemy;
	// Use this for initialization
	void Start () {
        StartCoroutine("Spawn");
	}

    IEnumerator Spawn()
    {
        if(!CameraManager.instance.isPaused&& !CameraManager.instance.isIntro&& !CameraManager.instance.finish)
        {
            Debug.Log("Spawn!!");
            tmpEnemy = (GameObject)Instantiate(enemy, transform.position, transform.rotation);
        }
        yield return new WaitForSeconds(3);
        StartCoroutine("Spawn");
    }
}
