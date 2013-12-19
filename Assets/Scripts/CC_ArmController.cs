using UnityEngine;
using System.Collections;

public class CC_ArmController : CC_Behaviour {
	public float speed;

	private Vector3 position {
		get {
			return transform.position;
		}
	}

	private CC_Controller.Side side;

	#region Joints
	public Transform Root, Wrist;
	private Transform Arm, Elbow, Forearm;

	private Transform Index1, Index2, Index3, Index4;

	private Transform Middle1, Middle2, Middle3, Middle4;

	private Transform Ring1, Ring2, Ring3, Ring4;

	private Transform Pinkie1, Pinkie2, Pinkie3, Pinkie4;

	private Transform Thumb1, Thumb2, Thumb3;

	private Transform Palm;

	#region colliders
	private Transform PalmCollider;
	private Transform Finger1Collider, Finger2Collider, Finger3Collider, Finger4Collider;
	private Transform Thumb1Collider, Thumb2Collider, Thumb3Collider;

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
		Forearm = Elbow.FindChild("Forearm_" + s);
		Wrist = Forearm.FindChild("Wrist_" + s);
		Index1 = Wrist.FindChild(s + "_index1");
		Index2 = Index1.FindChild(s + "_index2");
		Index3 = Index2.FindChild(s + "_index3");
		Index4 = Index3.FindChild(s + "_index4");
		Middle1 = Wrist.FindChild(s + "_middle1");
		Middle2 = Middle1.FindChild(s + "_middle2");
		Middle3 = Middle2.FindChild(s + "_middle3");
		Middle4 = Middle3.FindChild(s + "_middle4");
		Palm = Wrist.FindChild(s + "_Palm_pnt");
		Pinkie1 = Wrist.FindChild(s + "_pinkie1");
		Pinkie2 = Pinkie1.FindChild(s + "_pinkie2");
		Pinkie3 = Pinkie2.FindChild(s + "_pinkie3");
		Pinkie4 = Pinkie3.FindChild(s + "_pinkie4");
		Ring1 = Wrist.FindChild(s + "_ring1");
		Ring2 = Ring1.FindChild(s + "_ring2");
		Ring3 = Ring2.FindChild(s + "_ring3");
		Ring4 = Ring3.FindChild(s + "_ring4");
		Thumb1 = Wrist.FindChild(s + "_thumb1");
		Thumb2 = Thumb1.FindChild(s + "_thumb2");
		Thumb3 = Thumb2.FindChild(s + "_thumb3");

		PalmCollider = Palm.FindChild("Arm_Box_Palm");
		Finger1Collider = Middle1.FindChild("Arm_Box_Middle01");
		Finger2Collider = Middle2.FindChild("Arm_Box_Middle02");
		Finger3Collider = Middle3.FindChild("Arm_Box_Middle03");
		Finger4Collider = Middle4.FindChild("Arm_Box_Middle04");
		Thumb1Collider = Thumb1.FindChild("Arm_Box_Thumb01");
		Thumb2Collider = Thumb2.FindChild("Arm_Box_Thumb02");
		Thumb3Collider = Thumb3.FindChild("Arm_Box_Thumb03");
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
			
			CC_Chopstick chopstick = heldObj.GetComponent<CC_Chopstick>();
			
			if(chopstick != null){
				CC_Camera camera = level.mainCamera.GetComponent<CC_Camera>();
				if(camera.obj1 == Palm){
					camera.obj1 = chopstick.pickupPoint;
				} else if(camera.obj2 == Palm){
					camera.obj2 = chopstick.pickupPoint;
				}
			}
		}
	}

	private void LetGo() {
		if (heldObj != null) {
			CC_Chopstick chopstick = heldObj.GetComponent<CC_Chopstick>();
			
			if(chopstick != null){
				CC_Camera camera = level.mainCamera.GetComponent<CC_Camera>();
				if(camera.obj1 == chopstick.pickupPoint){
					camera.obj1 = Palm;
				} else if(camera.obj2 == chopstick.pickupPoint){
					camera.obj2 = Palm;
				}
			}
			
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

	void OnDrawGizmos() {
		// Gizmos.color = Color.red;
		// Gizmos.DrawRay(Palm.position, Palm.up * 5);
		if(Palm != null) {
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



