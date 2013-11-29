using UnityEngine;
using System.Collections.Generic;

public class CC_Level : MonoBehaviour {

	public List<CC_Sushi> sushi = new List<CC_Sushi>();
	public List<CC_Chopstick> chopsticks = new List<CC_Chopstick>();
	public CC_Mouth mouth;
	public Camera mainCamera;
	
	public GUIText results;
	public GUIText finishMenu;
	public GameObject goWin;
	public GameObject goLose;
	public bool gameOver = false;
	
	public enum SoundType {accept, back, chopstickClick, chopstickCollide, demotivational, dish, eating, fireworksExplosions, hover, intro, levelStart, lose, motivational, scoreIncrement, sushiCollide, sushiDrop, win};
	
	public CC_SoundManager soundManager;
	
	//public AudioClip slot_machine;
	//public List<GameObject> _players = new List<GameObject>();

	public bool DebugMode = true;
	
	public long playerScore = 0;
	public int pointsToAdd = 0;
	
	//points needed to beat the level
	public int pointsToWin = 1000000;
	//time the player has to beat the level
	public int levelTime = 180;
	//available points
	public int availablePoints;
	
	public static CC_Level instance { get; private set;}
	
	void Awake() {
		instance = this;
		soundManager = this.GetComponent<CC_SoundManager>();
	}
	
	void Start() {	
		//start
		soundManager.playSound(SoundType.levelStart);
		
		foreach(CC_Sushi s in sushi){
			availablePoints += s.GetComponent<CC_Sushi>().PointValue;
		}
		
		Debug.Log(availablePoints);
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
				if(pointsToAdd%(g*10) == 0)
					soundManager.playSound(SoundType.scoreIncrement);
			}
		}
		
		if(playerScore >= pointsToWin && gameOver == false){
			//player wins
			gameOver = true;
			pointsToAdd = 0;
			results.text = "YOU ARE WINNER!";
			finishMenu.text = "1 - Next Level      2 - Main Menu";
			Vector3 tempV = new Vector3(0, 0, -3.0f);
			Vector3 tempW = tempV + mainCamera.transform.position;
			Instantiate(goWin, tempW, Quaternion.identity);
			soundManager.playSound(SoundType.win);

			//	stop controls
			//	stop all other sounds
		}else if((levelTime <= 0 && gameOver == false) || (availablePoints < pointsToWin && gameOver == false)){
			//player loses
			gameOver = true;
			pointsToAdd = 0;
			results.text = "YOU LOSE.";
			finishMenu.text = "1 - Replay        2 - Main Menu";
			Vector3 tempV = new Vector3(0, 0, -3.0f);
			Vector3 tempW = tempV + mainCamera.transform.position;
			Instantiate(goLose, tempW, Quaternion.identity);
			soundManager.playSound(SoundType.lose);
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
