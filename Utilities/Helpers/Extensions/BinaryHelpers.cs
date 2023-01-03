using System.Text;

namespace XControls.Utilities.Helpers.Extensions
{
    public static class BinaryHelpers
    {
        public static byte[] HexToBin(this string hex)
        {
            var result = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                result[i / 2] = byte.Parse(hex.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);
            }
            return result;
        }
        public static string BytesToBinaryString(this byte[] bytes)
        {
            var ba = new System.Collections.BitArray(bytes);
            var sb = new StringBuilder();
            for (int i = 0; i < ba.Length; i++)
            {
                int byteStart = i / 8 * 8;
                int bit = 7 - i % 8;
                sb.Append(ba[byteStart + bit] ? '1' : '0');
                if (i % 8 == 7)
                    sb.Append(' ');
            }
            return sb.ToString();
        }
    }
}
