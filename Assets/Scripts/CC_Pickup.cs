using UnityEngine;
using System.Collections;

public class CC_Pickup : CC_Behaviour {

	private bool isHeld = false;
	public Vector3 fromHand;

	public float handDistance = 10.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool Pickup(Vector3 handPos){
		isHeld = true;
		fromHand = transform.position - handPos;

		return true;
	}

	public void Hold(Vector3 handPos){

	}
}
