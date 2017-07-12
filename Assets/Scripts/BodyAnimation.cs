using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyAnimation : MonoBehaviour {

	private Animator animator;
	private GameObject body;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		body = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (body.GetComponent<CharacterController>().isGrounded)
		{
			animator.SetBool("Jump",false);
			animator.SetFloat("WalkSpeed",body.transform.InverseTransformDirection(body.GetComponent<CharacterController>().velocity).z/2.6f);
		} else {
			animator.SetFloat("WalkSpeed",0);
		}

		if (!body.GetComponent<Body>().gotAnObject)
		{
			animator.SetBool("Holding",false);
		} else {
			animator.SetBool("Holding",true);
		}
	}

	void ReceiveJump() {
		animator.SetBool("Jump",true);
	}

	void ReceiveThrow() {
		animator.SetTrigger("Thrown");
	}

	void ReceivePickUp() {
		animator.SetTrigger("PickUpStart");
	}

	void PickUpStart()
	{
		Debug.Log("Pickup start");
		SendMessageUpwards("PickUpAttach");
	}

	void HoldUpDone()
	{
		Debug.Log("Hold up done");
		SendMessageUpwards("HoldUpAttach");
	}
}
