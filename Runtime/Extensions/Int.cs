using System;
using System.Collections.Generic;

namespace NorskaLib.Extensions
{
    public static class IntExtensions
    {
        public static int Square(this int value)
        {
            return value * value;
        }

        public static int Cube(this int value)
        {
            return value * value * value;
        }

        public static bool IsBetween(this int value, int min, int max)
        {
            return value >= min && value <= max;
        }

        public static bool IsBetween(this int value, int min, int max, bool exclusiveMin = false, bool exclusiveMax = false)
        {
            static bool CompareToMin(int value, int min, bool exclusive)
            {
                return exclusive ? value > min : value >= min;
            }

            static bool CompareToMax(int value, int max, bool exclusive)
            {
                return exclusive ? value < max : value <= max;
            }

            return CompareToMin(value, min, exclusiveMin) && CompareToMax(value, max, exclusiveMax);
        }

        public static bool EqualsAny(this int value, int a, int b)
        {
            return value == a || value == b;
        }

        public static bool EqualsAny(this int value, int a, int b, int c)
        {
            return value == a || value == b || value == c;
        }

        public static bool EqualsAny(this int value, int a, int b, int c, int d)
        {
            return value == a || value == b || value == c || value == d;
        }

        public static bool EqualsAny(this int value, int a, int b, int c, int d, int e)
        {
            return value == a || value == b || value == c || value == d || value == e;
        }

        public static bool EqualsAny(this int value, IEnumerable<int> values)
        {
            foreach (var v in values)
                if (value == v)
                    return true;

            return false;
        }

        public static bool IsLess(this int value, int a, int b)
        {
            return value < a && value < b;
        }

        public static bool IsLessOrEqual(this int value, int a, int b)
        {
            return value < a && value < b;
        }

        public static bool IsGreater(this int value, int a, int b)
        {
            return value > a && value > b;
        }

        public static bool IsGreaterOrEqual(this int value, int a, int b)
        {
            return value >= a && value >= b;
        }

        public static bool GetBit(this short mask, int index)
        {
            if (index < 0 || index > 15)
                throw new ArgumentOutOfRangeException(nameof(index), "bit index must be between 0 and 15.");

            return ((mask >> index) & 1) != 0;
        }

        public static bool GetBit(this int mask, int index)
        {
            if (index < 0 || index > 31)
                throw new ArgumentOutOfRangeException(nameof(index), "bit index must be between 0 and 31.");

            return ((mask >> index) & 1) != 0;
        }

        public static short SetBit(this short mask, int index, bool set)
        {
            if (index < 0 || index > 15)
                throw new ArgumentOutOfRangeException(nameof(index), "bitIndex must be between 0 and 15.");

            return set 
                ? (short)(mask | (short)(1 << index)) 
                : (short)(mask & ~(1 << index));
        }

        public static int SetBit(this int mask, int index, bool set)
        {
            if (index < 0 || index > 15)
                throw new ArgumentOutOfRangeException(nameof(index), "bitIndex must be between 0 and 15.");

            return set
                ? (mask | (1 << index))
                : (mask & ~(1 << index));
        }
    }
}