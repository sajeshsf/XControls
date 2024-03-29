﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace XControls.Helpers.Extensions
{
    public static class MathHelpers
    {
        public static bool IsPositive(this int value) => value > 0;
        public static bool IsNegative(this int value) => value < 0;
        public static double MakeInRange(this double currentValue, double minValue, double maxValue, Collection<double> possibleValues)
        {
            if (maxValue > minValue)
            {
                var newValue = currentValue;
                if (currentValue < minValue)
                {
                    newValue = minValue;
                }
                else if (currentValue > maxValue)
                {
                    newValue = maxValue;
                }
                if (possibleValues != null && !possibleValues.Contains(newValue))
                {
                    newValue = possibleValues.Aggregate((x, y) => Math.Abs(x - newValue) < Math.Abs(y - newValue) ? x : y);
                }
                return currentValue;
            }
            else
            {
                return -1;
            }
        }
        public static int MakeInRange(this int currentValue, int minValue, int maxValue, Collection<int> possibleValues)
        {
            if (maxValue > minValue)
            {
                var newValue = currentValue;
                if (currentValue < minValue)
                {
                    newValue = minValue;
                }
                else if (currentValue > maxValue)
                {
                    newValue = maxValue;
                }
                if (possibleValues != null && !possibleValues.Contains(newValue))
                {
                    newValue = possibleValues.Aggregate((x, y) => Math.Abs(x - newValue) < Math.Abs(y - newValue) ? x : y);
                }
                return currentValue;
            }
            else
            {
                return -1;
            }
        }
        public static double MakeInRange(this double currentValue, double minValue, double maxValue)
        {
            return maxValue > minValue ? currentValue < minValue ? minValue : currentValue > maxValue ? maxValue : currentValue : -1;
        }
        public static int MakeInRange(this int currentValue, int minValue, int maxValue)
        {
            if (maxValue == minValue)
            {
                return maxValue;
            }
            if (maxValue > minValue)
            {
                if (currentValue <= minValue)
                {
                    return minValue;
                }
                if (currentValue >= maxValue)
                {
                    return maxValue;
                }
                return currentValue;
            }
            return -1;
        }
        public static bool IsWithin(this int value, int minimum, int maximum) => value >= minimum && value <= maximum;
        public static bool IsWithin(this int value, int minimum, int maximum, bool inclusive) =>
            inclusive ? value.IsWithin(minimum, maximum) : value > minimum && value < maximum;
        public static bool IsWithin(this double value, double minimum, double maximum) => value >= minimum && value <= maximum;
        public static bool IsWithin(this double value, bool enabled, double minimum, double maximum)
            => !enabled || value.IsWithin(minimum, maximum);
        public static bool IsWithin(this double value, bool enabled, double minimum, double maximum, bool inclusive)
            => !enabled || value.IsWithin(minimum, maximum, inclusive);
        public static bool IsWithin(this double value, double minimum, double maximum, bool inclusive) =>
            inclusive ? value.IsWithin(minimum, maximum) : value > minimum && value < maximum;
        public static double GetClosest(this Collection<double> list, double number)
        {
            if (!(list ?? throw new ArgumentNullException(nameof(list))).Contains(number))
            {
                return list.Aggregate((x, y) => Math.Abs(x - number) < Math.Abs(y - number) ? x : y);
            }
            else
            {
                return number;
            }
        }
        public static int GetClosest(this Collection<int> list, int number) =>
            !(list ?? throw new ArgumentNullException(nameof(list))).Contains(number) ? list.Aggregate((x, y) => Math.Abs(x - number) < Math.Abs(y - number) ? x : y) : number;
        public static int RoundValueToNearst100(double value)
        {
            int result = (int)Math.Round(value / 100);
            if (value > 0 && result == 0)
            {
                result = 1;
            }
            return result * 100;
        }
        public static double GetPercentage(double currentVal, double maxValue) => Math.Min(Math.Round(100 * (currentVal / maxValue), 2), 100);
        public static (T min, T max) GetMinAndMaxValue<T>(this T[] array) where T : IComparable<T>
        {
            _ = array ?? throw new ArgumentNullException(nameof(array));
            var minimumValue = array[0];
            var maximumValue = array[0];
            Parallel.For(0, array.Length, indx =>
            {
                var pixelValue = array[indx];

                if (pixelValue.CompareTo(minimumValue) < 0)
                    minimumValue = pixelValue;

                if (pixelValue.CompareTo(maximumValue) > 0)
                    maximumValue = pixelValue;

            });
            return (minimumValue, maximumValue);
        }
        public static (double sf, double offset) GetScalefactorOffset(double minPixelValue, double maxPixelValue, double absoluteMaxValue = byte.MaxValue)
        {
            var differnce = (maxPixelValue - minPixelValue);
            return (differnce / absoluteMaxValue, minPixelValue);
        }
        public static (float sf, float offset) GetScalefactorOffset(float minPixelValue, float maxPixelValue, float absoluteMaxValue = byte.MaxValue)
        {
            var differnce = (maxPixelValue - minPixelValue);
            return (differnce / absoluteMaxValue, minPixelValue);
        }
        public static (long sf, long offset) GetScalefactorOffset(long minPixelValue, long maxPixelValue, long absoluteMaxValue = byte.MaxValue)
        {
            var differnce = (maxPixelValue - minPixelValue);
            return (differnce / absoluteMaxValue, minPixelValue);
        }
        public static (int sf, int offset) GetScalefactorOffset(int minPixelValue, int maxPixelValue, int absoluteMaxValue = byte.MaxValue)
        {
            var differnce = (maxPixelValue - minPixelValue);
            return (differnce / absoluteMaxValue, minPixelValue);
        }
        public static (uint sf, uint offset) GetScalefactorOffset(uint minPixelValue, uint maxPixelValue, uint absoluteMaxValue = byte.MaxValue)
        {
            var differnce = (maxPixelValue - minPixelValue);
            return (differnce / absoluteMaxValue, minPixelValue);
        }
        public static (ulong sf, ulong offset) GetScalefactorOffset(ulong minPixelValue, ulong maxPixelValue, ulong absoluteMaxValue = byte.MaxValue)
        {
            var differnce = (maxPixelValue - minPixelValue);
            return (differnce / absoluteMaxValue, minPixelValue);
        }
    }
}
