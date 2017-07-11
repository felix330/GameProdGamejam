using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowBar : MonoBehaviour {

	//public GameObject throwingObject;
	private float value;
	private float currentPower;
	private float min;
	private float max;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void ChangeScrollbar(GameObject _throwingObject){
		if (_throwingObject.GetComponent<ThrowableObject>().ThrowMode)
		{
			GetComponent<RectTransform>().localScale = Vector2.one;
			//Scales Throw Power between 1 and 0
			currentPower = _throwingObject.GetComponent<ThrowableObject>().throwPower;
			max = _throwingObject.GetComponent<ThrowableObject>().maxThrowPower;
			min = _throwingObject.GetComponent<ThrowableObject>().minThrowPower;
			value = (currentPower-min)/(max-min);
			GetComponent<Scrollbar>().value = value;
		} else {
			GetComponent<RectTransform>().localScale = Vector2.zero;
		}
	}
}
