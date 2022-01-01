﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XControls.Helpers;

namespace XWorkers.Spreadsheet
{
    public abstract class FileReader
    {
        public string FileName { get; set; }
        public FileReader(string fileName) => FileName = fileName;
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
