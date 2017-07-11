using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
	public Transform myTargetHead;
	public Transform myTargetBody;
	private Vector3 headPosition;
	private Vector3 bodyPosition;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(myTargetHead.GetComponent<HeadBehaviour>().attachedToBody){
			myTargetBody = myTargetHead.transform.parent.parent;
			bodyPosition = myTargetBody.position;
			transform.LookAt(myTargetBody);
			transform.position = new Vector3(bodyPosition.x+7, bodyPosition.y+2, bodyPosition.z-5.35f);
		} else {
			headPosition = myTargetHead.position;
			transform.LookAt(myTargetHead);
			transform.position = new Vector3(headPosition.x+4, headPosition.y+3, headPosition.z-1.35f);
		}
	}
}
