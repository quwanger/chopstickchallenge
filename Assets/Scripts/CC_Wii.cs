using UnityEngine;
using System.Collections;

public class CC_Wii : MonoBehaviour{

	public int rightRemote;
	public int leftRemote;
	public Transform leftArm;
	public Transform rightArm;
	public GameObject wiiObject;
	public Vector3 speed;
	public float zDepth;
	public Wii wii;
	public CC_ArmController leftArmController;

	/* todo: move velocity to each hand */
	public Vector3 lastInput;		
	public Vector3 handVelLeft;
	public Vector3 handVelRight;

	void OnGUI () {

	}

	void Start () {
		leftRemote = 0;
		rightRemote = 1;

		wii = wiiObject.GetComponent<Wii>();
		
		zDepth = 15;
	}

	void Update () {
		if(Wii.IsActive(leftRemote) || Wii.IsActive(rightRemote)) {
			// Recalibaration
			Wii.CheckForMotionPlus(leftRemote);
			Wii.CheckForMotionPlus(rightRemote);

			if(Wii.GetButtonDown(leftRemote, "ONE")) {
				Debug.Log("Calibrate Left Remote");
				Wii.CalibrateMotionPlus(leftRemote);
			} else if(Wii.GetButtonDown(leftRemote, "TWO")) {
				Debug.Log("Uncalibrate Left Remote");
				Wii.UncalibrateMotionPlus(leftRemote);
			}

			if(Wii.GetButtonDown(rightRemote, "PLUS")) {
				Wii.CalibrateMotionPlus(rightRemote);
			} else if (Wii.GetButtonDown(rightRemote, "MINUS")) {
				Debug.Log("Calibrate Right Remote");
				Wii.UncalibrateMotionPlus(rightRemote);
			}
			
			var posL = leftArm.transform.position;
			var accL = Wii.GetWiimoteAcceleration(leftRemote);

			var posR = rightArm.transform.position;
			var accR = Wii.GetWiimoteAcceleration(rightRemote);
			
			handVelRight.x = (accR.x)*1.2f;
			handVelRight.y = (accR.y+0.1f)*1.2f;
			handVelLeft.x = (accL.x)*1.2f;
			handVelLeft.y = (accL.y+0.1f)*1.2f;
			
			//leftArm.transform.position = new Vector3(posL.x-handVelLeft.x,posL.y-handVelLeft.y,posL.z);
			//rightArm.transform.position = new Vector3(posR.x-handVelRight.x,posR.y-handVelRight.y,posR.z);
			
			// Z-Depth movement
			// If button is pressed, increase or decrease z-depth
			if(Wii.GetButton(leftRemote, "A") || Wii.GetButton(rightRemote, "A")) {
				leftArm.transform.position = Vector3.Lerp(
					leftArm.transform.position, 
					new Vector3(leftArm.position.x + speed.x,leftArm.position.y + speed.y,leftArm.position.z + (-zDepth)),
					0.03f
				);
				
				rightArm.transform.position = Vector3.Lerp(
					rightArm.transform.position, 
					new Vector3(rightArm.position.x + speed.x,rightArm.position.y + speed.y,rightArm.position.z + (-zDepth)),
					0.03f
				);
			}
		
			if(Wii.GetButton(leftRemote, "B") || Wii.GetButton(rightRemote, "B")) {
				leftArm.transform.position = Vector3.Lerp(
					leftArm.transform.position, 
					new Vector3(leftArm.position.x + speed.x,leftArm.position.y + speed.y,leftArm.position.z + zDepth),
					0.03f
				);
				
				rightArm.transform.position = Vector3.Lerp(
					rightArm.transform.position, 
					new Vector3(rightArm.position.x + speed.x,rightArm.position.y + speed.y,rightArm.position.z + zDepth),
					0.03f
				);
			}
		}

		// Rotations
		if(Wii.HasMotionPlus(leftRemote)) {
			//Debug.Log(Wii.GetMotionPlus(leftRemote));

			if(Wii.IsMotionPlusCalibrated(leftRemote)) {
				// Get motion data
				Vector3 motionLeft = Wii.GetMotionPlus(leftRemote);
				
				// To manually calibrate during the game. This will be put in the pause menu once it's implemented
				if(Input.GetKeyDown("space") || Wii.GetButtonDown(leftRemote,"HOME")) {
					leftArm.localRotation = Quaternion.identity;
				}
				
				// Rotate the object
				//Debug.Log("X: " + leftArmController.Wrist.eulerAngles.x);

				leftArmController.SetWrist(new Vector3(
					leftArmController.Wrist.eulerAngles.x, 
					leftArmController.Wrist.eulerAngles.y, 
					leftArmController.Wrist.eulerAngles.z)
				);

				/*leftArm.RotateAround(leftArm.position,leftArm.right,-motionLeft.x);
				leftArm.RotateAround(leftArm.position,leftArm.up,-motionLeft.y);
				leftArm.RotateAround(leftArm.position,leftArm.forward,-motionLeft.z);*/
			}
		}

		if(Wii.HasMotionPlus(rightRemote)) {
			//Debug.Log(Wii.GetMotionPlus(rightRemote));

			if(Wii.IsMotionPlusCalibrated(rightRemote)) {
				//Get motion data
				Vector3 motionRight = Wii.GetMotionPlus(rightRemote);
				
				// To manually calibrate during the game. This will be put in the pause menu once it's implemented
				if(Input.GetKeyDown("space") || Wii.GetButtonDown(rightRemote,"HOME")) {
					rightArm.localRotation = Quaternion.identity;
				}
				
				// Rotate the object
				/*rightArm.RotateAround(rightArm.position,rightArm.right,-motionRight.x);
				rightArm.RotateAround(rightArm.position,rightArm.up,-motionRight.y);
				rightArm.RotateAround(rightArm.position,rightArm.forward,-motionRight.z);*/
			}
		}
	}
}