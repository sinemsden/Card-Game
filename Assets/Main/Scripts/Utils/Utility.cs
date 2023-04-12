using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    public static float CalculateAngle(Vector3 from, Vector3 to)
    {
        return Quaternion.FromToRotation(Vector3.forward, to - from).eulerAngles.y;
    }
    public struct Ease
    {
        //0-1
        public static float Linear(float t)
        {
            return t;
        }
        //0-1
        public static float EaseInQuad(float t)
        {
            return t * t;
        }
        //0-1
        public static float EaseOutQuad(float t)
        {
            return t * (2 - t);
        }
    }
}
