using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBehaviour : MonoBehaviour {
	public bool groundTouching = false;
	public bool isFocusedOnBody = false;
	
	private float headRotation;
	private float rotationSpeed = 180;
	private Vector3 rayForFocusingOnBody;
	public bool attachedToBody;
	public GameObject testBall;
	public GameObject predictionLine;

	private float throwPower;
	public float maxThrowPower, minThrowPower;

	private Vector3 tempPosition;
	private Quaternion tempRotation;

	private GameObject newTestBall;

	private bool ThrowMode;
	
	// Use this for initialization
	void Start () {
		throwPower = 400f;
		tempPosition = transform.position;
		tempRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {

		if (!attachedToBody)
		{

			GetComponent<SphereCollider>().enabled = true;
			GetComponent<Rigidbody>().isKinematic = false;
			headRotation = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
			
			if(groundTouching){
				transform.Rotate(Vector3.up * headRotation);
				if(Input.GetKey(KeyCode.F)){
					FocusOnBody();
				}
			}
		} else {
			GetComponent<Rigidbody>().isKinematic = true;
			GetComponent<SphereCollider>().enabled = false;

			float tempThrowPower = throwPower;



			if (ThrowMode)
			{
				if(Input.GetAxis("Distance")>0 && throwPower < maxThrowPower)
				{
					throwPower += 0.2f;
				}

				if(Input.GetAxis("Distance")<0 && throwPower > minThrowPower)
				{
					throwPower -= 0.2f;
				}

				if (tempThrowPower != throwPower || transform.position != tempPosition || transform.rotation != tempRotation)
				{
					ResetThrowPredict();
				}

				if (Input.GetButtonDown("Throw"))
				{
					transform.parent.parent.gameObject.GetComponent<Body>().headless = true;
					transform.parent = null;
					GetComponent<Rigidbody>().isKinematic = false;
					attachedToBody = false;
					GetComponent<Rigidbody>().AddForce(new Vector3(transform.TransformDirection(Vector3.forward).x*throwPower,1.7f*throwPower,transform.TransformDirection(Vector3.forward).z*throwPower));
				}
			}

			if (Input.GetButtonDown("Throw"))
			{
				if (ThrowMode == false)
				{
					ThrowMode = true;
					ThrowPredict();
				}
			}
		}

		tempPosition = transform.position;
		tempRotation = transform.rotation;
	}
	
	void OnCollisionStay(Collision collisionInfo){
		if(collisionInfo.gameObject.tag == "Ground"){
			groundTouching = true;
		}
	}
	
	void FocusOnBody(){
		rayForFocusingOnBody = transform.TransformDirection(Vector3.forward);
		RaycastHit hit;
		
        Debug.DrawRay(transform.position, rayForFocusingOnBody * 15, Color.green);

        if (Physics.Raycast(transform.position, rayForFocusingOnBody, out hit, 100)){
			if(hit.transform.tag == "Body"){
				Debug.Log("Körper entdeckt");
				isFocusedOnBody = true;
				hit.transform.gameObject.GetComponent<Body>().isUsed = true;
			}
		}
	}
		
	void ThrowPredict()
	{
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
}
