using UnityEngine;

namespace NorskaLib.Utilities
{
    public struct Vector3Utils
    {
        public static Vector3 ComponentMult (Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static Vector3 ComponentMult(Vector3 a, Vector3Int b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static Vector3 ComponentMult(Vector3Int a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static Vector3Int ComponentMult(Vector3Int a, Vector3Int b)
        {
            return new Vector3Int(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static Vector3 ComponentDiv(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
        }

        public static bool Approximately(Vector3 a, Vector3 b)
        {
            return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y) && Mathf.Approximately(a.z, b.z);
        }

        public static Vector3 Uniform(float value)
        {
            return new Vector3(value, value, value);
        }

        /// <returns> A position in the middle between given positions. </returns>
        public static Vector3 Center(Vector3 a, Vector3 b)
        {
            return (a + b) / 2;
        }

        public static Vector3Int RoundToInt(Vector3 value)
        {
            return new Vector3Int(
                Mathf.RoundToInt(value.x),
                Mathf.RoundToInt(value.y),
                Mathf.RoundToInt(value.z));
        }

        public static Vector3 Perpendicular(Vector3 a, Vector3 b, Vector3 referenceAxis)
        {
            Vector3 direction = a - b;
            Vector3 arbitraryVector = Vector3.Cross(direction, referenceAxis);
            return -Vector3.Cross(direction, arbitraryVector).normalized;
        }

        public static Vector3 InverseLerp(Vector3 a, Vector3 b, Vector3 position)
        {
            return new Vector3(
                Mathf.InverseLerp(a.x, b.x, position.x),
                Mathf.InverseLerp(a.y, b.y, position.y),
                Mathf.InverseLerp(a.z, b.z, position.z));
        }

        public static Vector3 Clamp01(Vector3 value)
            => Clamp(value, Uniform(0), Uniform(1));
        public static Vector3 Clamp(Vector3 value, float min, float max) 
            => Clamp(value, Uniform(min), Uniform(max));
        public static Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max) 
            => new Vector3(
                x: Mathf.Clamp(value.x, min.x, max.x),
                y: Mathf.Clamp(value.y, min.y, max.y),
                z: Mathf.Clamp(value.z, min.z, max.z));
    }
}
