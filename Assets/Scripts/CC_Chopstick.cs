using UnityEngine;
using System.Collections;

public class CC_Chopstick : CC_Pickup {
	
	public float MagneticConstant = 1.0f;
	
	// Use this for initialization
	void Start () {
		level.RegisterChopstick(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.GetComponent<CC_Chopstick>()){
			level.soundManager.playSound(CC_Level.SoundType.chopstickCollide);
		}else{
			//hits anything else
		}
	}
}
