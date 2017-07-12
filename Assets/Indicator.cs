using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Shows a possessed body
public class Indicator : MonoBehaviour {

	private Body b;
	// Use this for initialization
	void Start () {
		b = transform.parent.gameObject.GetComponent<Body>();
	}
	
	// Update is called once per frame
	void Update () {
		if (b.headless && b.isUsed){
			GetComponent<MeshRenderer>().enabled = true;
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,transform.localEulerAngles.y+1,transform.localEulerAngles.z);
		} else {
			GetComponent<MeshRenderer>().enabled = false;
		}
	}
}
