using UnityEngine;
using System.Collections;

public class CC_Score : MonoBehaviour {
	
	private int score;
	
	// Use this for initialization
	void Start () {
		score = GameObject.Find("CC_Level").GetComponent<CC_Level>().playerScore;
		
		//Debug.Log(score.ToString().Length);
	}
	
	// Update is called once per frame
	void Update () {
		
		score = GameObject.Find("CC_Level").GetComponent<CC_Level>().playerScore;
		
		this.GetComponent<GUIText>().text = "Score: ";
		for(int i = 0; i < (10 - score.ToString().Length); i++){
			this.GetComponent<GUIText>().text += "0";
		}
		this.GetComponent<GUIText>().text += score.ToString();
	}
}
