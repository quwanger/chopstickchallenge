using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	/*
	private float alphaFadeValue = 0.0f;
	private float transitionTimeIn = 2.0f;
	public bool fadeOut = false;
	public Texture Overlay;
	
	void Awake() {
		AudioListener.volume = 0.0f;
	}
	
	IEnumerator doFade()
	{
		fadeOut = true;
		yield return new WaitForSeconds(2.0f);
		Application.LoadLevel("Main");
	}
	
	void OnGUI() {
		if(fadeOut){
			alphaFadeValue += Mathf.Clamp01(Time.deltaTime / transitionTimeIn);
			AudioListener.volume = (1.0f - alphaFadeValue);	
		}
		
		GUI.color = new Color(0, 0, 0, alphaFadeValue);
		GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height ), Overlay);
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Y)){
		 	StartCoroutine(doFade());
		}
	}*/
	
	private float alphaFadeValue = 1.0f;
	private float transitionTimeIn = 4.0f;
	public bool fadeOut = false;
	public Texture Overlay;
	public int index = 0;
	public GameObject chopstick;
	private Vector3 startPos;
	private Vector3 optionsPos;
	private Vector3 levelPos;
	private bool mmActive = true;
	
	// 0 = top 
	// 1 = middle
	// 2 = bottom
	
	void Awake() {
		AudioListener.volume = 0.0f;
		
		startPos = new Vector3(chopstick.transform.position.x, GameObject.Find("pos1").transform.position.y, chopstick.transform.position.z);
		levelPos = new Vector3(chopstick.transform.position.x, GameObject.Find("pos2").transform.position.y, chopstick.transform.position.z);
		optionsPos = new Vector3(chopstick.transform.position.x, GameObject.Find("pos3").transform.position.y, chopstick.transform.position.z);
		
		mainMenu();
	}
	
	IEnumerator doFade()
	{
		fadeOut = true;
		yield return new WaitForSeconds(4.0f);
	}
	
	void OnGUI() {
		if(fadeOut){
			alphaFadeValue -= Mathf.Clamp01(Time.deltaTime / transitionTimeIn);
			AudioListener.volume = (1.0f - alphaFadeValue);	
		}
		
		GUI.color = new Color(0, 0, 0, alphaFadeValue);
		GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height ), Overlay);
	}
	
	void mainMenu()
	{	
		GameObject.Find("level1").guiTexture.enabled = false;
		GameObject.Find("level2").guiTexture.enabled = false;
		GameObject.Find("level3").guiTexture.enabled = false;	
		
		GameObject.Find("options").guiText.enabled = true;
		GameObject.Find("start").guiText.enabled = true;
		GameObject.Find("level").guiText.enabled = true;
		GameObject.Find("Chopstick").renderer.enabled = true;
		
		index = 0;
		iTween.MoveTo(chopstick, iTween.Hash("position",startPos, "x", 0.0f, "onComplete", "TweenComplete", "onCompleteTarget", gameObject));
		
		mmActive = true;
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Backspace))
		{
			mainMenu();
			this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.back);
		}
		
		StartCoroutine(doFade());

		if(Input.GetKeyDown(KeyCode.W))
		{
			this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.hover);
			if(index == 1){
				index -= 1;
				iTween.MoveTo(chopstick, iTween.Hash("position",startPos, "time", 0.0f, "onComplete", "TweenComplete", "onCompleteTarget", gameObject));
			}
			
			else if(index == 2){
				index -= 1;
				iTween.MoveTo(chopstick, iTween.Hash("position",levelPos, "time", 0.0f, "onComplete", "TweenComplete", "onCompleteTarget", gameObject));	
			}	
		}

		if(Input.GetKeyDown(KeyCode.S))
		{
			this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.hover);
			if(index == 1){
				index += 1;
				iTween.MoveTo(chopstick, iTween.Hash("position",optionsPos, "time", 0.0f, "onComplete", "TweenComplete", "onCompleteTarget", gameObject));	
			}
			
			else if(index == 0){
				index += 1;
				iTween.MoveTo(chopstick, iTween.Hash("position",levelPos, "time", 0.0f, "onComplete", "TweenComplete", "onCompleteTarget", gameObject));
			}
		}
		
		if(index == 0)
		{
			if(Input.GetKeyDown(KeyCode.Return))
			{
				this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.accept);
				Application.LoadLevel("main");
			}
		}
		
		if(index == 1)
		{
			if(Input.GetKeyDown(KeyCode.Return))
			{
				this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.accept);
				levelSelect();
			}
		}
		
		if(index == 2)
		{
			if(Input.GetKeyDown(KeyCode.Return))
			{
				this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.accept);
				options();
			}
		}
		
		
		if(mmActive == false)
		{
			if(Input.GetKeyDown(KeyCode.Alpha1))
			{
				this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.accept);
				Application.LoadLevel("main");
			}
			
			if(Input.GetKeyDown(KeyCode.Alpha2))
			{
				this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.accept);
				Application.LoadLevel("Sushi_Restaurant");
			}
			
			if(Input.GetKeyDown(KeyCode.Alpha3))
			{
				this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.accept);
				Application.LoadLevel("Hospital");
			}
		}
	}
	
	void levelSelect()
	{
		mmActive = false;
		GameObject.Find("options").guiText.enabled = false;
		GameObject.Find("start").guiText.enabled = false;
		GameObject.Find("level").guiText.enabled = false;
		GameObject.Find("Chopstick").renderer.enabled = false;
		
		GameObject.Find("level1").guiTexture.enabled = true;
		GameObject.Find("level2").guiTexture.enabled = true;
		GameObject.Find("level3").guiTexture.enabled = true;
	}
	
	void options()
	{
	}
}



		