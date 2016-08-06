using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

	public Transform targetTransform;
	public float distance;
	public float height;
	public float duration;

	private Vector3 camPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
			camPosition = targetTransform.position - transform.forward * distance + transform.up * height;
			transform.position = Vector3.Lerp (transform.position, camPosition, Time.deltaTime * duration);
			transform.LookAt (targetTransform.position);
	}

}
