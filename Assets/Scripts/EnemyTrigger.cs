using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Trigger for enemy to find objects
public class EnemyTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider c) {
		if (c.gameObject.tag == "ThrowableObject") {
			transform.parent.SendMessage("ThrowableObjectFound",c.gameObject);
		}
	}

	void OnTriggerExit (Collider c) {
		if (c.gameObject.tag == "ThrowableObject") {
			transform.parent.SendMessage("ThrowableObjectGone",c.gameObject);
		}
	}
}
