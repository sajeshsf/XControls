using System;

namespace Xcross.Lib.Common.Utilities.FileHandling.Win32
{
    public static class Win32FileApi
    {
        public static bool WriteFileWinAPI1(this byte[] ByteBuf, string filename)
        {
            _ = ByteBuf ?? throw new ArgumentNullException(nameof(ByteBuf));
            _ = filename ?? throw new ArgumentNullException(nameof(filename));
            try
            {
                int bytes;
                bool retval;
                using (var WFIO = new WinFileIO(ByteBuf))
                {
                    WFIO.OpenForWriting(filename);
                    bytes = WFIO.Write(ByteBuf.Length);
                    retval = WFIO.Close();
                }
                return bytes == ByteBuf.Length;
            }
            catch (Exception ex)
            {
                //LogHelper.Logger.Error($"Failed to write File - {filename}", ex);
                throw;
            }
        }
        public static bool WriteFileWinAPI2(this byte[] ByteBuf, string filename)
        {
            _ = ByteBuf ?? throw new ArgumentNullException(nameof(ByteBuf));
            _ = filename ?? throw new ArgumentNullException(nameof(filename));
            try
            {
                int bytes;
                bool retval;
                using (var WFIO = new WinFileIO(ByteBuf))
                {
                    WFIO.OpenForWriting(filename);
                    bytes = WFIO.WriteBlocks(ByteBuf.Length);
                    retval = WFIO.Close();
                }
                return bytes == ByteBuf.Length && retval;
            }
            catch (Exception ex)
            {
                //LogHelper.Logger.Error($"Failed to write File - {filename}", ex);
                throw;
            }
        }
    }
}
