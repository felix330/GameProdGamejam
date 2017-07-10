using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBehaviour : MonoBehaviour {
	public bool groundTouching = false;
	public bool isFocusedOnBody = false;
	
	private float headRotation;
	private float rotationSpeed = 180;
	private Vector3 rayForFocusingOnBody;
	public bool attachedToBody;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!attachedToBody)
		{
			GetComponent<Rigidbody>().isKinematic = false;
			headRotation = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
			
			if(!isFocusedOnBody && groundTouching){
				transform.Rotate(Vector3.up * headRotation);
				if(Input.GetKey(KeyCode.F)){
					FocusOnBody();
				}
			}
		} else {
			GetComponent<Rigidbody>().isKinematic = true;
		}
	}
	
	void OnCollisionStay(Collision collisionInfo){
		if(collisionInfo.gameObject.tag == "Ground"){
			groundTouching = true;
		}
	}
	
	void FocusOnBody(){
		rayForFocusingOnBody = transform.TransformDirection(Vector3.forward);
		RaycastHit hit;
		
		//Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, rayForFocusingOnBody * 5, Color.green);

        if (Physics.Raycast(transform.position, rayForFocusingOnBody, out hit, 5)){
			if(hit.transform.tag == "Body"){
				Debug.Log("Körper entdeckt");
				isFocusedOnBody = true;
				hit.transform.gameObject.GetComponent<Body>().isUsed = true;
			}
		}
	}
		
		
}
