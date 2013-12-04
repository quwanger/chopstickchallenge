using UnityEngine;
using System.Collections;

public class CC_Cardboard : MonoBehaviour {
	
	public float initialX;
	public float initialY;
	public float targetX;
	
	// Use this for initialization
	void Start () {
		initialX = this.transform.position.x;
		initialY = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
