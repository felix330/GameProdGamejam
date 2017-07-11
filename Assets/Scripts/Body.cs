using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {

	public bool isUsed = false;
	public bool headless = true;
	public bool gotAnObject = false;
	public GameObject headPosition;
	public float maxRotation, minRotation;
	// Use this for initialization
	void Start () {
		  Cursor.lockState = CursorLockMode.Locked;
	}

	//Modifizierter Code aus der Unity Referenz https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
	public float speed;
	public float jumpSpeed;
	public float gravity;
	public float rotationSpeed;
	private Vector3 moveDirection = Vector3.zero;
	
	void Update() {
		
		CharacterController controller = GetComponent<CharacterController>();
		
		if(controller.isGrounded){
			if (headless && isUsed) {
				//Debug.Log("Ich werde beobachtet.");
				moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= speed;
				if (Input.GetButton("Jump"))
					moveDirection.y = jumpSpeed;
				transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y+Input.GetAxis("Horizontal")*rotationSpeed,transform.eulerAngles.z);
			}
			
			if (!headless && !isUsed) {
				//Debug.Log("Habe meinen Kopf gefunden.");
				moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= speed;
				if (Input.GetButton("Jump"))
					moveDirection.y = jumpSpeed;
				
				if(Input.GetAxis("Horizontal") != 0){
					transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y+Input.GetAxis("Horizontal")*rotationSpeed,transform.eulerAngles.z);
				} else {
					/*if(transform.eulerAngles.x > maxRotation){
						transform.eulerAngles = new Vector3(maxRotation,transform.eulerAngles.y,transform.eulerAngles.z);
					}else if(transform.eulerAngles.x < minRotation){
						transform.eulerAngles = new Vector3(minRotation,transform.eulerAngles.y,transform.eulerAngles.z);
					}*/
					
					transform.eulerAngles = new Vector3(transform.eulerAngles.x/*+Input.GetAxis("Mouse Y")*rotationSpeed*2*/,transform.eulerAngles.y+Input.GetAxis("Mouse X")*rotationSpeed*2,transform.eulerAngles.z);
				}
			}
			
			if (headless && !isUsed) {
				//Debug.Log("Ich bin kopflos.");
			}
			
		}
		
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}

	void OnControllerColliderHit(ControllerColliderHit collisionInfo){
		if(collisionInfo.gameObject.tag == "ThrowableObject" && !gotAnObject){
			gotAnObject = true;
			collisionInfo.gameObject.transform.parent = headPosition.transform;
			collisionInfo.gameObject.transform.localPosition = Vector3.zero;
			collisionInfo.gameObject.transform.rotation = headPosition.transform.rotation;
			collisionInfo.gameObject.GetComponent<ThrowableObject>().attachedToBody = true;			
			if(collisionInfo.gameObject.name == "Head"){
				collisionInfo.gameObject.GetComponent<HeadBehaviour>().attachedToBody = true;
				isUsed = false;
				headless = false;
			}
			
		}
	}
}
