using UnityEngine;
using System.Collections;

public class CC_Mouth : CC_Behaviour {

	private Vector3 origPosition; // Prototype code
	public GameObject celebrate;
	
	public AudioClip[] sounds;

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
			//Debug.Log(other.gameObject);
			Instantiate(celebrate, other.transform.position, other.transform.rotation);
			//this.GetComponent<AudioSource>().audio.clip = sounds[Random.Range(0, sounds.Length)];
			//this.GetComponent<AudioSource>().PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
		}
	}
}
