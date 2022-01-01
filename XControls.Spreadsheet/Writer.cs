using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XControls.Helpers;
using static XControls.Helpers.StringConstants;

namespace XWorkers.Spreadsheet
{
    public static class Writer
    {
        public static Task WriteToFile(this string content, string fileName,
         string? header = null, Encoding? encoding = null)
        {
            return Task.Run(async () =>
            {
                using var file = encoding == null ? new StreamWriter(fileName, true) : new StreamWriter(fileName, true, encoding);
                if (header != null)
                {
                    await file.WriteAsync(header);
                }
                await file.WriteAsync(content);
            });
        }
        public static Task WriteToFile<T>(this IEnumerable<T> param, Func<T, string> selector, string fileName,
            string? header = null, Encoding? encoding = null, string? newLine = null)
        {
            var separator = newLine ?? Environment.NewLine;
            var content = string.Join(separator, param.Select(selector));
            return WriteToFile(content, fileName, header, encoding);
        }
        public static Task WriteToFile(this DataTable tsvTable, string fileName,
            string? header = null, string separator = TABSPACE, Encoding? encoding = null, string? newLine = null)
        {
            string selector(DataRow x) => string.Join(separator, x.ItemArray);
            var enumerable = tsvTable.Rows.OfType<DataRow>();
            return enumerable.WriteToFile(selector, fileName, header, encoding, newLine);
        }
        public static Task WriteTsv(this DataTable table, string fileName, string? header = null, Encoding? encoding = null,
            string? newLine = null) => table.WriteToFile(fileName, header, TABSPACE, encoding, newLine);
        public static Task WriteCsv(this DataTable table, string fileName, string? header = null, Encoding? encoding = null,
            string? newLine = null) => table.WriteToFile(fileName, header, COMA, encoding, newLine);
        public static Task WriteSxm<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> parameters, string fileName,
           string separator = SPACE, string? header = null, Encoding? encoding = null)
        {
            string selector(KeyValuePair<TKey, TValue> param)
            {
                var tempString = new StringBuilder();
                tempString.Append(param.Key?.ToString());
                tempString.Append(separator);
                tempString.Append(param.Value?.ToString());
                return tempString.ToString();
            }
            return parameters.WriteToFile(selector, fileName, header, encoding);
        }
    }
}
