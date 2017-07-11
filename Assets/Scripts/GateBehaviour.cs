using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBehaviour : MonoBehaviour {
	public string myKey;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OpenGate(string _key){
		if(_key == myKey){
			gameObject.SetActive(false);
		}
	}
}
