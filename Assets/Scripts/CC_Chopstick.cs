using UnityEngine;
using System.Collections;

public class CC_Chopstick : CC_Pickup {
	
	public float MagneticConstant = 1.0f;

	public Transform pickupPoint;
	private CC_Chopstick otherChopstick;

	private bool pickupAble = false;

	public Vector3 position {get{ return transform.position; }}
	
	private GameObject heldObj;
	
	// Use this for initialization
	void Start () {
		pickupPoint = transform.GetChild(0).GetChild(0);

		if (level.chopsticks.Count == 1) {
			otherChopstick = level.chopsticks[0];
			otherChopstick.otherChopstick = this;
			Debug.Log(this.gameObject);
			Debug.Log(this.otherChopstick.gameObject);
		}

		level.RegisterChopstick(this);
	}
	
	// Update is called once per frame
	void Update () {
		if(heldObj != null){
			heldObj.rigidbody.constraints = RigidbodyConstraints.None;
			heldObj = null;
		}
		
		Debug.Log ((pickupPoint.position - otherChopstick.pickupPoint.position).magnitude);
		if((pickupPoint.position - otherChopstick.pickupPoint.position).magnitude < level.ChopstickPickupDistance){
			pickupAble = true;
		}
		else
			pickupAble = false;

		RaycastHit hit;

		if(pickupAble){
			if(Physics.Linecast(pickupPoint.position, otherChopstick.pickupPoint.position, out hit)){
				Vector3 midPoint = Vector3.Lerp(pickupPoint.position, otherChopstick.pickupPoint.position, 0.5f);
				
				if(hit.collider.gameObject.GetComponent<CC_Sushi>()){
					hit.collider.gameObject.transform.position = midPoint;
					hit.collider.gameObject.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
					
					heldObj = hit.collider.gameObject;
				}
			}
		}
	}
	
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.GetComponent<CC_Chopstick>()){
			level.soundManager.playSound(CC_Level.SoundType.chopstickCollide);
		}else{
			//hits anything else
		}
	}

	void OnDrawGizmos() {
		if (otherChopstick == null)
			return;

		if(pickupAble)
			Gizmos.color = Color.yellow;
		else
			Gizmos.color = Color.red;
		Gizmos.DrawLine(pickupPoint.position, otherChopstick.pickupPoint.position);
	}
}
