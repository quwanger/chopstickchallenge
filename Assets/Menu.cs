using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	private float alphaFadeValue = 1.0f;
	private float transitionTimeIn = 4.0f;
	public bool fadeOut = false;
	public Texture Overlay;
	public int index = 0;
	public int optionsIndex = 0;
	public int inputIndex = 0;
	
	public GameObject start;
	public GameObject level;
	public GameObject option;
	
	public GameObject inputWiiText;
	public GameObject inputXboxText;
	public GameObject inputKeyboardText;
	private string levelSelected = "main";
	private Color gold;
	private bool mmActive = true;
	private bool optionsActive = false;
	private bool levelsActive = false;
	private bool inputActive = false;
	
	// 0 = top 
	// 1 = middle
	// 2 = bottom
	
	void Awake() {
		AudioListener.volume = 0.0f;
		gold = new Color(255,246,0);
	}
	
	void Start(){
		this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.intro);
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
	
	
	void Update () {
		StartCoroutine(doFade());
		Debug.Log(Screen.width);	
			
		if(inputActive)
		{	
			if(Input.GetKeyDown(KeyCode.K))
			{
				this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.hover);
				if(inputIndex == 1){
					inputIndex -= 1;
				}
				
				else if(inputIndex == 2){
					inputIndex -= 1;
				}	
			}
	
			if(Input.GetKeyDown(KeyCode.L))
			{	
				this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.hover);
				if(inputIndex == 1){
					inputIndex += 1;	
				}
				
				else if(inputIndex == 0){
					inputIndex += 1;
				}
			}
			
			if(Input.GetKeyDown(KeyCode.Return))
			{
				this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.accept);
				Application.LoadLevel(levelSelected);				
			}
			
			textHighlighting(inputIndex);
			instructionImages(inputIndex);
		}
		
		if(mmActive)
		{
			if(Input.GetKeyDown(KeyCode.W))
			{
				this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.hover);
				if(index == 1){
					index -= 1;
				}
				
				else if(index == 2){
					index -= 1;
				}	
			}
	
			if(Input.GetKeyDown(KeyCode.S))
			{
				this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.hover);
				if(index == 1){
					index += 1;
				}
				
				else if(index == 0){
					index += 1;
				}
			}
		
			if(index == 0)
			{
				start.guiText.material.SetColor("_Color", gold);
				level.guiText.material.SetColor("_Color", Color.white);
				option.guiText.material.SetColor("_Color", Color.white);
				
				if(Input.GetKeyDown(KeyCode.Return))
				{
					this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.accept);
					inputSelection();
				}
			}
			
			if(index == 1)
			{
				start.guiText.material.SetColor("_Color", Color.white);
				level.guiText.material.SetColor("_Color", gold);
				option.guiText.material.SetColor("_Color", Color.white);

				if(Input.GetKeyDown(KeyCode.Return))
				{
					this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.accept);
					levelSelect();
				}
			}
			
			if(index == 2)
			{
				start.guiText.material.SetColor("_Color", Color.white);
				level.guiText.material.SetColor("_Color", Color.white);
				option.guiText.material.SetColor("_Color", gold);

				if(Input.GetKeyDown(KeyCode.Return))
				{
					this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.accept);
					options();
				}
			}
		}
		
		if(mmActive == false)
		{			
			if(Input.GetKeyDown(KeyCode.Backspace))
			{
				mainMenu();
				this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.back);
			}
		}
			
		if(levelsActive == true)
		{
			if(Input.GetKeyDown(KeyCode.Alpha1))
			{
				this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.accept);
				levelSelected = "main";
				inputSelection();
			}
			
			if(Input.GetKeyDown(KeyCode.Alpha2))
			{
				this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.accept);
				levelSelected = "Sushi_Restaurant";
				inputSelection();
			}
			
			if(Input.GetKeyDown(KeyCode.Alpha3))
			{
				this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.accept);
				levelSelected = "Hospital";
				inputSelection();
			}
		}
		
		// options
		if(optionsActive == true)
		{
			if(Input.GetKeyDown(KeyCode.K))
			{
				this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.hover);
				if(optionsIndex == 1){
					optionsIndex -= 1;
				}
				
				else if(optionsIndex == 2){
					optionsIndex -= 1;
				}	
			}
	
			if(Input.GetKeyDown(KeyCode.L))
			{	
				this.gameObject.GetComponent<CC_SoundManager>().playSound(CC_Level.SoundType.hover);
				if(optionsIndex == 1){
					optionsIndex += 1;	
				}
				
				else if(optionsIndex == 0){
					optionsIndex += 1;
				}
			}
			
			textHighlighting(optionsIndex);
			instructionImages(optionsIndex);
		}
	}
	
	void textHighlighting(int tempIndex)
	{
		// input menu
		if(tempIndex==0)
		{
			inputKeyboardText.guiText.material.SetColor("_Color", gold);
			inputXboxText.guiText.material.SetColor("_Color", Color.white);
			inputWiiText.guiText.material.SetColor("_Color", Color.white);
		}
		
		if(tempIndex==1)
		{
			inputKeyboardText.guiText.material.SetColor("_Color", Color.white);
			inputXboxText.guiText.material.SetColor("_Color", gold);
			inputWiiText.guiText.material.SetColor("_Color", Color.white);
		}
			
		if(tempIndex==2)
		{
			inputKeyboardText.guiText.material.SetColor("_Color", Color.white);
			inputXboxText.guiText.material.SetColor("_Color", Color.white);
			inputWiiText.guiText.material.SetColor("_Color", gold);
		}
	}
	
	void instructionImages(int tempIndex)
	{
		if(tempIndex==0)
		{
			GameObject.Find("wiiImage").guiTexture.enabled = true;
			GameObject.Find("xboxImage").guiTexture.enabled = false;
			GameObject.Find("keyboardImage").guiTexture.enabled = false;
		}
		
		if(tempIndex==1)
		{
			GameObject.Find("wiiImage").guiTexture.enabled = false;
			GameObject.Find("xboxImage").guiTexture.enabled = true;
			GameObject.Find("keyboardImage").guiTexture.enabled = false;
		}
		
		if(tempIndex==2)
		{
			GameObject.Find("wiiImage").guiTexture.enabled = false;
			GameObject.Find("xboxImage").guiTexture.enabled = false;
			GameObject.Find("keyboardImage").guiTexture.enabled = true;
		}
	}
	
	void mainMenu()
	{	
		optionsActive = false;
		mmActive = true;
		inputActive = false;
		levelsActive = false;
		
		GameObject.Find("level1").guiTexture.enabled = false;
		GameObject.Find("level2").guiTexture.enabled = false;
		GameObject.Find("level3").guiTexture.enabled = false;	
		GameObject.Find("level4").guiTexture.enabled = false;
		
		GameObject.Find("options").guiText.enabled = true;
		GameObject.Find("start").guiText.enabled = true;
		GameObject.Find("level").guiText.enabled = true;

		GameObject.Find("wiiImage").guiTexture.enabled = false;
		GameObject.Find("xboxImage").guiTexture.enabled = false;
		GameObject.Find("keyboardImage").guiTexture.enabled = false;
		
		GameObject.Find("inputKeyboard").guiText.enabled = false;
		GameObject.Find("inputWii").guiText.enabled = false;
		GameObject.Find("inputXbox").guiText.enabled = false;
		
		GameObject.Find("logo").guiTexture.enabled = true;
		
		index = 0;		
		mmActive = true;
	}
	
	void levelSelect()
	{
		optionsActive = false;
		mmActive = false;
		inputActive = false;
		levelsActive = true;
		
		GameObject.Find("options").guiText.enabled = false;
		GameObject.Find("start").guiText.enabled = false;
		GameObject.Find("level").guiText.enabled = false;
		
		GameObject.Find("level1").guiTexture.enabled = true;
		GameObject.Find("level2").guiTexture.enabled = true;
		GameObject.Find("level3").guiTexture.enabled = true;
		GameObject.Find("level4").guiTexture.enabled = true;
		
		GameObject.Find("wiiImage").guiTexture.enabled = false;
		GameObject.Find("xboxImage").guiTexture.enabled = false;
		GameObject.Find("keyboardImage").guiTexture.enabled = false;

		GameObject.Find("logo").guiTexture.enabled = false;
	}
	
	void options()
	{
		optionsActive = true;
		mmActive = false;
		inputActive = false;
		levelsActive = false;
		
		optionsIndex = 0;
		GameObject.Find("options").guiText.enabled = false;
		GameObject.Find("start").guiText.enabled = false;
		GameObject.Find("level").guiText.enabled = false;
		
		GameObject.Find("level1").guiTexture.enabled = false;
		GameObject.Find("level2").guiTexture.enabled = false;
		GameObject.Find("level3").guiTexture.enabled = false;
		GameObject.Find("level4").guiTexture.enabled = false;
		
		GameObject.Find("inputKeyboard").guiText.enabled = true;
		GameObject.Find("inputWii").guiText.enabled = true;
		GameObject.Find("inputXbox").guiText.enabled = true;
		
		GameObject.Find("logo").guiTexture.enabled = false;
	}
	
	void inputSelection()
	{
		optionsActive = false;
		mmActive = false;
		inputActive = true;
		levelsActive = false;
		
		inputIndex = 0;
		GameObject.Find("options").guiText.enabled = false;
		GameObject.Find("start").guiText.enabled = false;
		GameObject.Find("level").guiText.enabled = false;
		
		GameObject.Find("level1").guiTexture.enabled = false;
		GameObject.Find("level2").guiTexture.enabled = false;
		GameObject.Find("level3").guiTexture.enabled = false;
		GameObject.Find("level4").guiTexture.enabled = false;

		GameObject.Find("inputKeyboard").guiText.enabled = true;
		GameObject.Find("inputWii").guiText.enabled = true;
		GameObject.Find("inputXbox").guiText.enabled = true;
		
		GameObject.Find("logo").guiTexture.enabled = false;
	}
}