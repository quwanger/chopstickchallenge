using UnityEngine;
using System.Collections.Generic;

public class CC_Level : MonoBehaviour {

	public List<CC_Sushi> sushi = new List<CC_Sushi>();
	public List<CC_Chopstick> chopsticks = new List<CC_Chopstick>();
	public CC_Mouth mouth;
	
	public AudioClip slot_machine;
	//public List<GameObject> _players = new List<GameObject>();

	public bool DebugMode = true;
	
	public int playerScore = 0;
	
	public int pointsToAdd = 0;
	
	public static CC_Level instance { get; private set;}
	
	void Awake() {
		instance = this;
	}
	
	void Start() {	
		//start
	}
	
	void Update() {
		//make sure it doesn't go over max score
		if(playerScore < 10000000000){
			if(pointsToAdd > 0){
				int f = pointsToAdd.ToString().Length;
				int g = 0;
				if(f < 3){
					g = 1;
				}else{
					g = (int)Mathf.Pow(10, (f-2));
				}
				
				playerScore += g;
				pointsToAdd -= g;
				if(pointsToAdd%(g*5) == 0)
					this.audio.PlayOneShot(slot_machine);
			}
		}
	}
	
	public CC_Level level {
		get {
			return instance;
		}
		set {
			ThrowSetException("level");
		}
	}
	
	private void ThrowSetException(string source) {
		Debug.LogError("Tried to set a read-only property: " + source);
	}

	// Register
	public void RegisterSushi(CC_Sushi obj) {
		//ONLY to be called by the CC_Sushi class
		sushi.Add(obj);
	}

	public void RegisterChopstick(CC_Chopstick obj) {
		//ONLY to be called by the CC_Chopstick class
		chopsticks.Add(obj);
	}

	// Array Converters
	public CC_Sushi[] sushiArray {
		get {
			return sushi.ToArray();
		}
		set {
			ThrowSetException("sushi");
		}
	}

	public CC_Chopstick[] chopstickArray {
		get {
			return chopsticks.ToArray();
		}
		set {
			ThrowSetException("chopstick");
		}
	}
	
	public void AddPoints(int pta){
		pointsToAdd += pta;
	}
}
