using UnityEngine;
using System.Collections;

public class CC_Controller : MonoBehaviour {

	public enum ControllerType { xbox, wii, keyboard };
	public ControllerType controllerType = ControllerType.keyboard;

	public enum Side { left, right };
	public Side side = Side.left;

	public float Sensitivity = 1.0f;
	public float TranslateLerp = 0.03f;
	public float RotateLerp = 0.01f;

	public float TranslateSpeed = 15.0f;
	public float RotateSpeed = 2.0f;
	public float ZTranslateSpeed = 0.07f;

	//public CC_ArmController leftArmController;
	//public CC_ArmController rightArmController;

	public CC_ArmController arm;
	public float zDepth = 15.0f;

	// Use this for initialization
	void Start () {
		arm = GetComponent<CC_ArmController>();
	}
	
	// Update is called once per frame
	void Update () {
		switch (controllerType) {
			case ControllerType.xbox:
				XboxHandler();
				break;

			case ControllerType.wii:
				WiiHandler();
				break;

			case ControllerType.keyboard:
				KeyboardHandler();
				break;

		}
	}

	private void KeyboardHandler() {
		if (side == Side.left) {
			// Translate
			if (Input.GetKey(KeyCode.W)) {
				translateArm(0.0f, TranslateSpeed * Sensitivity, 0.0f);
			}
			if (Input.GetKey(KeyCode.S)) {
				translateArm(0.0f, -TranslateSpeed * Sensitivity, 0.0f);
			}
			if (Input.GetKey(KeyCode.A)) {
				translateArm(TranslateSpeed * Sensitivity, 0.0f, 0.0f);
			}
			if (Input.GetKey(KeyCode.D)) {
				translateArm(-TranslateSpeed * Sensitivity, 0.0f, 0.0f);
			}
			if (Input.GetKey(KeyCode.Q)) {
				translateArm(0.0f, 0.0f, TranslateSpeed * Sensitivity);
			}
			if (Input.GetKey(KeyCode.E)) {
				translateArm(0.0f, 0.0f, -TranslateSpeed * Sensitivity);
			}

			// Rotate Wrist
			if (Input.GetKey(KeyCode.LeftShift)) {
				if (Input.GetKey(KeyCode.F)) {
					rotateWrist(-RotateSpeed, 0.0f, 0.0f);
				}
				if (Input.GetKey(KeyCode.H)) {
					rotateWrist(RotateSpeed, 0.0f, 0.0f);
				}
				if (Input.GetKey(KeyCode.T)) {
					rotateWrist(0.0f, RotateSpeed, 0.0f);
				}
				if (Input.GetKey(KeyCode.G)) {
					rotateWrist(0.0f, -RotateSpeed, 0.0f);
				}
				if (Input.GetKey(KeyCode.R)) {
					rotateWrist(0.0f, 0.0f, RotateSpeed);
				}
				if (Input.GetKey(KeyCode.Y)) {
					rotateWrist(0.0f, 0.0f, -RotateSpeed);
				}
			}

			// Rotate Arm
			else {
				if (Input.GetKey(KeyCode.F)) {
					rotateArm(-RotateSpeed, 0.0f, 0.0f);
				}
				if (Input.GetKey(KeyCode.H)) {
					rotateArm(RotateSpeed, 0.0f, 0.0f);
				}
				if (Input.GetKey(KeyCode.T)) {
					rotateArm(0.0f, RotateSpeed, 0.0f);
				}
				if (Input.GetKey(KeyCode.G)) {
					rotateArm(0.0f, -RotateSpeed, 0.0f);
				}
				if (Input.GetKey(KeyCode.R)) {
					rotateArm(0.0f, 0.0f, RotateSpeed);
				}
				if (Input.GetKey(KeyCode.Y)) {
					rotateArm(0.0f, 0.0f, -RotateSpeed);
				}
			}

			// Clench
			if(Input.GetKey(KeyCode.Space)){
				arm.Clench();
			} else {
				arm.Unclench();
			}
		}
		if (side == Side.right){
			if(Input.GetKey(KeyCode.UpArrow)) {
				translateArm(0, 0, -TranslateSpeed);
			}
		}
	}

	private void WiiHandler() {

	}

	private void XboxHandler() {
		// //Left joystick, movement in x-axis 
		// this.transform.position = this.transform.position + new Vector3(Input.GetAxis("HorizontalL") * Sensitivity, Input.GetAxis("VerticalL") * Sensitivity, 0);

		// //Right Joystick, rotation (right)
		// if (Input.GetAxis("HorizontalR") == 1) {
		// 	float rot = this.transform.rotation.x;
		// 	rot += 2.0f;
		// 	this.transform.Rotate(rot, 0, 0);
		// }

		// //Right Joystick, rotation (left)
		// if (Input.GetAxis("HorizontalR") == -1) {
		// 	float rot = this.transform.rotation.z;
		// 	rot -= 2.0f;
		// 	this.transform.Rotate(rot, 0, 0);
		// }
		
		

		if(side == Side.left){			
			translateArm(
				Input.GetAxis("HorizontalL") * Sensitivity * TranslateSpeed,
				Input.GetAxis("VerticalR") * Sensitivity * TranslateSpeed,
				//(Mathf.Pow(Input.GetAxis("LBumper"), 2) - Mathf.Pow(Input.GetAxis("RBumper"), 2)) * Sensitivity * TranslateSpeed,
				Input.GetAxis("VerticalL") * Sensitivity * TranslateSpeed
			);
			
			if(Input.GetAxis("RBumper") == 1 || Input.GetAxis ("LBumper") == 1){
				arm.Clench();
			} else {
				arm.Unclench();
			}
			
			rotateWrist(RotateSpeed * (Input.GetAxis("Fire3") - Input.GetAxis("Fire2")), -Input.GetAxis("VerticalR"), Input.GetAxis("HorizontalR"));
		}
		
		if(side == Side.right){			
			translateArm(
				Input.GetAxis("2_HorizontalL") * Sensitivity * TranslateSpeed,
				Input.GetAxis("2_VerticalR") * Sensitivity * TranslateSpeed,
				//(Mathf.Pow(Input.GetAxis("2_LBumper"), 2) - Mathf.Pow(Input.GetAxis("2_RBumper"), 2)) * Sensitivity * TranslateSpeed,
				Input.GetAxis("2_VerticalL") * Sensitivity * TranslateSpeed
			);
			
			if(Input.GetAxis("2_RBumper") == 1 || Input.GetAxis ("2_LBumper") == 1){
				arm.Clench();
			} else {
				arm.Unclench();
			}
			
			rotateWrist(RotateSpeed * (Input.GetAxis("2_Fire3") - Input.GetAxis("2_Fire2")), -Input.GetAxis("2_VerticalR"), Input.GetAxis("2_HorizontalR"));
		}
	}

	private void translateArm(float x, float y, float z) {
		if(side == Side.left) {
			transform.position = Vector3.Lerp(
				transform.position,
				new Vector3(transform.position.x + x, transform.position.y - y, transform.position.z - z),
				TranslateLerp
			);
		}
		
		if(side == Side.right) {
			transform.position = Vector3.Lerp(
				transform.position,
				new Vector3(transform.position.x + x, transform.position.y - y, transform.position.z - z),
				TranslateLerp
			);
		}
	}

	private void rotateWrist(float x, float y, float z) {
		if(side == Side.left) {
			arm.Wrist.Rotate(arm.Wrist.up, x, Space.World);
			arm.Wrist.Rotate(arm.Wrist.forward, y, Space.World);
			arm.Wrist.Rotate(arm.Wrist.right, -z, Space.World);
		}
		
		if(side == Side.right) {
			arm.Wrist.Rotate(arm.Wrist.up, -x, Space.World);
			arm.Wrist.Rotate(arm.Wrist.forward, z, Space.World);
			arm.Wrist.Rotate(arm.Wrist.right, -y, Space.World);
		}
	}

	private void resetRotateWrist() {

	}

	private void rotateArm(float x, float y, float z) {
		arm.Root.Rotate(arm.Root.up, x, Space.World);
		arm.Root.Rotate(arm.Root.forward, y, Space.World);
		arm.Root.Rotate(arm.Root.right, z, Space.World);
	}
}
