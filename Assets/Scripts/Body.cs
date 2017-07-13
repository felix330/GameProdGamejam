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

	//Enthält modifizierten Code aus der Unity Referenz https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
	public float speed;
	public float jumpSpeed;
	public float gravity;
	public float rotationSpeed;
	private Vector3 moveDirection = Vector3.zero;
	public GameObject hand;

	private GameObject connectedObject;
	private bool pickingUp;

	void Update() {
		
		CharacterController controller = GetComponent<CharacterController>();
		
		if(controller.isGrounded && !pickingUp){
			if (headless && isUsed) {
				//Debug.Log("Ich werde beobachtet.");
				moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= speed;
				if (Input.GetButton("Jump")) {
					BroadcastMessage("ReceiveJump");
					moveDirection.y = jumpSpeed;
				}
				transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y+Input.GetAxis("Horizontal")*rotationSpeed,transform.eulerAngles.z);
			}
			
			if (!headless && !isUsed) {
				//Debug.Log("Habe meinen Kopf gefunden.");
				moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= speed;
				if (Input.GetButton("Jump")) {
					BroadcastMessage("ReceiveJump");
					moveDirection.y = jumpSpeed;
				}
				if(Input.GetAxis("Horizontal") != 0){
					transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y+Input.GetAxis("Horizontal")*rotationSpeed,transform.eulerAngles.z);
				} else {
					
					
					transform.eulerAngles = new Vector3(transform.eulerAngles.x/*+Input.GetAxis("Mouse Y")*rotationSpeed*2*/,transform.eulerAngles.y+Input.GetAxis("Mouse X")*rotationSpeed*2,transform.eulerAngles.z);
				}
			}
			
			if (headless && !isUsed) {
				//Debug.Log("Ich bin kopflos.");
			}
			
		}

		if (pickingUp)
		{
			moveDirection.x = 0;
			moveDirection.z = 0;
		}
		
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}

	//Collect objects
	void OnControllerColliderHit(ControllerColliderHit collisionInfo){
		if(collisionInfo.gameObject.tag == "ThrowableObject" && !gotAnObject && !pickingUp){
			//gotAnObject = true;
			Debug.Log("ReceivePickup");
			BroadcastMessage("ReceivePickUp");
			connectedObject = collisionInfo.gameObject;
			pickingUp = true;
			/*collisionInfo.gameObject.transform.parent = headPosition.transform;
			collisionInfo.gameObject.transform.localPosition = Vector3.zero;
			collisionInfo.gameObject.transform.rotation = headPosition.transform.rotation;
			collisionInfo.gameObject.GetComponent<ThrowableObject>().attachedToBody = true;			
			if(collisionInfo.gameObject.name == "Head"){
				collisionInfo.gameObject.GetComponent<HeadBehaviour>().attachedToBody = true;
				isUsed = false;
				headless = false;
			}*/
			
		}
	}

	void PickUpAttach() {
		connectedObject.transform.parent = headPosition.transform;
		connectedObject.transform.localPosition = Vector3.zero;
		connectedObject.transform.rotation = headPosition.transform.rotation;
		connectedObject.GetComponent<ThrowableObject>().attachedToBody = true;
		connectedObject.GetComponent<ThrowableObject>().myBody = gameObject;
		if(connectedObject.gameObject.name == "Head"){
			connectedObject.gameObject.GetComponent<HeadBehaviour>().attachedToBody = true;
			isUsed = false;
			headless = false;
		}
		headPosition.transform.parent = hand.transform;
		headPosition.transform.localPosition = new Vector3(-0.7f,0.25f,0.14f);
	}

	void HoldUpAttach() {
		gotAnObject = true;
		if (headless){
			headPosition.transform.parent = hand.transform;
		} else {
			headPosition.transform.parent = transform;
			headPosition.transform.localEulerAngles = Vector3.zero;
			headPosition.transform.localPosition = new Vector3(0.09f,1.4f,-0.14f);
		}

		pickingUp = false;
	}

	void CatchHead(GameObject h)
	{
		if (headless && !gotAnObject && h.GetComponent<ThrowableObject>().myBody != gameObject)
		{

			headPosition.transform.localPosition = new Vector3(0.09f,1.4f,-0.14f);
			headPosition.transform.localEulerAngles = Vector3.zero;
			h.transform.parent = headPosition.transform;
			h.transform.localPosition = Vector3.zero;
			h.transform.rotation = headPosition.transform.rotation;
			h.GetComponent<ThrowableObject>().attachedToBody = true;
			h.GetComponent<ThrowableObject>().myBody = gameObject;
			h.gameObject.GetComponent<HeadBehaviour>().attachedToBody = true;

			isUsed = false;
			headless = false;
			gotAnObject = true;
			BroadcastMessage("HeadCatch");
		}
	}
}
