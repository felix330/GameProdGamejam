using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateButtonBehaviour : MonoBehaviour {
	public List<string> gateKeys = new List<string>();
	public List<GameObject> gates = new List<GameObject>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter(Collision collisionInfo){
		if(gateKeys.Contains(collisionInfo.gameObject.name)){
			//Debug.Log(collisionInfo.gameObject.name);
			foreach(string key in gateKeys){
				foreach(GameObject gate in gates){
					//Debug.Log(key + " + " + gate);
					gate.GetComponent<GateBehaviour>().SendMessage("OpenGate", key);
				}
			}
		}
	}
}
