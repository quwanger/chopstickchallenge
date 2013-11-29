using UnityEngine;
using System.Collections;

public class CC_TimeManager : MonoBehaviour {
	
	
	public float timeInMillis = 0;
	private float startTime; //in seconds
	private CC_Level level;
	
	private bool running = false;
	
	void Start () {
	}
	
	void Update () {
		
		if(running)
			timeInMillis = startTime - Time.time;
	}
	
	public string GetCurrentTimeString(){
		int d = (int)(timeInMillis * 100.0f);
    	int minutes = d / (60 * 100);
    	int seconds = (d % (60 * 100)) / 100;
    	int hundredths = (d % 100);
    	return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);
	}
	
	public void StartTimer() {
		running = true;
		level = GameObject.Find("CC_Level").GetComponent<CC_Level>();
		startTime = Time.time + level.GetComponent<CC_Level>().levelTime;
	}
	
	public void StopTimer() {
		running = false;
	}
}
