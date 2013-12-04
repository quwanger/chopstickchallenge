using UnityEngine;
using System.Collections;

public class CC_Camera : CC_Behaviour
{

	public float defaultDistance = 10.0f;

	private float distance;

	private Camera camera;
	private DepthOfFieldScatter depthEffect;
	private Vector3 target;

	public float edgeBoundAngle = 0.0f;

	public GameObject obj1;
	public GameObject obj2;

	public Vector3 position {
		get {
			return transform.position;
		}
	}

	// Use this for initialization
	void Start() {
		camera = GetComponent<Camera>();
		depthEffect = GetComponent<DepthOfFieldScatter>();
	}

	// Update is called once per frame
	void Update() {
		// Get target
		target = Vector3.Lerp(obj1.transform.position, obj2.transform.position, 0.5f);
		Vector3 fromCamera = (position - target).normalized;
		target = target + fromCamera * defaultDistance;
		//transform.position = target;

		// Find Distance needed
		float xDist = Mathf.Abs(obj1.transform.position.x - obj2.transform.position.x);
		xDist = xDist / (2 * Mathf.Atan((getHorizontalFOV() - edgeBoundAngle * Mathf.Deg2Rad) / 2));

		float zDist = Mathf.Abs(obj1.transform.position.y - obj2.transform.position.y);
		zDist = zDist / (2 * Mathf.Atan((camera.fov * Mathf.Deg2Rad - edgeBoundAngle * Mathf.Deg2Rad) / 2));

		distance = Mathf.Max(xDist, zDist);

		//if (!GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), obj1.collider.bounds)) // Check if object is within bounds of frustum

		transform.position = Vector3.Lerp(transform.position, target - Vector3.back * distance + Vector3.up * distance/5.0f, 0.05f);

		transform.LookAt(target);

		// Depth of Field
		depthEffect.focalLength = (target - position).magnitude;
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, target);

		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, obj1.transform.position);
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, obj2.transform.position);

		Gizmos.color = Color.red;
		Gizmos.DrawCube(target, new Vector3(0.1f, 0.1f, 0.1f));
	}

	private float getHorizontalFOV() {
		return 2 * Mathf.Atan(Mathf.Tan(camera.fieldOfView * Mathf.Deg2Rad / 2) * camera.aspect * Mathf.Rad2Deg);
		//float hFOV = hFOVInRads * Mathf.Rad2Deg;
	}
}