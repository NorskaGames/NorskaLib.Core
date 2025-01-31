using NorskaLib.Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NorskaLib.Utilities
{
    public static class PhysicsUtils
    {
        private static RaycastHit[] raycastBuffer = new RaycastHit[256];
        private static Collider[] collidersBuffer = new Collider[256];
        private static HashSet<Collider> collidersHashSet = new HashSet<Collider>();

        /// <returns> Position along the cast trajectory, where sphere was when it produced the hit. </returns>
        public static Vector3 SphereCastPivot(Vector3 origin, Vector3 direction, float hitDistance)
        {
            return origin + direction * hitDistance;
        }

        public static int OverlapSectorNonAlloc(Vector3 origin, float facingAngle, float angularSpan, float innerRadius, float outerRadius, Collider[] results, int layerMask, float density = 10)
        {
            collidersHashSet.Clear();

            var halfSpan = angularSpan * 0.5f;
            var iterationsCount = Mathf.RoundToInt(angularSpan / density) + 1;
            var iterationSpan = angularSpan / iterationsCount;
            var castDistance = outerRadius - innerRadius;
            var lastResultIndex = 0;
            var resultsCount = 0;

            for (int i = 0; i < iterationsCount; i++)
            {
                var angle = (facingAngle - halfSpan) + i * iterationSpan;
                var castDirection = MathUtils.PositionOnCircle3D(angle);
                var castOrigin = innerRadius.ApproximatelyZero()
                    ? origin
                    : MathUtils.PositionOnCircle3D(origin, angle, innerRadius);
                var hitsCount = Physics.RaycastNonAlloc(castOrigin, castDirection, raycastBuffer, castDistance, layerMask);

                //Debug.DrawLine(castOrigin, 
                //    castOrigin + castDirection * castDistance,
                //    hitsCount > 0 ? Color.red : Color.red.WithR(0.5f),
                //    3);

                for (int h = 0; h < hitsCount; h++)
                {
                    var hitInfo = raycastBuffer[h];
                    var isUnique = collidersHashSet.Add(hitInfo.collider);
                    if (!isUnique)
                        continue;
                   
                    results[lastResultIndex] = hitInfo.collider;
                    lastResultIndex++;
                    resultsCount++;
                    if (lastResultIndex >= results.Length)
                        return resultsCount;
                }
            }

            return resultsCount;
        }
    }
}
