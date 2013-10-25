static var whichRemote	: int;
var motionPlus			: Transform;
var WiiObject			: GameObject;

var speed				: float;

var Wii;
var totalRemotes = 0;
whichRemote = 0;

Wii = WiiObject.GetComponent("Wii");

function OnGUI () {

}

function Start () {
	
}

function Update () {
	var wiiAccel

	if(Wii.GetButtonDown(whichRemote, "PLUS")) {
		Wii.StartSearch();
	}

	pointerArray = Wii.GetRawIRData(whichRemote);		
	mainPointer = Wii.GetIRPosition(whichRemote);
	
	Debug.Log("01: " + pointerArray[0]);
	
	///Debug.Log(mainPointer);
	
	//1010 -> 960
	var lerpX = pointerArray[0].x;
	
	if(lerpX>=0){
		var targetX = 1010*(1-lerpX) + 960*(lerpX);
		
		motionPlus.transform.position = Vector3.Lerp(
			motionPlus.transform.position,
			Vector3(targetX,motionPlus.transform.position.y,motionPlus.transform.position.z),
			0.03
			);
	}
	
	//motionPlus.transform.position = motionPlus.transform.position + new Vector3(

	/*theIRMain.pixelInset = Rect(mainPointer.x*Screen.width-25.0f,mainPointer.y*Screen.height-25.0f,50.0,50.0);
	var sizeScale = 5.0f;
	
	theIR1.pixelInset = Rect (pointerArray[0].x*Screen.width-(pointerArray[0].z*sizeScale/2.0f),
	 							pointerArray[0].y*Screen.height-(pointerArray[0].z*sizeScale/2.0f),
	 							pointerArray[0].z*sizeScale*10, pointerArray[0].z*sizeScale*10);
	 							
	theIR2.pixelInset = Rect (pointerArray[1].x*Screen.width-(pointerArray[1].z*sizeScale/2.0f),
								pointerArray[1].y*Screen.height-(pointerArray[1].z*sizeScale/2.0f),
								pointerArray[1].z*sizeScale*10, pointerArray[1].z*sizeScale*10);
								
	theIR3.pixelInset = Rect (pointerArray[2].x*Screen.width-(pointerArray[2].z*sizeScale/2.0f),
								pointerArray[2].y*Screen.height-(pointerArray[2].z*sizeScale/2.0f),
								pointerArray[2].z*sizeScale*10, pointerArray[2].z*sizeScale*10);
								
	theIR4.pixelInset = Rect (pointerArray[3].x*Screen.width-(pointerArray[3].z*sizeScale/2.0f),
								pointerArray[3].y*Screen.height-(pointerArray[3].z*sizeScale/2.0f),
								pointerArray[3].z*sizeScale*10, pointerArray[3].z*sizeScale*10);*/

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