using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NorskaLib.Utilities
{
    public struct DebugUtils
    {
        public static void DrawPolyline(Vector3[] vertices, bool loop, Color color, float duration = 0)
        {
            if (loop)
                Debug.DrawLine(vertices.Last(), vertices.First(), color, duration);

            for (int i = 1; i < vertices.Length; i++)
                Debug.DrawLine(vertices[i - 1], vertices[i], color, duration);
        }
        public static void DrawPolylineLazy(IEnumerable<Vector3> vertices, bool loop, Color color, float duration = 0)
        {
            var firstSet = false;
            var frstVertex = default(Vector3);
            var prevVertex = default(Vector3);
            foreach (var crntVertex in vertices)
            {
                if (!firstSet)
                {
                    frstVertex = crntVertex;
                    prevVertex = frstVertex;
                    firstSet = true;
                    continue;
                }

                Debug.DrawLine(crntVertex, prevVertex, color, duration);
                prevVertex = crntVertex;
            }

            if (loop)
                Debug.DrawLine(frstVertex, prevVertex, color, duration);
        }

        public static void DrawBox(Vector3 origin, Vector3 halfExtents, Quaternion orientation, Color color, float duration = 0)
        {
            // Calculate the 8 corner points of the box
            Vector3[] corners = new Vector3[8];

            // Bottom vertices
            corners[0] = origin + orientation * new Vector3(-halfExtents.x, -halfExtents.y, -halfExtents.z);
            corners[1] = origin + orientation * new Vector3(halfExtents.x, -halfExtents.y, -halfExtents.z);
            corners[2] = origin + orientation * new Vector3(halfExtents.x, -halfExtents.y, halfExtents.z);
            corners[3] = origin + orientation * new Vector3(-halfExtents.x, -halfExtents.y, halfExtents.z);

            // Top vertices
            corners[4] = origin + orientation * new Vector3(-halfExtents.x, halfExtents.y, -halfExtents.z);
            corners[5] = origin + orientation * new Vector3(halfExtents.x, halfExtents.y, -halfExtents.z);
            corners[6] = origin + orientation * new Vector3(halfExtents.x, halfExtents.y, halfExtents.z);
            corners[7] = origin + orientation * new Vector3(-halfExtents.x, halfExtents.y, halfExtents.z);

            // Draw bottom face
            Debug.DrawLine(corners[0], corners[1], color, duration);
            Debug.DrawLine(corners[1], corners[2], color, duration);
            Debug.DrawLine(corners[2], corners[3], color, duration);
            Debug.DrawLine(corners[3], corners[0], color, duration);

            // Draw top face
            Debug.DrawLine(corners[4], corners[5], color, duration);
            Debug.DrawLine(corners[5], corners[6], color, duration);
            Debug.DrawLine(corners[6], corners[7], color, duration);
            Debug.DrawLine(corners[7], corners[4], color, duration);

            // Draw vertical edges
            Debug.DrawLine(corners[0], corners[4], color, duration);
            Debug.DrawLine(corners[1], corners[5], color, duration);
            Debug.DrawLine(corners[2], corners[6], color, duration);
            Debug.DrawLine(corners[3], corners[7], color, duration);
        }

        public static void DrawCircle(Vector3 origin, float radius, Color color, float duration = 0, int subdivision = 8)
        {
            IEnumerable<Vector3> GetVerticesLazy()
            {
                subdivision = subdivision < 8 ? 8 : subdivision;

                var angularDelta = 360.0f / subdivision;
                for (int i = 0; i < subdivision; i++)
                    yield return MathUtils.PositionOnCircle3D(origin, angularDelta * i, radius);
            }

            DrawPolylineLazy(GetVerticesLazy(), true, color, duration);
        }

        public static void DrawCrossPoint(Vector3 position, Vector3 size, Color color, float duration = 0)
        {
            var halfsize = size * 0.5f;
            Debug.DrawLine(position + Vector3.up * halfsize.y, position + Vector3.down * halfsize.y, color, duration);
            Debug.DrawLine(position + Vector3.left * halfsize.x, position + Vector3.right * halfsize.x, color, duration);
            Debug.DrawLine(position + Vector3.forward * halfsize.z, position + Vector3.back * halfsize.z, color, duration);
        }
        public static void DrawCrossPoint(Vector3 position, float size, Color color, float duration = 0)
        {
            DrawCrossPoint(position, Vector3Utils.Uniform(size), color, duration);
        }

        public static void DrawSector(Vector3 origin, float facing, float span, float radius, Color color, float duration = 0, int subdivision = 2)
        {
            IEnumerable<Vector3> GetVerticesLazy()
            {
                subdivision = subdivision < 2 ? 2 : subdivision;
                var arcSubposDelta = 1.0f / subdivision;
                var angularOrigin = facing - (span * 0.5f);
                var angularLimit = facing + (span * 0.5f);

                yield return origin;
                yield return MathUtils.PositionOnCircle3D(origin, angularOrigin, radius);
                for (int i = 1; i < subdivision; i++)
                {
                    var angle = Mathf.Lerp(angularOrigin, angularLimit, arcSubposDelta * i);
                    yield return MathUtils.PositionOnCircle3D(origin, angle, radius);
                }
                yield return MathUtils.PositionOnCircle3D(origin, angularLimit, radius);
            }

            DrawPolylineLazy(GetVerticesLazy(), true, color, duration);
        }
        public static void DrawSector(Vector3 origin, float facing, float span, float radiusInner, float radiusOuter, Color color, float duration = 0, int subdivision = 2)
        {
            IEnumerable<Vector3> GetVerticesLazy()
            {
                subdivision = subdivision < 2 ? 2 : subdivision;
                var angularDelta = 1.0f / subdivision;
                var angularOrigin = facing - (span * 0.5f);
                var angularLimit = facing + (span * 0.5f);

                for (int i = 0; i <= subdivision; i++)
                {
                    var angle = Mathf.Lerp(angularOrigin, angularLimit, angularDelta * i);
                    yield return MathUtils.PositionOnCircle3D(origin, angle, radiusOuter);
                }
                for (int i = subdivision; i >= 0; i--)
                {
                    var angle = Mathf.Lerp(angularOrigin, angularLimit, angularDelta * i);
                    yield return MathUtils.PositionOnCircle3D(origin, angle, radiusInner);
                }
            }

            DrawPolylineLazy(GetVerticesLazy(), true, color, duration);
        }
    }
}
