using UnityEngine;
using System.Collections;

public class CellSelectorController : MonoBehaviour {

	private Camera currentCamera;
	private RaycastHit rayHit;
	private Ray mouseRay;
	public GameObject cross;
	public float rayDistance;
	public float force;

	// Use this for initialization
	void Start () 
	{
		currentCamera = GetComponent<Camera> ();
	}

	// Update is called once per frame
	void Update () 
	{
		SetCross ();
		if (Input.GetMouseButtonDown (0))
			LaunchRayCast ();
	}

	private void LaunchRayCast()
	{
		mouseRay = currentCamera.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast (mouseRay,out rayHit, rayDistance))
		{
			if (rayHit.transform.tag == "Sphere") 
			{
				//rayHit.rigidbody.AddForce (rayHit.transform.forward*force,ForceMode.Acceleration);
				rayHit.rigidbody.AddForce (-rayHit.normal*force,ForceMode.Acceleration);
			}
		}
	}

	private void SetCross()
	{
		RaycastHit rayHit2;
		mouseRay = currentCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast (mouseRay, out rayHit2, rayDistance)) {
			if (rayHit2.transform.tag != "Player" && rayHit2.transform.tag != "MainCamera") 
			{
				cross.SetActive (true);
				//rayHit.rigidbody.AddForce (rayHit.transform.forward*force,ForceMode.Acceleration);
				//rayHit2.rigidbody.AddForce (mouseRay.direction*force,ForceMode.Acceleration);
				cross.transform.position = rayHit2.point + (0.1f * rayHit2.normal);
				cross.transform.forward = -rayHit2.normal;
			}
			else 
			{
				cross.SetActive (false);
			}
		}
		else
		{
			cross.SetActive (false);
		}
	}
}
