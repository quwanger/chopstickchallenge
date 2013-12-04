var rightRemote			: int;
var leftRemote			: int;
var leftArm				: Transform;
var rightArm			: Transform;
var WiiObject			: GameObject;
var speed				: Vector3;
var zDepth				: float;
var Wii;
var leftArmController;

/* todo: move velocity to each hand */
var lastInput			: Vector3;
var handVelLeft			: Vector3;
var handVelRight    	: Vector3;

function OnGUI () {

}

function Start () {
	leftRemote = 0;
	rightRemote = 1;

	Wii = WiiObject.GetComponent("Wii");
	
	zDepth = 15;
}

function Update () {
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
		
		handVelRight.x = (accR.x)*1.2;
		handVelRight.y = (accR.y+0.1)*1.2;
		handVelLeft.x = (accL.x)*1.2;
		handVelLeft.y = (accL.y+0.1)*1.2;
		
		leftArm.transform.position = Vector3(posL.x-handVelLeft.x,posL.y-handVelLeft.y,posL.z);
		rightArm.transform.position = Vector3(posR.x-handVelRight.x,posR.y-handVelRight.y,posR.z);
		
		// Z-Depth movement
		// If button is pressed, increase or decrease z-depth
		if(Wii.GetButton(leftRemote, "A") || Wii.GetButton(rightRemote, "A")) {
			leftArm.transform.position = Vector3.Lerp(
				leftArm.transform.position, 
				Vector3(leftArm.position.x + speed.x,leftArm.position.y + speed.y,leftArm.position.z + (-zDepth)),
				0.03
			);
			
			rightArm.transform.position = Vector3.Lerp(
				rightArm.transform.position, 
				Vector3(rightArm.position.x + speed.x,rightArm.position.y + speed.y,rightArm.position.z + (-zDepth)),
				0.03
			);
		}
	
		if(Wii.GetButton(leftRemote, "B") || Wii.GetButton(rightRemote, "B")) {
			leftArm.transform.position = Vector3.Lerp(
				leftArm.transform.position, 
				Vector3(leftArm.position.x + speed.x,leftArm.position.y + speed.y,leftArm.position.z + zDepth),
				0.03
			);
			
			rightArm.transform.position = Vector3.Lerp(
				rightArm.transform.position, 
				Vector3(rightArm.position.x + speed.x,rightArm.position.y + speed.y,rightArm.position.z + zDepth),
				0.03
			);
		}
	}

	// Rotations
	if(Wii.HasMotionPlus(leftRemote)) {
		//Debug.Log(Wii.GetMotionPlus(leftRemote));

		if(Wii.IsMotionPlusCalibrated(leftRemote)) {
			// Get motion data
			motionLeft = Wii.GetMotionPlus(leftRemote);
			
			// To manually calibrate during the game. This will be put in the pause menu once it's implemented
			if(Input.GetKeyDown("space") || Wii.GetButtonDown(leftRemote,"HOME")) {
				leftArm.localRotation = Quaternion.identity;
			}
			
			// Rotate the object
			leftArm.RotateAround(leftArm.position,leftArm.right,-motionLeft.x);
			leftArm.RotateAround(leftArm.position,leftArm.up,-motionLeft.y);
			leftArm.RotateAround(leftArm.position,leftArm.forward,-motionLeft.z);
		}
	}

	if(Wii.HasMotionPlus(rightRemote)) {
		//Debug.Log(Wii.GetMotionPlus(rightRemote));

		if(Wii.IsMotionPlusCalibrated(rightRemote)) {
			//Get motion data
			motionRight = Wii.GetMotionPlus(rightRemote);
			
			// To manually calibrate during the game. This will be put in the pause menu once it's implemented
			if(Input.GetKeyDown("space") || Wii.GetButtonDown(rightRemote,"HOME")) {
				rightArm.localRotation = Quaternion.identity;
			}
			
			// Rotate the object
			rightArm.RotateAround(rightArm.position,rightArm.right,-motionRight.x);
			rightArm.RotateAround(rightArm.position,rightArm.up,-motionRight.y);
			rightArm.RotateAround(rightArm.position,rightArm.forward,-motionRight.z);
		}
	}
}