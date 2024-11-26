using NorskaLib.Utilities;
using UnityEngine;

namespace NorskaLib.Extensions
{
    public static class RectTransformExtensions
    {
        public static void DestroyChildren(this RectTransform instance)
        {
            var childCount = instance.childCount;
            for (var i = childCount - 1; i > -1; i--)
            {
                Object.Destroy(instance.GetChild(i).gameObject);
            }
        }

        /// <returns> A vector from (0;0) to (1;1) where (0;0) matches top-left corner of the RectTransform.rect. </returns>
        public static Vector2 NormalizedRectPostion(this RectTransform instance, Vector2 localPosition)
        {
            var localMinPosition = -1 * instance.pivot * instance.rect.size;
            var localMaxPosition = (Vector2.one - instance.pivot) * instance.rect.size;
            return Vector2Utils.InverseLerp(localMinPosition, localMaxPosition, localPosition);
        }

        public static void Mimic(this RectTransform thisTransform, RectTransform sourceTransform)
        {
            thisTransform.anchoredPosition = sourceTransform.anchoredPosition;
            thisTransform.anchorMax = sourceTransform.anchorMax;
            thisTransform.anchorMin = sourceTransform.anchorMin;
            thisTransform.pivot = sourceTransform.pivot;
            thisTransform.sizeDelta = sourceTransform.sizeDelta;

            thisTransform.localPosition = sourceTransform.localPosition;
            thisTransform.localRotation = sourceTransform.localRotation;
            thisTransform.localScale = sourceTransform.localScale;
        }
    }
}