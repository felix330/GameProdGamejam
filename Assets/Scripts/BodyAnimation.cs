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
			animator.SetFloat("WalkSpeed",body.transform.InverseTransformDirection(body.GetComponent<CharacterController>().velocity).z/3);
		} else {
			animator.SetFloat("WalkSpeed",0);
		}
	}
}
