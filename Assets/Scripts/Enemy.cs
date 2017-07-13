using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CapsuleCollider))]
public class Enemy : MonoBehaviour {

	private GameObject ThrowObj;

	private Vector3 basePosition;
	private Quaternion baseRotation;

	private CharacterController controller;
	public float gravity;
	public GameObject headPosition;

	public float throwPower;
	public Vector3 throwDirection;

	private bool inPursuit;
	// Use this for initialization
	void Start () {
		basePosition = transform.position;
		baseRotation = transform.rotation;
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		//Only follow stray heads
		Vector3 moveDirection = new Vector3();
		if (inPursuit && ThrowObj.GetComponent<HeadBehaviour>() != null)
		{
			if (!ThrowObj.GetComponent<HeadBehaviour>().attachedToBody && ThrowObj.GetComponent<HeadBehaviour>().groundTouching)
			{
				moveDirection = Vector3.MoveTowards(transform.position,ThrowObj.transform.position,2f)-transform.position;
				transform.LookAt(ThrowObj.transform.position);
				transform.localEulerAngles = new Vector3(0,transform.localEulerAngles.y,0);
			} else {
				moveDirection = Vector3.MoveTowards(transform.position,basePosition,2f)-transform.position;
			}
		} else {
			moveDirection = Vector3.MoveTowards(transform.position,basePosition,2f)-transform.position;
			transform.LookAt(basePosition);
			transform.localEulerAngles = new Vector3(0,transform.localEulerAngles.y,0);
		}

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}

	//message receivers
	void ThrowableObjectFound (GameObject h) {
		Debug.Log("Found a head");
		ThrowObj = h;
		inPursuit = true;
	}

	void ThrowableObjectGone (GameObject h) {
		Debug.Log("Head is gone!");
		inPursuit = false;
	}



	//Take the head and throw it into direction
	void PickUpHead() {
		Debug.Log("Picking up Head");
		/*ThrowObj.GetComponent<Rigidbody>().isKinematic = true;
		ThrowObj.transform.parent = headPosition.transform;
		ThrowObj.gameObject.transform.localPosition = Vector3.zero;
		ThrowObj.gameObject.transform.rotation = headPosition.transform.rotation;
		ThrowObj.transform.parent = null;
		ThrowObj.GetComponent<Rigidbody>().isKinematic = false;*/
		inPursuit = false;
		ThrowObj.GetComponent<Rigidbody>().AddForce(new Vector3(transform.TransformDirection(Vector3.forward).x,1.7f*3,transform.TransformDirection(Vector3.forward).z)*100);

		//ThrowObj.SendMessage("GetThrown",new Vector3(throwPower,1.7f*throwPower,transform.TransformDirection(throwDirection).z*throwPower));
		//Debug.Log(transform.TransformDirection(throwDirection).x);
		//ThrowObj.GetComponent<Rigidbody>().AddForce(new Vector3(8*throwPower,1.7f*throwPower,transform.TransformDirection(throwDirection).z*throwPower));
	}
}
