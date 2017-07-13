using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsFall : MonoBehaviour {

	public int secondsToFall;
	private bool hitGround;
	// Use this for initialization
	void Start () {
		hitGround = false;
		StartCoroutine(FallAfterSeconds());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator FallAfterSeconds()
	{
		yield return new WaitForSeconds(secondsToFall);
		GetComponent<Rigidbody>().isKinematic = false;
	}

	void OnCollisionEnter (Collision c)
	{
		if (!GetComponent<Rigidbody>().isKinematic && c.gameObject.tag == "Ground" && c.gameObject.name != "Cell_1" && !hitGround){
			GetComponent<AudioSource>().Play();
			hitGround = true;
		}
	}
}
