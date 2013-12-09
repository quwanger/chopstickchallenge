using UnityEngine;
using System.Collections;

public class CC_ArmController : MonoBehaviour {
	public float speed;

	private Vector3 position {
		get {
			return transform.position;
		}
	}

	#region Joints
	private Transform Arm{ get{ return transform.GetChild(0); } }

	public Transform Root{ get{ return transform.GetChild(1); } }

	private Transform Elbow{ get{ return Root.GetChild(0); } }

	private Transform Forearm{ get{ return Elbow.GetChild(0); } }

	public Transform Wrist{ get{ return Forearm.GetChild(0); } }

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

	#endregion

	public bool clenched = false;
	public bool clenchButton = false;

	private float restZPosition;
	public float zSpeed = 0.0f;
	public float zLimit = 100.0f;
	public float zSpring = 0.05f;

	private int clenchTimer = 10;

	public float palmDistance = 2.5f;

	// private SphereCollider PalmZone;

	CC_Pickup heldObj;
	
	void Awake() {
		restZPosition = position.z;
	}

	void Start () {
		speed = 0.5f;
		// PalmZone = Wrist.GetComponent<SphereCollider>();
		// Debug.Log(PalmZone.gameObject);
	}
	
	void Update () {
		//InputHandle();

		// if (wiiClench || Input.GetKey(KeyCode.W) || Input.GetAxis("Fire1") == 1) {
		if (clenchButton) {
			if(animation["Clench"].time != 0)
				PickUp();

			MoveObj();
			if(!animation.IsPlaying("Clench") && clenched == false){
				animation.Play("Clench");
			}
			clenched = true;
			animation["Clench"].speed = 1.0f;
		}
		else{
			LetGo();
			if(!animation.IsPlaying("Clench") && clenched == true){
				animation.Play("Clench");
				animation["Clench"].time = animation["Clench"].length;
			}

			clenched = false;
			animation["Clench"].speed = -1.0f;
		}

		// Z Direction
		//transform.position = new Vector3(position.x, position.y, (position.z - zSpeed, 2));
	}

	private void MoveObj() {
		if (heldObj == null)
			return;

		heldObj.transform.position = Index1.position + heldObj.fromHand;
	}

	private void PickUp(){
		RaycastHit hit;
		//if (Physics.SphereCast(Wrist.position, palmDistance, Wrist.up, out hit, CCUtils.LayerMask(8))) {
		//	Debug.Log(hit.collider.gameObject);
		//}
		int[] mask = {8, 9};

		Debug.Log("PickUp");
		if (Physics.Raycast(Palm.position + Wrist.up*1.5f, -Wrist.up, out hit, palmDistance, CCUtils.LayerMask(mask)) ||
			Physics.Raycast(Palm.position + Wrist.up*1.5f -  Wrist.right * 5, -Wrist.up, out hit, palmDistance, CCUtils.LayerMask(mask)) ||
			Physics.Raycast(Palm.position + Wrist.up*1.5f  + Wrist.right * 5, -Wrist.up, out hit, palmDistance, CCUtils.LayerMask(mask))) {
			Debug.Log("PickUp obj " + hit.collider.gameObject);
			if(hit.collider.gameObject.GetComponent<CC_Pickup>() != null && heldObj == null)
				HoldObj(hit.collider.gameObject.GetComponent<CC_Pickup>());
		}
	}

	private void HoldObj(CC_Pickup obj) {
		heldObj = obj;
		heldObj.rigidbody.freezeRotation = true;
		heldObj.fromHand = heldObj.transform.position - Palm.position;
	}

	private void LetGo() {
		if (heldObj != null) {
			heldObj.rigidbody.freezeRotation = false;
			heldObj = null;
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



