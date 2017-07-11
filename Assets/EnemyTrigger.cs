using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider c)
	{
		if (c.gameObject.tag == "Head")
		{
			transform.parent.SendMessage("HeadFound",c.gameObject);
		}
	}

	void OnTriggerExit (Collider c)
	{
		if (c.gameObject.tag == "Head")
		{
			transform.parent.SendMessage("HeadGone",c.gameObject);
		}
	}
}
