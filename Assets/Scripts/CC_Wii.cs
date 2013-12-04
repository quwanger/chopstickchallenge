using UnityEngine;
using System.Collections;

public class CC_Wii : MonoBehaviour{

	public int rightRemote;
	public int leftRemote;
	public GameObject wiiObject;
	public Vector3 speed;
	public float zDepth;
	public Wii wii;
	public CC_ArmController leftArmController;
	public CC_ArmController rightArmController;

	/* todo: move velocity to each hand */
	public Vector3 lastInput;		
	public Vector3 handVelLeft;
	public Vector3 handVelRight;

	public float resetX;
	public float resetY;
	public float resetZ;

	void OnGUI () {

	}

	void Start () {
		leftRemote = 0;
		rightRemote = 1;

		wii = wiiObject.GetComponent<Wii>();

		zDepth = 15;

		resetX = -12.93f;
		resetY = 71.9f;
		resetZ = -9.5f;
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
			
			/************************* IR ******************************/
			// IR pointer data
			// Vector3[] pointerArray = Wii.GetRawIRData(leftRemote);	
			// Vector3 wiiIRPos = pointerArray[0];

			// // If IR value is out of range,
			// if(wiiIRPos.x >= 0) { 
			// 	// Range set to left most and right most positions
			// 	target.x = 1010 * (1-wiiIRPos.x) + 960 * (wiiIRPos.x); 
			// 	target.y = -144 * (1-wiiIRPos.y) + -115 * (wiiIRPos.y);

			// 	speed = target - leftArmController.Root.position;
				
			// 	// Lerping to the target position
			// 	leftArmController.Root.position = Vector3.Lerp(
			// 		leftArmController.Root.position,
			// 		new Vector3(target.x,target.y,leftArmController.Root.position.z),
			// 		0.03f
			// 	);
			// }
			// // When wiimote is not visible
			// else{
			// 	// Lerp at a default speed
			// 	leftArmController.Root.position = Vector3.Lerp(
			// 		leftArmController.Root.position,
			// 		Vector3(leftArmController.Root.position.x + speed.x,leftArmController.Root.position.y + speed.y,leftArmController.Root.position.z),
			// 		0.03
			// 	);
				
			// 	speed *= 0.95f;
			// }
			/************************* IR ******************************/

			/************************* ACCEL ******************************/
			// var posL = leftArmController.Root.position;
			// var accL = Wii.GetWiimoteAcceleration(leftRemote);

			// var posR = rightArmController.Root.position;
			// var accR = Wii.GetWiimoteAcceleration(rightRemote);
			
			// handVelRight.x = (accR.x)*1.2f;
			// handVelRight.y = (accR.y+0.1f)*1.2f;
			// handVelLeft.x = (accL.x)*1.2f;
			// handVelLeft.y = (accL.y+0.1f)*1.2f;
			
			// leftArmController.Root.position = new Vector3(posL.x-handVelLeft.x,posL.y-handVelLeft.y,posL.z);
			// rightArmController.Root.position = new Vector3(posR.x-handVelRight.x,posR.y-handVelRight.y,posR.z);
			/************************* ACCEL ******************************/
			
			// Z-Depth movement
			// If button is pressed, increase or decrease z-depth
			if(Wii.GetButton(leftRemote, "UP")) {
				leftArmController.transform.position = Vector3.Lerp(
					leftArmController.transform.position, 
					new Vector3(leftArmController.transform.position.x + speed.x,leftArmController.transform.position.y + speed.y,leftArmController.transform.position.z + (-zDepth)),
					0.03f
				);
			} else if(Wii.GetButton(rightRemote, "UP")) {
				rightArmController.transform.position = Vector3.Lerp(
					rightArmController.transform.position, 
					new Vector3(rightArmController.transform.position.x + speed.x,rightArmController.transform.position.y + speed.y,rightArmController.transform.position.z + (-zDepth)),
					0.03f
				);
			}
		
			if(Wii.GetButton(leftRemote, "DOWN")) {
				leftArmController.transform.position = Vector3.Lerp(
					leftArmController.transform.position, 
					new Vector3(leftArmController.transform.position.x + speed.x,leftArmController.transform.position.y + speed.y,leftArmController.transform.position.z + zDepth),
					0.03f
				);
			} else if(Wii.GetButton(rightRemote, "DOWN")) {
				rightArmController.transform.position = Vector3.Lerp(
					rightArmController.transform.position, 
					new Vector3(rightArmController.transform.position.x + speed.x,rightArmController.transform.position.y + speed.y,rightArmController.transform.position.z + zDepth),
					0.03f
				);
			}

			if(Wii.GetButtonDown(leftRemote, "B")) {
				leftArmController.Clench();
			} else if(Wii.GetButtonDown(rightRemote, "B")) {
				rightArmController.Clench();
			}

			if(Wii.GetButtonUp(leftRemote, "B")) {
				leftArmController.Unclench();
			} else if(Wii.GetButtonUp(rightRemote, "B")) {
				rightArmController.Unclench();
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
					//leftArm.localRotation = Quaternion.identity;
					leftArmController.Wrist.rotation = Quaternion.Euler(resetX,resetY,resetZ);
					leftArmController.Root.rotation = Quaternion.Euler(0,90,0);
				}

				// Rotate the object
				leftArmController.Root.Rotate(leftArmController.Root.forward,-motionLeft.x*2,Space.World);
				leftArmController.Root.Rotate(leftArmController.Root.up,-motionLeft.y*2,Space.World);
				leftArmController.Wrist.Rotate(leftArmController.Wrist.forward,-motionLeft.x*2,Space.World);
				leftArmController.Wrist.Rotate(leftArmController.Wrist.up,-motionLeft.y*2,Space.World);
			}
		}

		if(Wii.HasMotionPlus(rightRemote)) {
			//Debug.Log(Wii.GetMotionPlus(rightRemote));

			if(Wii.IsMotionPlusCalibrated(rightRemote)) {
				//Get motion data
				Vector3 motionRight = Wii.GetMotionPlus(rightRemote);
				
				// To manually calibrate during the game. This will be put in the pause menu once it's implemented
				if(Input.GetKeyDown("space") || Wii.GetButtonDown(rightRemote,"HOME")) {
					//rightArm.localRotation = Quaternion.identity;
					rightArmController.Wrist.rotation = Quaternion.Euler(resetX,resetY,resetZ);
					rightArmController.Root.rotation = Quaternion.Euler(0,90,0);
				}
				
				// Rotate the object
				rightArmController.Root.Rotate(rightArmController.Root.forward,-motionRight.x*2,Space.World);
				rightArmController.Root.Rotate(rightArmController.Root.up,motionRight.y*2,Space.World);
				rightArmController.Wrist.Rotate(rightArmController.Wrist.forward,-motionRight.x*2,Space.World);
				rightArmController.Wrist.Rotate(rightArmController.Wrist.up,motionRight.y*2,Space.World);
			}
		}
	}
}