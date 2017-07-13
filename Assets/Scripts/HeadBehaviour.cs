using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Head class, requires ThrowableObject component
[RequireComponent(typeof(ThrowableObject))]
public class HeadBehaviour : MonoBehaviour {
	public bool groundTouching = false;
	public bool isFocusedOnBody = false;
	public bool attachedToBody;
	public GameObject laserPoint;
	public GameObject myCurrentBody;
	
	private float headRotationX;
	private float headRotationY;
	private float rotationSpeed = 180;
	private Vector3 rayForFocusingOnBody;
	private GameObject newLaserPoint;
	private bool laserCreated;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		attachedToBody = GetComponent<ThrowableObject>().attachedToBody;
		headRotationY = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
		transform.Rotate(Vector3.right * headRotationY);
		if (!attachedToBody)
		{
			//X Rotation only works without a body, otherwise turns body
			headRotationX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

			if(groundTouching){
				transform.Rotate(Vector3.up *  headRotationX);
				if(Input.GetKey(KeyCode.F)){
					if(!laserCreated){
						newLaserPoint = Instantiate(laserPoint);
						laserCreated = true;
					}
					FocusOnBody();
				}
				if(Input.GetKeyUp(KeyCode.F)){
					Destroy(newLaserPoint);
					newLaserPoint = null;
					laserCreated = false;
				}
			}
		} else {
			if(laserCreated){
				Destroy(newLaserPoint);
				newLaserPoint = null;
				laserCreated = false;
			}
				
		}
	}
	
	void OnCollisionStay(Collision collisionInfo){
		if(collisionInfo.gameObject.tag == "Ground"){
			groundTouching = true;
		}
	}


	void FocusOnBody(){
		//Throw a Raycast in Z direction and watch for bodies to be selected
		rayForFocusingOnBody = transform.TransformDirection(Vector3.forward);
		RaycastHit hit;
		Debug.Log(myCurrentBody);
        Debug.DrawRay(transform.position, rayForFocusingOnBody * 1000, Color.green);

        if (Physics.Raycast(transform.position, rayForFocusingOnBody, out hit, 1000)){
			newLaserPoint.transform.position = hit.point;
			if(hit.transform.tag == "Body"){
				Debug.Log("Körper entdeckt");
				isFocusedOnBody = true;
				if(myCurrentBody != null && myCurrentBody != hit.transform.gameObject){
					myCurrentBody.transform.gameObject.GetComponent<Body>().isUsed = false;
					myCurrentBody.transform.gameObject.GetComponent<Body>().headless = true;
				}
				myCurrentBody = hit.transform.gameObject;
				hit.transform.gameObject.GetComponent<Body>().isUsed = true;
			}
		}
	}
		
}
