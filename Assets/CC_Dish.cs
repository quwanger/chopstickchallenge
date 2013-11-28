using UnityEngine;
using System.Collections;

public class CC_Dish : CC_Behaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collision){
		level.soundManager.playSound(CC_Level.SoundType.dish);	
	}
}
