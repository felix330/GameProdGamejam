using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBehaviourPlaceholder : MonoBehaviour {
	
	public static bool isFocused = false;
	private float bodyRotation;
	private float rotationSpeed = 150;
	private float bodyMovement;
	private float movementSpeed = 1;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		bodyRotation = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
		bodyMovement = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
		
		//Wenn der Kopf nicht auf einem Körper plaziert ist und den Bodenberührt, 
		//kann der Spieler ihn über A und D Rotieren. Mit F kann er einen Körper anvisieren und danach diesen aus SecondPerson-Sicht steuern
		if(isFocused){
			transform.Rotate(Vector3.up * bodyRotation);
			transform.Translate(Vector3.forward * bodyMovement);
		}
	}
	
	void OnCollisionEnter(Collision collisionInfo){
		if(collisionInfo.gameObject.name == "Head"){
			collisionInfo.gameObject.transform.parent = this.gameObject.transform;
			collisionInfo.gameObject.transform.rotation = this.gameObject.transform.rotation;
		}
	}
}
