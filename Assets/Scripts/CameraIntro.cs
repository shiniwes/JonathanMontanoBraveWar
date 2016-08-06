using UnityEngine;
using System.Collections;

public class CameraIntro : MonoBehaviour {
    
	void OnEnable () {
        CameraManager.instance.ShowMenuInit();
    }
}
