namespace XWorkers.Spreadsheet
{
    public class TsvReader : FileReader
    {
        public TsvReader(string fileName) => FileName = fileName;
    }
}
