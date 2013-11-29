using UnityEngine;
using System.Collections;

public class CC_ArmController : MonoBehaviour {
	public GameObject arm;
	public float speed;
	public Transform Arm{ get{ return transform.GetChild(0); } }
	public Transform Root{ get{ return transform.GetChild(2); } }
	public Transform Elbow{ get{ return Root.GetChild(0); } }
	public Transform Forearm{ get{ return Elbow.GetChild(0); } }
	public Transform Wrist{ get{ return Forearm.GetChild(0); } }
	public Transform Index1{ get{ return Wrist.GetChild(0); } }
	public Transform Index2{ get{ return Index1.GetChild(0); } }
	public Transform Index3{ get{ return Index2.GetChild(0); } }
	public Transform Index4{ get{ return Index3.GetChild(0); } }
	public Transform Middle1{ get{ return Wrist.GetChild(1); } }
	public Transform Middle2{ get{ return Middle1.GetChild(0); } }
	public Transform Middle3{ get{ return Middle2.GetChild(0); } }
	public Transform Middle4{ get{ return Middle3.GetChild(0); } }
	public Transform Ring1{ get{ return Wrist.GetChild(4); } }
	public Transform Ring2{ get{ return Ring1.GetChild(0); } }
	public Transform Ring3{ get{ return Ring2.GetChild(0); } }
	public Transform Ring4{ get{ return Ring3.GetChild(0); } }
	public Transform Pinkie1{ get{ return Wrist.GetChild(3); } }
	public Transform Pinkie2{ get{ return Pinkie1.GetChild(0); } }
	public Transform Pinkie3{ get{ return Pinkie2.GetChild(0); } }
	public Transform Pinkie4{ get{ return Pinkie3.GetChild(0); } }
	public Transform Thumb1{ get{ return Wrist.GetChild(5); } }
	public Transform Thumb2{ get{ return Thumb1.GetChild(0); } }
	public Transform Thumb3{ get{ return Thumb2.GetChild(0); } }
	public Transform Palm{ get{ return Wrist.GetChild(2); } }

	void Awake() {
		
	}

	public void SetWrist(Vector3 angles) {
		Wrist.eulerAngles = angles;
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



