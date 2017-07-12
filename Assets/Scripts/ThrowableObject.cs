using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base Class for Objects which can be thrown, like Heads, Boxes etc
public class ThrowableObject : MonoBehaviour {
	public bool groundTouching = false;
	public bool attachedToBody;
	public GameObject testBall;
	public GameObject predictionLine;

	public float throwPower;
	public float maxThrowPower, minThrowPower;

	private Vector3 tempPosition;
	private Quaternion tempRotation;

	private GameObject newTestBall;
	private ThrowBar scrollbarScript; 

	public bool ThrowMode;
	
	// Use this for initialization
	void Start () {
		throwPower = 400f;
		tempPosition = transform.position;
		tempRotation = transform.rotation;
		
		scrollbarScript = GameObject.Find("Scrollbar").GetComponent<ThrowBar>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!attachedToBody){
			GetComponent<MeshCollider>().enabled = true;
			GetComponent<Rigidbody>().isKinematic = false;

			if (!predictionLine.GetComponent<PredictionLine>().active)
			{
				predictionLine.GetComponent<LineRenderer>().positionCount = 0;
				predictionLine.GetComponent<PredictionLine>().positions = new ArrayList();
				predictionLine.GetComponent<PredictionLine>().ball = null;
			}
			ThrowMode = false;
		} else {
			GetComponent<Rigidbody>().isKinematic = true;
			GetComponent<MeshCollider>().enabled = false;
			
			float tempThrowPower = throwPower;
			if (ThrowMode)
			{
				if(Input.GetAxis("Distance")>0 && throwPower < maxThrowPower)
				{
					throwPower += 1f;
				}

				if(Input.GetAxis("Distance")<0 && throwPower > minThrowPower)
				{
					throwPower -= 1f;
				}

				if (tempThrowPower != throwPower || transform.position != tempPosition || transform.rotation != tempRotation)
				{
					ResetThrowPredict();
				}

				if (Input.GetButtonDown("Throw"))
				{
					if(gameObject.name == "Head"){
						transform.parent.parent.gameObject.GetComponent<Body>().headless = true;
					}
					transform.parent.parent.gameObject.GetComponent<Body>().gotAnObject = false;
					transform.parent.parent.BroadcastMessage("ReceiveThrow");
					transform.parent = null;
					GetComponent<Rigidbody>().isKinematic = false;
					attachedToBody = false;
					ThrowMode = false;
					GetThrown(new Vector3(transform.TransformDirection(Vector3.forward).x*throwPower,1.7f*throwPower,transform.TransformDirection(Vector3.forward).z*throwPower));
				}
			} else {

			}
			
			scrollbarScript.ChangeScrollbar(gameObject);
			
			if (Input.GetButtonDown("Throw"))
			{
				if (ThrowMode == false && attachedToBody)
				{
					ThrowMode = true;
					ThrowPredict();
				}
			}
			
			if (Input.GetKeyDown(KeyCode.Q))
			{
				if(gameObject.name == "Head"){
					transform.parent.parent.gameObject.GetComponent<Body>().headless = true;
				}
				transform.parent.parent.gameObject.GetComponent<Body>().gotAnObject = false;
				transform.parent = null;
				GetComponent<Rigidbody>().isKinematic = false;
				attachedToBody = false;
				ThrowMode = false;
				GetComponent<Rigidbody>().AddForce(new Vector3(transform.TransformDirection(Vector3.forward).x,1.7f,transform.TransformDirection(Vector3.forward).z));
			}
				
		}

		tempPosition = transform.position;
		tempRotation = transform.rotation;
	}
	
			
	void ThrowPredict()
	{
		Debug.Log("Throwpredict"+gameObject.name);
		newTestBall = Instantiate(testBall);
		newTestBall.transform.position = transform.position;
		newTestBall.transform.rotation = transform.rotation;
		newTestBall.GetComponent<Rigidbody>().isKinematic = false;

		newTestBall.GetComponent<Rigidbody>().AddForce(new Vector3(transform.TransformDirection(Vector3.forward).x*throwPower,1.7f*throwPower,transform.TransformDirection(Vector3.forward).z*throwPower));
		predictionLine.GetComponent<PredictionLine>().ball = newTestBall;

		predictionLine.GetComponent<PredictionLine>().active = true;

	}

	void ResetThrowPredict()
	{
		if (newTestBall != null)
		{
			Destroy(newTestBall);
			newTestBall = null;
			predictionLine.GetComponent<LineRenderer>().positionCount = 0;
			predictionLine.GetComponent<PredictionLine>().positions = new ArrayList();
			predictionLine.GetComponent<PredictionLine>().ball = null;
			ThrowPredict();
		}
	}

	void GetThrown(Vector3 v)
	{
		ThrowMode = false;
		newTestBall = null;
		predictionLine.GetComponent<LineRenderer>().positionCount = 0;
		predictionLine.GetComponent<PredictionLine>().positions = new ArrayList();
		predictionLine.GetComponent<PredictionLine>().ball = null;
		predictionLine.GetComponent<PredictionLine>().active = false;
		Debug.Log("ThrowStrength" + v);
		GetComponent<Rigidbody>().AddForce(v);
	}
}
