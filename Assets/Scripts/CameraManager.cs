using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    public static CameraManager instance;
    public Camera cameraIntro;
    public Camera cameraMain;
    public Camera cameraMap;
    public GameObject menuInit;
    public bool isPaused;
    public bool isIntro;
    public bool finish;

    void Awake()
    {
        instance = this;
        isPaused = false;
        isIntro = true;
        finish = false;
    }

    // Use this for initialization
    void Start () {
        cameraIntro.enabled = true;
        cameraMain.enabled = false;
        cameraMap.enabled = false;
        menuInit.SetActive(false);
    }
    public void ShowMenuInit()
    {
        menuInit.SetActive(true);
        isPaused = true;
        isIntro = false;
    }
    public void StartLevel()
    {
        menuInit.SetActive(false);
        cameraMain.enabled = true;
        cameraMap.enabled = true;
        cameraIntro.enabled = false;
        isPaused = false;
    }
    public void FinishLevel()
    {
        finish = true;
    }

}
