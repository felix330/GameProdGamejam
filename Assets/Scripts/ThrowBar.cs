using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//UI element which shows Throw Power
public class ThrowBar : MonoBehaviour {

	private float value;
	private float currentPower;
	private float min;
	private float max;

	// Use this for initialization
	void Start () {
		GetComponent<RectTransform>().localScale = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void ChangeScrollbar(GameObject _throwingObject){
		if (_throwingObject.GetComponent<ThrowableObject>().ThrowMode) {
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
