using UnityEngine;
using System.Collections.Generic;

public class CC_Level : MonoBehaviour {

	public List<GameObject> _sushi = new List<GameObject>();
	//public List<GameObject> _chopsticks = new List<GameObject>();
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
	
	public GameObject[] sushi {
		get {
			return _sushi.ToArray();
		}
		set {
			ThrowSetException("sushi");
		}
	}
	
	private void ThrowSetException(string source) {
		Debug.LogError("Tried to set a read-only property: " + source);
	}
	
	public void RegisterSushi(GameObject go) {
		//ONLY to be called by the TTSRacer class
		_sushi.Add(go);
	}
}
