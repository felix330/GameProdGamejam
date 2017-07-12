using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBehaviour : MonoBehaviour {
	public GameObject myKey;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OpenGate(GameObject _key){
		if(_key == myKey){
			gameObject.SetActive(false);
		}
	}
}
