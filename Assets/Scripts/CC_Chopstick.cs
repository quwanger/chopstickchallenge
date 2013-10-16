using UnityEngine;
using System.Collections;

public class CC_Chopstick : CC_Behaviour {
	
	public float MagneticConstant = 1.0f;
	
	// Use this for initialization
	void Start () {
		level.RegisterChopstick(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
