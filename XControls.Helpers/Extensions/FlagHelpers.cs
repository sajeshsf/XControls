using System;

namespace XControls.Helpers.Extensions
{
    public static class FlagHelpers
    {
        public static T SetFlag<T>(this Enum value, T flag, bool set)
        {
            if (value == null || flag == null)
            {
                return flag;
            }
            var underlyingType = Enum.GetUnderlyingType(value.GetType());
            // note: AsInt mean: math integer vs enum (not the c# int type)
            dynamic valueAsInt = Convert.ChangeType(value, underlyingType);
            dynamic flagAsInt = Convert.ChangeType(flag, underlyingType);
            if (set)
            {
                valueAsInt |= flagAsInt;
            }
            else
            {
                valueAsInt &= ~flagAsInt;
            }
            return (T)valueAsInt;
        }
        public static int SetBit(int position, int currentValue, bool onOffFlag)
        {
            int mask = 1 << position - 1;
            if (onOffFlag)
            {
                currentValue |= mask;
            }
            else
            {
                currentValue &= ~mask;
            }
            return currentValue;
        }
        public static bool GetBit(int position, short currentValue)
        {
            int mask = 1 << position - 1;
            return (currentValue & mask) > 0;// (currentValue & mask) >> (position - 1) > 0;
        }
    }
}
