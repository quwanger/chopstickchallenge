var whichRemote		: int;
var motionPlus		: Transform;
var WiiObject		: GameObject;
var speed			: Vector3;
var zDepth			: float;
var Wii;

/* todo: move velocity to each hand */
var lastInput		:Vector3;
var handVel			:Vector3;

function OnGUI () {

}

function Start () {
	var totalRemotes = 0;
	whichRemote = 0;
	Wii = WiiObject.GetComponent("Wii");
	
	zDepth = 15;
}

function Update () {

	if(Wii.IsActive(whichRemote)) {
		// Recalibaration
		if(Wii.GetButtonDown(whichRemote, "PLUS")) {
			Wii.StartSearch();
		}
		
		var p = motionPlus.transform.position;
		var a = Wii.GetWiimoteAcceleration(whichRemote);
		
		handVel.x = (a.x)*2;
		handVel.y = (a.y+0.2)*2;
		
		
		motionPlus.transform.position = Vector3(p.x-handVel.x,p.y-handVel.y,p.z);
		
	
		/*
		// IR pointer data
		pointerArray = Wii.GetRawIRData(whichRemote);		
		var wiiIRPos = pointerArray[0];
		// Target position to lerp to
		var target : Vector3;
		
		// If IR value is out of range,
		if(wiiIRPos.x >= 0) { 
			// Range set to left most and right most positions
			target.x = 1010 * (1-wiiIRPos.x) + 960 * (wiiIRPos.x); 
			target.y = -144 * (1-wiiIRPos.y) + -115 * (wiiIRPos.y);
			
			speed = target - motionPlus.position;
			
			// Lerping to the target position
			motionPlus.transform.position = Vector3.Lerp(
				motionPlus.transform.position,
				Vector3(target.x,target.y,motionPlus.position.z),
				0.03
			);
		}
		// When wiimote is not visible
		else{
			// Lerp at a default speed
			motionPlus.transform.position = Vector3.Lerp(
				motionPlus.transform.position,
				Vector3(motionPlus.position.x + speed.x,motionPlus.position.y + speed.y,motionPlus.position.z),
				0.03
			);
			
			speed *= 0.95f;
		}
		*/
		
		// Z-Depth movement
		// If button is pressed, increase or decrease z-depth
		if(Wii.GetButton(whichRemote, "A")) {
			motionPlus.transform.position = Vector3.Lerp(
				motionPlus.transform.position, 
				Vector3(motionPlus.position.x + speed.x,motionPlus.position.y + speed.y,motionPlus.position.z + (-zDepth)),
				0.03
			);
		}
	
		if(Wii.GetButton(whichRemote, "B")) {
			motionPlus.transform.position = Vector3.Lerp(
				motionPlus.transform.position, 
				Vector3(motionPlus.position.x + speed.x,motionPlus.position.y + speed.y,motionPlus.position.z + zDepth),
				0.03
			);
		}
	}

	// Rotations
	if(Wii.HasMotionPlus(whichRemote))
	{	
		if(Wii.IsMotionPlusCalibrated(whichRemote)) {
			// Get motion data
			motion = Wii.GetMotionPlus(whichRemote);
			
			// To manually calibrate during the game. This will be put in the pause menu once it's implemented
			if(Input.GetKeyDown("space") || Wii.GetButtonDown(whichRemote,"HOME"))
			{
				motionPlus.localRotation = Quaternion.identity;
			}
			
			// Rotate the object
			motionPlus.RotateAround(motionPlus.position,motionPlus.right,-motion.x);
			motionPlus.RotateAround(motionPlus.position,motionPlus.up,-motion.y);
			motionPlus.RotateAround(motionPlus.position,motionPlus.forward,-motion.z);
		}
	}
}