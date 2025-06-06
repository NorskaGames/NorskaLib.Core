using NorskaLib.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NorskaLib.Extensions
{
    public static class IListExtensions
    {
        public static bool IndexIsValid(this IList list, int index)
        {
            return index >= 0 && index <= list.Count - 1;
        }

        public static List<int> GetIndexesList(this IList list)
        {
            return MathUtils.GetRangeList(0, list.Count - 1);
        }

        public static int[] GetIndexesArray(this IList list)
        {
            return MathUtils.GetRangeArray(0, list.Count - 1);
        }

        public static void Shuffle<T>(this IList<T> instance)
        {
            int n = instance.Count;
            while (n > 1)
            {
                n--;
                var k = UnityEngine.Random.Range(0, n + 1);

                T buffer = instance[k];
                instance[k] = instance[n];
                instance[n] = buffer;
            }
        }

        public static IList<T> ShuffledCopy<T>(this IList<T> instance)
        {
            var copy = new List<T>(instance);

            int n = copy.Count;
            while (n > 1)
            {
                n--;
                var k = UnityEngine.Random.Range(0, n + 1);

                T buffer = copy[k];
                copy[k] = copy[n];
                copy[n] = buffer;
            }

            return copy;
        }

        public static T Random<T>(this IList<T> list)
        {
            var index = UnityEngine.Random.Range(0, list.Count);
            return list[index];
        }
        public static T Random<T>(this IList<T> list, out int index)
        {
            index = UnityEngine.Random.Range(0, list.Count);
            return list[index];
        }
        public static T Random<T>(this IList<T> list, System.Random randomizer)
        {
            var index = randomizer.Next(0, list.Count);
            return list[index];
        }
        public static T Random<T>(this IList<T> list, System.Random randomizer, out int index)
        {
            index = randomizer.Next(0, list.Count);
            return list[index];
        }

        public static bool Contains<T>(this IList<T> list, T element, out int index) where T : class
        {
            for (int i = 0; i < list.Count; i++)
                if (element == list[i])
                {
                    index = i;
                    return true;
                }

            index = -1;
            return false;
        }

        /// <returns> '-1' if no match found. </returns>
        public static int IndexOf<T>(this IList<T> list, T element) where T : class
        {
            for (int i = 0; i < list.Count; i++)
                if (element == list[i])
                    return i;

            return -1;
        }
        /// <returns> '-1' if no match found. </returns>
        public static int IndexOf<T>(this IList<T> list, Func<T, bool> query)
        {
            for (int i = 0; i < list.Count; i++)
                if (query(list[i]))
                    return i;

            return -1;
        }
        /// <returns> '-1' if no match found. </returns>
        public static int IndexOf<T>(this IList<T> list, int id) where T : IIntegerIdProvider
        {
            for (int i = 0; i < list.Count; i++)
                if (list[i].Id == id)
                    return i;

            return -1;
        }
        /// <returns> '-1' if no match found. </returns>
        public static int IndexOf<T>(this IList<T> list, string id) where T : IStringIdProvider
        {
            for (int i = 0; i < list.Count; i++)
                if (list[i].Id == id)
                    return i;

            return -1;
        }

        public static bool TryGet<T>(this IList<T> list, Func<T, bool> query, out T result)
        {
            for (int i = 0; i < list.Count; i++)
            {
                result = list[i];
                if (query(result))
                    return true;
            }

            result = default(T);
            return false;
        }

        /// <returns> TRUE - if result is still inside collection range. </returns>
        /// <exception cref="ArgumentOutOfRangeException"> Given 'index' was out of collection range. </exception>
        public static bool TryGetNextIndex<T>(this IList<T> list, int index, bool loop, out int nextIndex)
        {
            if (!index.IsBetween(0, list.Count))
                throw new ArgumentOutOfRangeException($"Index '{index}' is out of range '{0}''{list.Count}'");

            nextIndex = loop
                ? index + 1 >= list.Count
                    ? 0
                    : index + 1
                : index + 1;

            return nextIndex < list.Count;
        }

        /// <returns> TRUE - if result is still inside collection range. </returns>
        /// <exception cref="ArgumentOutOfRangeException"> Given 'index' was out of collection range. </exception>
        public static bool TryGetPrevIndex<T>(this IList<T> list, int index, bool loop, out int prevIndex)
        {
            if (!index.IsBetween(0, list.Count))
                throw new ArgumentOutOfRangeException($"Index '{index}' is out of range '{0}''{list.Count}'");

            prevIndex = loop
                ? index - 1 < 0
                    ? list.Count - 1
                    : index - 1
                : index - 1;

            return prevIndex >= 0;
        }

        /// <returns> FALSE - If 'crnt' is the last element in the list and loop is set to false. </returns>
        /// <exception cref="ArgumentOutOfRangeException"> - if 'crnt' is not present in the list.</exception>
        public static bool TryGetNext<T>(this IList<T> list, T crnt, out T next, bool loop = false)
        {
            var crntIndex = list.IndexOf(crnt);
            if (crntIndex == -1)
                throw new ArgumentOutOfRangeException("Element is not present in the collection!");

            var nextIndex = (crntIndex + 1) % list.Count;
            var valid = loop || nextIndex != 0;
            next = valid ? list[nextIndex] : default;
            return valid;
        }

        /// <returns> FALSE - If 'crnt' is the first element in the list and loop is set to false. </returns>
        /// <exception cref="ArgumentOutOfRangeException"> - if 'crnt' is not present in the list.</exception>
        public static bool TryGetPrev<T>(this IList<T> list, T crnt, out T next, bool loop = false)
        {
            var crntIndex = list.IndexOf(crnt);
            if (crntIndex == -1)
                throw new ArgumentOutOfRangeException("Element is not present in the collection!");

            var nextIndex = (crntIndex - 1) % list.Count;
            var valid = loop || nextIndex != 0;
            next = valid ? list[nextIndex] : default;
            return valid;
        }
    }
}