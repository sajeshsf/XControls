using System.Collections.Generic;
using XControls.Helpers;

namespace XWorkers.Spreadsheet
{
    public class SxmReader : FileReader
    {
        public SxmReader(string fileName) : base(fileName) => _ = 1;
        public Dictionary<string, object> Read() => ReadAndParse(new string[] { StringConstants.SPACE });
    }
}
