using UnityEngine;
using System.Collections;

public class CC_ArmController : MonoBehaviour {
	public GameObject arm;
	void Awake() {
		
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//if(Input.GetKeyDown("joystick button 0")){
		//if(Input.GetAxis("Left Trigger") == -1){
			//arm.transform.position.z += 0.01f;
		//	}
		//float temp = this.transform.position.z;
		
		if(Input.GetAxis("TriggersL_1") == 1){
 			this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (transform.position.z+0.05f));
			Debug.Log("hey");
		}
		
		if(Input.GetAxis("TriggersR_1") == 1){
 			this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (transform.position.z-0.05f));
			Debug.Log("hello");
		}
		
		// right joystick right
		if(Input.GetAxis("R_XAxis_1") == 1){
			float rot = this.transform.rotation.x;
			rot += 0.5f;
			this.transform.Rotate(rot,0,0);
			Debug.Log("rightRot");	
		}
		
		//right joystick left
		if(Input.GetAxis("R_XAxis_1") == -1){
			float rot = this.transform.rotation.z;
			rot -= 0.5f;
			this.transform.Rotate(rot,0,0);
			Debug.Log("leftRot");
		}
		
		// left joystick arm right
		if(Input.GetAxis("L_XAxis_1") == 1){
			this.transform.position = new Vector3((this.transform.position.x+0.05f), this.transform.position.y, transform.position.z);
			Debug.Log("right");	
		}
		
		//left joystick arm left
		if(Input.GetAxis("L_XAxis_1") == -1){
			this.transform.position = new Vector3((this.transform.position.x-0.05f), this.transform.position.y, transform.position.z);
			Debug.Log("left");
		}
		
		// left joystick arm up
		if(Input.GetAxis("L_YAxis_1") == 1){
			this.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y-0.05f), transform.position.z);
			Debug.Log("down");	
		}
		
		//left joystick arm down
		if(Input.GetAxis("L_YAxis_1") == -1){
			this.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y+0.05f), transform.position.z);
			Debug.Log("up");
		}
	}
}
