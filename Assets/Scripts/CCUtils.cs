using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class CCUtils
{
	public static Vector3 FlattenVectorX(Vector3 vec) {
		return new Vector3(0.0f, vec.y, vec.z);
	}

	public static Vector3 FlattenVectorY(Vector3 vec) {
		return new Vector3(vec.x, 0.0f, vec.z);
	}

	public static Vector3 FlattenVectorZ(Vector3 vec) {
		return new Vector3(vec.x, vec.y, 0.0f);
	}

	public static Vector3 AnglesBetween(Vector3 vec1, Vector3 vec2) {
		Vector3 angle = new Vector3();

		angle.x = Vector3.Angle(CCUtils.FlattenVectorX(vec1).normalized, CCUtils.FlattenVectorX(vec2).normalized);
		angle.y = Vector3.Angle(CCUtils.FlattenVectorY(vec1).normalized, CCUtils.FlattenVectorY(vec2).normalized);
		angle.z = Vector3.Angle(CCUtils.FlattenVectorZ(vec1).normalized, CCUtils.FlattenVectorZ(vec2).normalized);

		return angle;
	}
}