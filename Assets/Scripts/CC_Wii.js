var rightRemote		: int;
var leftRemote		: int;
var leftArm			: Transform;
var rightArm		: Transform;
var WiiObject		: GameObject;
var speed			: Vector3;
var zDepth			: float;
var Wii;

/* todo: move velocity to each hand */
var lastInput		: Vector3;
var handVelLeft		: Vector3;
var handVelRight    : Vector3;

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
		if(Wii.GetButtonDown(leftRemote, "PLUS")) {
			Wii.StartSearch();
		} else if(Wii.GetButtonDown(rightRemote, "MINUS")) {
			Wii.StartSearch();
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
	
		/*
		// IR pointer data
		pointerArray = Wii.GetRawIRData(leftRemote);		
		var wiiIRPos = pointerArray[0];
		// Target position to lerp to
		var target : Vector3;
		
		// If IR value is out of range,
		if(wiiIRPos.x >= 0) { 
			// Range set to left most and right most positions
			target.x = 1010 * (1-wiiIRPos.x) + 960 * (wiiIRPos.x); 
			target.y = -144 * (1-wiiIRPos.y) + -115 * (wiiIRPos.y);
			
			speed = target - leftArm.position;
			
			// Lerping to the target position
			leftArm.transform.position = Vector3.Lerp(
				leftArm.transform.position,
				Vector3(target.x,target.y,leftArm.position.z),
				0.03
			);
		}
		// When wiimote is not visible
		else{
			// Lerp at a default speed
			leftArm.transform.position = Vector3.Lerp(
				leftArm.transform.position,
				Vector3(leftArm.position.x + speed.x,leftArm.position.y + speed.y,leftArm.position.z),
				0.03
			);
			
			speed *= 0.95f;
		}
		*/
		
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
		Debug.Log(Wii.GetMotionPlus(leftRemote));

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