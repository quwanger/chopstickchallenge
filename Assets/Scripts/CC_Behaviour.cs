using UnityEngine;
using System.Collections;

public class CC_Behaviour : MonoBehaviour {
	
	public enum SushiType {Sushi1, Sushi2, Sushi3, Sushi4, Sushi5, Sushi6, Sushi7, Sushi8};
	
	public GameObject[] sushi;
	public GameObject[] chopsticks;
	public GameObject[] players;
	
	public CC_Level level {
		 get { 
			if(CC_Level.instance != null) {
				return CC_Level.instance; 
			}  else {
				Debug.LogError("FATAL: You need to have a CC_Level object in the scene to use level.");
				return new CC_Level();
			}
		}
	}
	
}
