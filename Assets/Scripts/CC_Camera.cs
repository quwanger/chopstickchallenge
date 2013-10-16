using UnityEngine;
using System.Collections;

public class CC_Camera : CC_Behaviour {

	private Camera camera;
	private Vector3 target;

	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update() {
		target = mouth.transform.position;
		target = Vector3.Lerp(target, chopsticks[0].transform.position, 0.5f);

		transform.LookAt(target);
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, target);
	}
}
