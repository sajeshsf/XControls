using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XControls.Helpers;

namespace XWorkers.Spreadsheet
{
    public class SxmWriter : FileWriter
    {
        public SxmWriter(string fileName) => FileName = fileName;
        public SxmWriter(string fileName, Encoding encoding)
        {
            FileName = fileName;
            Encoding = encoding;
        }
        public SxmWriter(string fileName, Dictionary<string, object> sxmParameters)
        {
            FileName = fileName;
            WriteToFile(sxmParameters);
        }
        public Task WriteToFile<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> parameters,
            string separator = StringConstants.SPACE) => Task.Run(() =>
        {
            ClearFile();
            foreach (var param in parameters)
            {
                var tempString = new StringBuilder();
                tempString.Append(param.Key?.ToString());
                tempString.Append(separator);
                tempString.Append(param.Value?.ToString());
                WriteLineToFile(tempString.ToString(), Encoding);
            }
            string.Join(Environment.NewLine, parameters);
        });
    }
}
