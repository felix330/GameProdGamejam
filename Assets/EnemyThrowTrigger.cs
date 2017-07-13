using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrowTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider c) {
		if (c.gameObject.tag == "ThrowableObject" && !c.gameObject.GetComponent<HeadBehaviour>().attachedToBody)
		{
			Debug.Log("Entered trigger");
			SendMessageUpwards("PickUpHead");
		}
	}
}
