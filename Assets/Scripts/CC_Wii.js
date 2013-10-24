static var whichRemote	: int;
var motionPlus			: Transform;
var WiiObject			: GameObject;

var Wii;
var totalRemotes = 0;
whichRemote = 0;

Wii = WiiObject.GetComponent("Wii");

function OnGUI () {

}

function Start () {
	
}

function Update () {
	wiiAccel = Wii.GetWiimoteAcceleration(whichRemote);
	
	if(Wii.HasMotionPlus(whichRemote))
	{	
		if(Wii.IsMotionPlusCalibrated(whichRemote)) {
			
			Debug.Log("JDLS");
			
			motion = Wii.GetMotionPlus(whichRemote);
			
			if(Input.GetKeyDown("space") || Wii.GetButtonDown(whichRemote,"HOME"))
			{
				motionPlus.localRotation = Quaternion.identity;
			}
			
			motionPlus.RotateAround(motionPlus.position,motionPlus.right,-motion.x);
			motionPlus.RotateAround(motionPlus.position,motionPlus.up,-motion.y);
			motionPlus.RotateAround(motionPlus.position,motionPlus.forward,-motion.z);
			
			Debug.Log(motion);
		}
	}
}