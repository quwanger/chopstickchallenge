using UnityEngine;
using System.Collections;

public class CC_Mouth : CC_Behaviour {

	private Vector3 origPosition; // Prototype code
	public GameObject celebrate;
	public ParticleSystem sushiSplosion;

	// Use this for initialization
	void Start () {
		level.mouth = this;
		origPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = origPosition + new Vector3(Mathf.Sin(Time.time*2), Mathf.Sin(Time.time*2 + 1), Mathf.Sin(Time.time + 2));
	}
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.GetComponent<CC_Sushi>()){
			//creates fireworks on feeding success
			Instantiate(celebrate, other.transform.position, other.transform.rotation);
			Instantiate(sushiSplosion, other.transform.position, other.transform.rotation);
			
			//plays random success sound
			level.soundManager.playSound(CC_Level.SoundType.fireworksExplosions);
			level.soundManager.playSound(CC_Level.SoundType.win);
			level.soundManager.playSound(CC_Level.SoundType.eating);
			
			//adds the points to the point counter
			level.pointsToAdd += other.GetComponent<CC_Sushi>().PointValue;
			
			//removes the sushi
			DestroyObject(other.gameObject);
		}
	}
}
