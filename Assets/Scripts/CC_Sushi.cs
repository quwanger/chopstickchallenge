using UnityEngine;
using System.Collections;

public class CC_Sushi : CC_Behaviour {
	
	public int PointValue = 10000;
	public float GripConstant = 1.0f;
	
	void Awake () {
		level.RegisterSushi(this);
	}

	void Update() {
	}
	
}