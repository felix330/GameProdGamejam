using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour {

	private Animator animator;
	private GameObject body;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		body = transform.parent.gameObject;
	}

	// Update is called once per frame
	void Update () {
		Debug.Log(animator.GetFloat("WalkSpeed"));
		if (body.GetComponent<CharacterController>().isGrounded)
		{
			animator.SetBool("Jump",false);
			animator.SetFloat("WalkSpeed",body.transform.InverseTransformDirection(body.GetComponent<CharacterController>().velocity).z/2.6f);
		} else {
			animator.SetFloat("WalkSpeed",0);
		}

	}
}
