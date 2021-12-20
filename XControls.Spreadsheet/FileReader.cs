using System;
using System.Collections.Generic;
using System.IO;

namespace XWorkers.Spreadsheet
{
    public abstract class FileReader
    {
        public string FileName { get; set; }
        public bool FileExists() => File.Exists(FileName);
        protected Dictionary<string, object> ReadAndParse(string[] separator)
        {
            var parameterList = new Dictionary<string, object>();
            using (var file = new StreamReader(FileName))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string[] result = line.Split(separator, 2, StringSplitOptions.RemoveEmptyEntries);
                    parameterList.Add(result[0], result[1]);
                }
            }
            return parameterList;
        }
    }
}
