using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictionLine : MonoBehaviour {

	public GameObject ball;
	public ArrayList positions;
	public int frameSkip;
	public bool active;
	int counter;

	// Use this for initialization
	void Start () {
		positions = new ArrayList();
		counter = 10;
	}
	
	// Update is called once per frame
	void Update () {
		if (active)
		{
			counter++;

			if (ball != null && !Physics.Raycast(ball.transform.position,Vector3.down,0.6f) && counter > 10)
			{
				positions.Add(ball.transform.position);
				drawLines();
				counter = 0;
			}
		}
	}

	void drawLines()
	{
		GetComponent<LineRenderer>().positionCount = positions.Count;

		GetComponent<LineRenderer>().SetPosition(GetComponent<LineRenderer>().positionCount-1,(Vector3)positions[positions.Count-1]);

		/*for (int i = 0; i<positions.Count; i++)
		{
			GetComponent<LineRenderer>().SetPosition(i,(Vector3)positions[i]);
		}*/
	}
}
