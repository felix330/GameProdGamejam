using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowBar : MonoBehaviour {

	public GameObject head;
	private float value;
	private float currentPower;
	private float min;
	private float max;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (head.GetComponent<HeadBehaviour>().ThrowMode)
		{
			GetComponent<RectTransform>().localScale = Vector2.one;
			//Scales Throw Power between 1 and 0
			currentPower = head.GetComponent<HeadBehaviour>().throwPower;
			max = head.GetComponent<HeadBehaviour>().maxThrowPower;
			min = head.GetComponent<HeadBehaviour>().minThrowPower;
			value = (currentPower-min)/(max-min);
			GetComponent<Scrollbar>().value = value;
		} else {
			GetComponent<RectTransform>().localScale = Vector2.zero;
		}
	}
}
