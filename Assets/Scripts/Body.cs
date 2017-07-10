using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {

	public bool isUsed;
	// Use this for initialization
	void Start () {
		
	}

	//Modifizierter Code aus der Unity Referenz https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
	public float speed;
	public float jumpSpeed;
	public float gravity;
	public float rotationSpeed;
	private Vector3 moveDirection = Vector3.zero;
	public GameObject headPosition;
	void Update() {
		
		CharacterController controller = GetComponent<CharacterController>();
		if (controller.isGrounded && isUsed) {
			moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;
			transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y+Input.GetAxis("Horizontal")*rotationSpeed,transform.eulerAngles.z);
		}
		
		if (controller.isGrounded && !isUsed) {
			moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;
			transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y+Input.GetAxis("Mouse X")*rotationSpeed,transform.eulerAngles.z);
		}
		
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}

	void OnControllerColliderHit(ControllerColliderHit collisionInfo){
		if(collisionInfo.gameObject.name == "Head"){
			collisionInfo.gameObject.transform.parent = headPosition.transform;
			collisionInfo.gameObject.transform.localPosition = Vector3.zero;
			collisionInfo.gameObject.transform.rotation = this.gameObject.transform.rotation;
			collisionInfo.gameObject.GetComponent<HeadBehaviour>().attachedToBody = true;
			isUsed = false;
		}
	}
}
