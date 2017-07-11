using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBar : MonoBehaviour {

	public GameObject head;
	private float value;
	private float min;
	private float max;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		max = head.GetComponent<HeadBehaviour>().maxThrowPower;
		min = head.GetComponent<HeadBehaviour>().minThrowPower;
		value = 1/(max-min);
		Debug.Log(value);
	}
}
