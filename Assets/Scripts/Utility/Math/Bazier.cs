using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public class Bazier
    {
        public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            var oneMinusT = 1.0f - t;
            return oneMinusT * oneMinusT * oneMinusT * p0 +
                   3.0f * oneMinusT * oneMinusT * t * p1 +
                   3.0f * oneMinusT * t * t * p2 +
                   t * t * t * p3;
        }

        public static Vector3 GetSloap(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            return -3.0f * (t - 1.0f) * (t - 1.0f) * p0 +
                3.0f * (3.0f * t -1.0f) * (t - 1.0f) * p1 +
                -3.0f * (3.0f * t - 2.0f) * t * p2 +
                3.0f * t * t * p3;
        }
    }
}

