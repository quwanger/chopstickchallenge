using UnityEngine;
using System.Collections;

public class CC_ArmController : MonoBehaviour {
	public GameObject arm;
	public float speed;

	private Vector3 position {
		get {
			return transform.position;
		}
	}

	private Transform Arm{ get{ return transform.GetChild(0); } }

	private Transform Root{ get{ return transform.GetChild(2); } }

	private Transform Elbow{ get{ return Root.GetChild(0); } }

	private Transform Forearm{ get{ return Elbow.GetChild(0); } }

	private Transform Wrist{ get{ return Forearm.GetChild(0); } }

	private Transform Index1{ get{ return Wrist.GetChild(0); } }
	private Transform Index2{ get{ return Index1.GetChild(0); } }
	private Transform Index3{ get{ return Index2.GetChild(0); } }
	private Transform Index4{ get{ return Index3.GetChild(0); } }

	private Transform Middle1{ get{ return Wrist.GetChild(1); } }
	private Transform Middle2{ get{ return Middle1.GetChild(0); } }
	private Transform Middle3{ get{ return Middle2.GetChild(0); } }
	private Transform Middle4{ get{ return Middle3.GetChild(0); } }

	private Transform Ring1{ get{ return Wrist.GetChild(4); } }
	private Transform Ring2{ get{ return Ring1.GetChild(0); } }
	private Transform Ring3{ get{ return Ring2.GetChild(0); } }
	private Transform Ring4{ get{ return Ring3.GetChild(0); } }

	private Transform Pinkie1{ get{ return Wrist.GetChild(3); } }
	private Transform Pinkie2{ get{ return Pinkie1.GetChild(0); } }
	private Transform Pinkie3{ get{ return Pinkie2.GetChild(0); } }
	private Transform Pinkie4{ get{ return Pinkie3.GetChild(0); } }

	private Transform Thumb1{ get{ return Wrist.GetChild(5); } }
	private Transform Thumb2{ get{ return Thumb1.GetChild(0); } }
	private Transform Thumb3{ get{ return Thumb2.GetChild(0); } }

	private Transform Palm{ get{ return Wrist.GetChild(2); } }

	public bool clenched = false;
	
	void Awake() {
		
	}

	void Start () {
		speed = 0.5f;
	}
	
	void Update () {
		InputHandle();
		if (Input.GetKey (KeyCode.W)){
			if(!animation.IsPlaying("Clench") && clenched == false){
				animation.Play("Clench");
			}
			clenched = true;
			animation["Clench"].speed = 1.0f;
		}
		else{
			if(!animation.IsPlaying("Clench") && clenched == true){
				animation.Play("Clench");
				animation["Clench"].time = animation["Clench"].length;
			}

			clenched = false;
			animation["Clench"].speed = -1.0f;
		}
	}

	private void InputHandle(){
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



