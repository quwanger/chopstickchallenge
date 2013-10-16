using UnityEngine;
using System.Collections;

public class CC_Mouth : CC_Behaviour {

	private Vector3 origPosition; // Prototype code

	// Use this for initialization
	void Start () {
		level.mouth = this;
		origPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = origPosition + new Vector3(Mathf.Sin(Time.time*2), Mathf.Sin(Time.time*2 + 1), Mathf.Sin(Time.time + 2));
	}
}
