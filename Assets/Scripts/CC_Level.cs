using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CC_Level : MonoBehaviour {

	public List<CC_Sushi> sushi = new List<CC_Sushi>();
	public List<CC_Chopstick> chopsticks = new List<CC_Chopstick>();
	public CC_Mouth mouth;
	public Camera mainCamera;
	public string nextLevel;
	
	public GameObject[] cutouts;
	public bool isBeingMotivated = false;
		
	public GUIText results;
	public GUIText finishMenu;
	public GameObject goWin;
	public GameObject goLose;
	public bool gameOver = false;
	public bool isWinner = false;
	
	public bool searchingForWiimotes = false;
	
	private float alphaFadeValue = 0.0f;
    private float transitionTimeIn = 2.0f;
    public bool fadeOut = false;
	//public bool fadeIn = true;
    public Texture Overlay;
	
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
		AudioListener.volume = 1.0f;
		instance = this;
		soundManager = this.GetComponent<CC_SoundManager>();
	}
	
	void Start() {	
		//start
		soundManager.playSound(SoundType.levelStart);
		
		foreach(CC_Sushi s in sushi){
			availablePoints += s.GetComponent<CC_Sushi>().PointValue;
		}
		
		//Debug.Log(availablePoints);
		
		this.GetComponent<CC_TimeManager>().StartTimer();
	}
	
	void Update() {
		//make sure it doesn't go over max score
		if(playerScore < 10000000000){
			if(pointsToAdd > 0){
				
				//if you are getting points, there is a 1% chance you will be motivated
				float r = Random.Range(0.0f, 100.0f);
				if(r<0.2f && !isBeingMotivated)
					StartCoroutine(CardboardCutout());
				
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
			isWinner = true;
			pointsToAdd = 0;
			results.text = "YOU ARE WINNER!";
			finishMenu.text = "1 - Next Level      2 - Main Menu";
			Vector3 tempV = new Vector3(0, 0, -3.0f);
			Vector3 tempW = tempV + mainCamera.transform.position;
			Instantiate(goWin, tempW, Quaternion.identity);
			soundManager.playSound(SoundType.win);
			this.GetComponent<CC_TimeManager>().StopTimer();
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
			this.GetComponent<CC_TimeManager>().StopTimer();
		}
		
		if(gameOver){
			if(isWinner){
				if(Input.GetKeyDown(KeyCode.Alpha1)){
					soundManager.playSound(SoundType.accept);
					StartCoroutine(doFade(nextLevel));
				}
			}else{
				if(Input.GetKeyDown(KeyCode.Alpha1)){
					soundManager.playSound(SoundType.accept);
					StartCoroutine(doFade(Application.loadedLevel.ToString()));
				}
			}
			if(Input.GetKeyDown(KeyCode.Alpha2)){
				soundManager.playSound(SoundType.back);
				StartCoroutine(doFade("Menu"));
			}
		}
		
		if(searchingForWiimotes){
			if(!Wii.IsSearching()){
				searchingForWiimotes=false;
				Debug.Log("Wiimote Search Over");
			}	
		}
		
		if(DebugMode){
			if(Input.GetKeyDown(KeyCode.Alpha6)){
				StartCoroutine(doFade("Main"));
			}else if(Input.GetKeyDown(KeyCode.Alpha7)){
				StartCoroutine(doFade("Sushi_Restaurant"));
			}else if(Input.GetKeyDown(KeyCode.Alpha8)){
				StartCoroutine(doFade("Hospital"));
			}else if(Input.GetKeyDown(KeyCode.Alpha9)){
				//StartCoroutine(doFade("Heaven"));
			}else if(Input.GetKeyDown(KeyCode.P)){
				pointsToAdd += 10000;
			}else if(Input.GetKeyDown(KeyCode.C)){
				StartCoroutine(CardboardCutout());
			}
		}
		
		if(Input.GetKeyDown(KeyCode.BackQuote)){
			Debug.Log("Begin Wiimote Calibration");
			Wii.StartSearch();
			searchingForWiimotes = true;
		}
	}
	
	IEnumerator doFade(string level)
    {
                fadeOut = true;
                yield return new WaitForSeconds(2.0f);
                Application.LoadLevel(level);
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
	
	public IEnumerator CardboardCutout(){
		isBeingMotivated = true;
		GameObject c = cutouts[Random.Range(0, cutouts.Length)];
		soundManager.playSound(SoundType.motivational);
		iTween.MoveTo(c, iTween.Hash("x", c.GetComponent<CC_Cardboard>().targetX, "time", 1.0f));
		yield return new WaitForSeconds(1.2f);
		CardboardReturn(c);
	}
	
	public void CardboardReturn(GameObject c){
		iTween.MoveTo(c, iTween.Hash("x", c.GetComponent<CC_Cardboard>().initialX, "time", 1.0f));
		isBeingMotivated = false;
	}
	
	public void AddPoints(int pta){
		pointsToAdd += pta;
	}
	
	public void OnGUI() {
        if(fadeOut){
        	alphaFadeValue += Mathf.Clamp01(Time.deltaTime / transitionTimeIn);
        	AudioListener.volume = (1.0f - alphaFadeValue);
        }/*else if(fadeIn){
			alphaFadeValue -= Mathf.Clamp01(Time.deltaTime / transitionTimeIn);
        	AudioListener.volume = (1.0f - alphaFadeValue);
		}*/
               
        GUI.color = new Color(0, 0, 0, alphaFadeValue);
        GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height ), Overlay);
    }
}
