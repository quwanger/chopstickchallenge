using UnityEngine;
using System.Collections;

public class CC_ArmController : MonoBehaviour {
	public float speed;

	private Vector3 position {
		get {
			return transform.position;
		}
	}

	private CC_Controller.Side side;

	#region Joints
	private Transform Arm, Root, Elbow, Forearm, Wrist;

	private Transform Index1, Index2, Index3, Index4;

	private Transform Middle1, Middle2, Middle3, Middle4;

	private Transform Ring1, Ring2, Ring3, Ring4;

	private Transform Pinkie1, Pinkie2, Pinkie3, Pinkie4;

	private Transform Thumb1, Thumb2, Thumb3;

	private Transform Palm;

	#region colliders

	private Transform PalmCollider{ get{ return Palm.GetChild(0); } }
	private Transform Finger1Collider{ get{ return Middle1.GetChild(1); } }
	private Transform Finger2Collider{ get{ return Middle2.GetChild(1); } }
	private Transform Finger3Collider{ get{ return Middle3.GetChild(1); } }
	private Transform Finger4Collider{ get{ return Middle4.GetChild(0); } }

	private Transform Thumb1Collider{ get{ return Thumb1.GetChild(1); } }
	private Transform Thumb2Collider{ get{ return Thumb2.GetChild(1); } }
	private Transform Thumb3Collider{ get{ return Thumb3.GetChild(0); } }

	#endregion

	#endregion

	private CC_PickupHandler pickupHandler;

	public bool clenched = false;
	public bool clenchButton = false;

	public bool isHandClosed = false;

	private float restZPosition;
	public float zSpeed = 0.0f;
	public float zLimit = 100.0f;
	public float zSpring = 0.05f;

	private int clenchTimer = 10;

	public float palmDistance = 2.5f;

	// private SphereCollider PalmZone;

	CC_Pickup heldObj;
	
	void Awake() {
		side = GetComponent<CC_Controller>().side;
		initJoints();

		restZPosition = position.z;
		pickupHandler = Middle1.GetComponent<CC_PickupHandler>();
	}

	void Start () {
		speed = 0.5f;
		// PalmZone = Wrist.GetComponent<SphereCollider>();
		// Debug.Log(PalmZone.gameObject);
	}

	private void initJoints() {
		char s = (side == CC_Controller.Side.right) ? 'R' : 'L';
		Arm = transform.FindChild("Arm_" + s);
		Root = transform.FindChild("Root_" + s);
		Elbow = Root.FindChild("Elbow_" + s);
		//Forearm
		//Wrist
		//Index1
		//Index2
		//Index3
		//Index4
		//Middle1
		//Middle2
		//Middle3
		//Middle4
		//Ring1
		//Ring2
		//Ring3
		//Ring4
		//Pinkie1
		//Pinkie2
		//Pinkie3
		//Pinkie4
		//Thumb1
		//Thumb2
		//Thumb3
		//Palm
	}
	
	void Update () {
		//InputHandle();

		// if (wiiClench || Input.GetKey(KeyCode.W) || Input.GetAxis("Fire1") == 1) {
		if (clenchButton) {

			if(animation["Clench"].time != 0)
				PickUp();
			else{
				isHandClosed = true;
			}

			MoveObj();
			if(!animation.IsPlaying("Clench") && clenched == false){
				animation.Play("Clench");
			}
			clenched = true;
			animation["Clench"].speed = 1.0f;
			SetHandTrigger(clenched);
		}
		else{
			LetGo();
			if(!animation.IsPlaying("Clench") && clenched == true){
				animation.Play("Clench");
				animation["Clench"].time = animation["Clench"].length;
			}
			else if(animation["Clench"].time == 0){
				isHandClosed = false;
				SetHandTrigger(isHandClosed);
			}

			clenched = false;
			animation["Clench"].speed = -1.0f;
		}
	}

	private void MoveObj() {
		if (heldObj == null)
			return;

		// heldObj.transform.position = Index1.position + heldObj.fromHand;
	}

	private void PickUp(){
		CC_Pickup obj = pickupHandler.GetItem();
		if(obj != null){
			if(obj.gameObject.GetComponent<CC_Pickup>() != null && heldObj == null)
				HoldObj(obj.gameObject.GetComponent<CC_Pickup>());
		}
	}

	private void HoldObj(CC_Pickup obj) {
		if(heldObj == null){
			heldObj = obj;
			// heldObj.rigidbody.freezeRotation = true;
			heldObj.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			heldObj.transform.parent = Palm;
			// heldObj.fromHand = heldObj.transform.position - Palm.position;
		}
	}

	private void LetGo() {
		if (heldObj != null) {
			heldObj.rigidbody.constraints = RigidbodyConstraints.None;
			heldObj.transform.parent = null;
			// heldObj.rigidbody.freezeRotation = false;
			heldObj = null;
			pickupHandler.Empty();
		}
	}

	private void SetHandTrigger(bool clench){
		PalmCollider.GetComponent<BoxCollider>().isTrigger = clench;

		Finger1Collider.GetComponent<Collider>().isTrigger = clench;
		Finger2Collider.GetComponent<Collider>().isTrigger = clench;
		Finger3Collider.GetComponent<Collider>().isTrigger = clench;
		Finger4Collider.GetComponent<Collider>().isTrigger = clench;

		Thumb1Collider.GetComponent<Collider>().isTrigger = clench;
		Thumb2Collider.GetComponent<Collider>().isTrigger = clench;
		Thumb3Collider.GetComponent<Collider>().isTrigger = clench;
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

	void OnDrawGizmos() {
		// Gizmos.color = Color.red;
		// Gizmos.DrawRay(Palm.position, Palm.up * 5);
		Gizmos.color = Color.cyan;
		Gizmos.DrawRay(Palm.position, Palm.forward * 5);
		Gizmos.color = Color.yellow;
		Gizmos.DrawRay(Palm.position, Palm.right * 5);

		Gizmos.color= Color.red;
		Gizmos.DrawCube(Palm.position+Wrist.up*1.5f, new Vector3(0.5f, 0.5f, 0.5f));

		Gizmos.color = Color.magenta;
		Gizmos.DrawRay(Palm.position+Wrist.up*1.5f, -Wrist.up * palmDistance);

		Gizmos.color = Color.magenta;
		Gizmos.DrawRay(Palm.position+Wrist.up*1.5f + Wrist.right * 5, -Wrist.up * palmDistance);

		Gizmos.color = Color.magenta;
		Gizmos.DrawRay(Palm.position+Wrist.up*1.5f - Wrist.right * 5, -Wrist.up * palmDistance);
	}

	#region InputHandlers
	public void TranslateZ(float speed){
		zSpeed = speed;
	}

	public void Clench(){
		clenchButton = true;
	}

	public void Unclench(){
		clenchButton = false;
	}
	#endregion
}



