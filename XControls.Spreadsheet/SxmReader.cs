using System.Collections.Generic;
using XControls.Helpers;

namespace XWorkers.Spreadsheet
{
    public class SxmReader : FileReader
    {
        public SxmReader(string fileName) => FileName = fileName;
        public Dictionary<string, object> Read() => ReadAndParse(new string[] { StringConstants.SPACE });
    }
}
