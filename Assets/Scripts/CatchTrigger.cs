using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Trigger event sender for when a flying head is caught by a body
public class CatchTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider c) {
		if (c.gameObject.name == "Head") {
			transform.parent.SendMessage("CatchHead",c.gameObject);
		}
	}
}
