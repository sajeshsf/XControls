using System.IO;
using System.Text;

namespace XWorkers.Spreadsheet
{
    public abstract class FileWriter
    {
        public string FileName { get; set; }
        public Encoding Encoding { get; set; }
        public void ClearFile() => _ = 1;// FileOperations.FileRecycle(FileName);
        protected void WriteLineToFile(string content)
        {
            using (var file = new StreamWriter(FileName, true))
            {
                file.WriteLine(content);
            }
        }
        protected void WriteLineToFile(string content, Encoding encoding)
        {
            using (var file = new StreamWriter(FileName, true, encoding))
            {
                file.WriteLine(content);
            }
        }
    }
}
