using UnityEngine;
using System.Collections.Generic;

public class CC_Level : MonoBehaviour {

	public List<CC_Sushi> sushi = new List<CC_Sushi>();
	public List<CC_Chopstick> chopsticks = new List<CC_Chopstick>();
	public CC_Mouth mouth;
	//public List<GameObject> _players = new List<GameObject>();

	public bool DebugMode = true;
	
	public static CC_Level instance { get; private set;}
	
	void Awake() {
		instance = this;
	}
	
	void Start() {	
		//start
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
}
