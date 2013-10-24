using UnityEngine;
using System.Collections;

public class CC_ArmController : MonoBehaviour {
	public GameObject arm;
	public float speed;
	
	void Awake() {
		
	}

	void Start () {
		speed = 0.5f;
	}
	
	void Update () {
		//Left joystick, movement in x-axis 
		this.transform.position = this.transform.position + new Vector3(Input.GetAxis("HorizontalL") * speed, Input.GetAxis("VerticalL") * speed, 0);
		
		//Right Joystick, rotation (right)
		if(Input.GetAxis("HorizontalR") == 1){
			float rot = this.transform.rotation.x;
			rot += 2.0f;
			this.transform.Rotate(rot,0,0);
		}
		
		//Right Joystick, rotation (left)
		if(Input.GetAxis("HorizontalR") == -1){
			float rot = this.transform.rotation.z;
			rot -= 2.0f;
			this.transform.Rotate(rot,0,0);
		}
		
		//Right Trigger, forward
		if(Input.GetAxis("TriggerR") == 1){
 			this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (transform.position.z-0.5f));
		}
		
		//Left Trigger, reverse
		if(Input.GetAxis("TriggerL") == 1){
 			this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (transform.position.z+0.5f));
		}
	}
}



