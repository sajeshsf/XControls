using System.Linq;

namespace XControls.Utilities.Helpers
{
    public class FileExtensionsAndFilters
    {
        public const string JPEG8FILTER = "JPEG (*.jpeg)|*.jpeg";
        public const string PNG8FILTER = "PNG (*.png)|*.png";
        public const string TIFF8FILTER = "8 - bit TIFF(*.tif)|*.tif";
        public const string TIFF16FILTER = "16 - bit TIFF(*.tif)|*.tif";
        public const string BMP8FILTER = "8-bit Bitmap (*.bmp)|*.bmp";
        public const string BMP24FILTER = "24-bit Bitmap (*.bmp)|*.bmp";
        public const string RAW8FILTER = "8-bit RAW (*.raw)|*.raw";
        public const string RAW16FILTER = "16-bit RAW (*.raw)|*.raw";
        public const string RAW32FILTER = "32-bit RAW (*.raw)|*.raw";
        public const string CCFFILEFILTER = "CCF File (*.ccf)|*.ccf";
        public const string SXMFILE_FILTER = "SXM File (*.sxm)|*.sxm";
        public const string TSVFILE_FILTER = "TSV File (*.tsv)|*.tsv";
        public const string DARKDATAFILTER = "DRK File (*.drk)|*.drk";
        public const string BRIGHTDATAFILTER = "BRT File (*.brt)|*.brt";
        public const string BADPIXELMAPFILTER = "CSV File (*.csv)|*.csv";
        public const string APPLICATIONPATHFILTER = "Application File (*.exe)|*.exe";
        public const string TEXTFILEFILTER = "Text File (*.txt)|*.txt";
        public const string JSONFILEFILTER = "(*.json)|*.json";
        public const string BINARYFILEFILTER = "(*.bin)|*.bin|*.BIN|(*.BIN)";

        public const string JPEGFORMAT = ".jpeg";
        public const string PNGFORMAT = ".png";
        public const string TIFFFORMAT = ".tif";
        public const string BMPFORMAT = ".bmp";
        public const string RAWFORMAT = ".raw";
        public const string CCFFILEFORMAT = ".ccf";
        public const string SXMFORMAT = ".sxm";
        public const string TSVFORMAT = ".tsv";
        public const string DARKDATAFORMAT = ".drk";
        public const string BRIGHTDATAFORMAT = ".brt";
        public const string BADPIXELMAPFORMAT = ".csv";
        public const string APPLICATIONEXTENSION = ".exe";
        public const string TEXTFILEXTENSION = ".txt";
        public const string JSONFILEFORMAT = ".json";
        public const string BINARYFORMATLOWERCASE = ".bin";
        public const string BINARYFORMATUPPERCASE = ".BIN";

        private FileExtensionsAndFilters()
        {

        }
        public static string CombineFilter(string a, string b) => a + "|" + b;
        public static string CombineFilter(string[] a)
        {
            var result = string.Empty;
            for (int i = 0; i < a.Length; i++)
            {
                result += a[i];
                if (i < a.Length - 1)
                {
                    result += "|";
                }
            }
            return result;
        }
        public static string[] GetExtensionsFromFilter(string filter)
        {
            var tmp1 = filter.Split('|');
            var tmp2 = tmp1[1].Split(';');
            var extensions = tmp2.Select(item =>
            {
                var tmp3 = item.Split('*');
                return tmp3[1].ToLower();
            }).ToArray();
            return extensions;
        }
    }
}