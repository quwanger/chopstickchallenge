using UnityEngine;
using System.Collections;

public class CC_Timer : MonoBehaviour {
	
	private float t;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		t = GameObject.Find("CC_Level").GetComponent<CC_TimeManager>().timeInMillis;
		this.GetComponent<GUIText>().text = GameObject.Find("CC_Level").GetComponent<CC_TimeManager>().GetCurrentTimeString();
		
		Color c;
		
		if(t < 60){
			this.GetComponent<GUIText>().material.color = Color.red;
		}
	}
}
