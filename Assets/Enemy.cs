using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private GameObject head;

	private Vector3 basePosition;
	private Quaternion baseRotation;

	private CharacterController controller;
	public float gravity;

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
		if (inPursuit && head != null && !head.GetComponent<HeadBehaviour>().attachedToBody && head.GetComponent<HeadBehaviour>().groundTouching)
		{
			moveDirection = Vector3.MoveTowards(transform.position,head.transform.position,2f)-transform.position;
		} else {
			moveDirection = Vector3.MoveTowards(transform.position,basePosition,2f)-transform.position;
		}

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}

	//message receivers
	void HeadFound (GameObject h) {
		Debug.Log("Found a head");
		head = h;
		inPursuit = true;
	}

	void HeadGone (GameObject h) {
		Debug.Log("Head is gone!");
		inPursuit = false;
	}
}
