using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private GameObject ThrowObj;

	private Vector3 basePosition;
	private Quaternion baseRotation;

	private CharacterController controller;
	public float gravity;
	public GameObject headPosition;

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
		if (inPursuit && ThrowObj != null && !ThrowObj.GetComponent<HeadBehaviour>().attachedToBody && ThrowObj.GetComponent<HeadBehaviour>().groundTouching)
		{
			moveDirection = Vector3.MoveTowards(transform.position,ThrowObj.transform.position,2f)-transform.position;
		} else {
			moveDirection = Vector3.MoveTowards(transform.position,basePosition,2f)-transform.position;
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

	void OnCollisionEnter(Collision c) {
		if (c.gameObject.tag == "ThrowableObject" && !ThrowObj.GetComponent<HeadBehaviour>().attachedToBody)
		{
			PickUpHead();
		}
	}

	void PickUpHead()
	{
		Debug.Log("Picking up Head");
		ThrowObj.transform.parent = headPosition.transform;
		ThrowObj.gameObject.transform.localPosition = Vector3.zero;
		ThrowObj.gameObject.transform.rotation = headPosition.transform.rotation;
	}
}
