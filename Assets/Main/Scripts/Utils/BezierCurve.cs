using UnityEngine;
using System;
public class BezierCurve 
{
    public static Vector3 GetPositionFromCurve(Vector3 start, Vector3 middle, Vector3 end, float t)
	{
		Vector3 position = (1.0f - t) * (1.0f - t) * start + 2.0f * (1.0f - t) * t * middle + t * t * end;
        
        return position;
	}
	public static Vector3 GetTangentFromCurve(Vector3 start, Vector3 middle, Vector3 end, float t)
	{
		Vector3 tangent = 2.0f * (1.0f - t) * (middle - start) + 2.0f * t * (end - middle);
		
		return tangent;
	}
	[Serializable]
	public struct Curve {
		public Vector3 start;
		public Vector3 middle;
		public Vector3 end;
	}
}