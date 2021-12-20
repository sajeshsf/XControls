using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XControls.Helpers;

namespace XWorkers.Spreadsheet
{
    public class TsvWriter : FileWriter
    {
        public TsvWriter(string fileName) => FileName = fileName;
        public TsvWriter(string fileName, DataTable tsvTable)
        {
            FileName = fileName;
            WriteToFile(tsvTable);
        }
        public Task WriteToFile(DataTable tsvTable, string separator = StringConstants.TABSPACE) => Task.Run(() =>
        {
            ClearFile();
            foreach (DataRow row in tsvTable.Rows)
            {
                var tempString = new StringBuilder();
                foreach (DataColumn column in tsvTable.Columns)
                {
                    tempString.Append(row[column]);
                    tempString.Append(separator);
                }
                WriteLineToFile(tempString.ToString());
            }
            //string.Join(Environment.NewLine, tsvTable.Rows.OfType<DataRow>().Select(x => string.Join(StringConstants.TABSPACE, x.ItemArray)));
        });
    }
}
