using UnityEngine;
using System.Collections;

public class CC_Sushi : CC_Behaviour { //CC_Pickup{
	
	public int PointValue = 10000;
	public float GripConstant = 1.0f;
	
	private bool hitTheFloor = false;
	
	void Awake () {
		level.RegisterSushi(this);
	}

	void Update() {
	}
	
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.GetComponent<CC_Sushi>()){
			//sushi hit other sushi
			level.soundManager.playSound(CC_Level.SoundType.sushiDrop);
		}else if(collision.gameObject.GetComponent<CC_Chopstick>()){
			level.soundManager.playSound(CC_Level.SoundType.sushiCollide);
		}else if((collision.gameObject.name == "ground" || collision.gameObject.name == "floor")&& !hitTheFloor){
			level.soundManager.playSound(CC_Level.SoundType.demotivational);
			level.availablePoints -= this.PointValue;
			Debug.Log(level.availablePoints);
			hitTheFloor = true;
		}else{
			level.soundManager.playSound(CC_Level.SoundType.sushiDrop);
		}
	}
	
	void OnCollsionExit(Collision collision){
		if(collision.gameObject.GetComponent<CC_Chopstick>()){
			//sushi leaves the chopstick
			level.soundManager.playSound(CC_Level.SoundType.demotivational);
		}
	}
}