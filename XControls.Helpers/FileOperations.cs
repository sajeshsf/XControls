using Microsoft.VisualBasic.FileIO;
using System.IO;

namespace XControls.Helpers
{
    public static class FileOperations
    {
        public static void Copy(this string src, string dst, bool useFilesystem = true)
        {
            if (File.Exists(src))
            {
                var path = Path.GetDirectoryName(dst);
                if (path != null)
                {
                    CreateDirectory(path);
                }
                if (useFilesystem)
                {
                    FileSystem.CopyFile(src, dst, UIOption.AllDialogs, UICancelOption.ThrowException);
                }
                else
                {
                    File.Copy(src, dst, true);
                }
            }
        }
        public static DirectoryInfo CreateDirectory(this string path) => !Directory.Exists(path) ? Directory.CreateDirectory(path) : new DirectoryInfo(path);
        /// <summary>  
        /// Delete File To Recycle Bin  
        /// WARMING: NETWORK FILES DON'T GO TO RECYCLE BIN  
        /// </summary>  
        /// <param name="file"></param>  
        public static void Recycle(this string file)
        {
            if (File.Exists(file))
            {
                FileSystem.DeleteFile(file, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
            }
        }
        /// <summary>  
        /// Delete Path To Recycle Bin  
        /// WARMING: NETWORK PATHS DON'T GO TO RECYCLE BIN  
        /// </summary>  
        /// <param name="path"></param>  
        public static void RecycleDirectory(this string path)
        {
            if (Directory.Exists(path))
            {
                FileSystem.DeleteDirectory(path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
            }
        }
        public static void CopyDirectory(this string src, string dst)
        {
            if (Directory.Exists(src))
            {
                CreateDirectory(dst);
                FileSystem.CopyFile(src, dst, UIOption.AllDialogs, UICancelOption.ThrowException);
            }
        }
        public static DriveInfo GetDriveInfo(this string path) => FileSystem.GetDriveInfo(Path.GetPathRoot(path));
        public static (long TotalSize, long UsedSpace, long AvailableFreeSpace) GetDriveSpaceInfo(string path)
        {
            var t = GetDriveInfo(path);
            return (t.TotalSize, t.TotalSize - t.AvailableFreeSpace, t.AvailableFreeSpace);
        }
        public static string ChangeExtention(this string filePath, string extention) => $"{Path.GetDirectoryName(filePath)}\\{Path.GetFileNameWithoutExtension(filePath)}{extention}";
        public static string InsertDirectory(this string filePath, string newFolder)
        {
            var v = Path.GetDirectoryName(filePath);
            if (v == null)
            {
                return Path.Combine(newFolder, Path.GetFileName(filePath));
            }
            return Path.Combine(v, newFolder, Path.GetFileName(filePath));
        }
    }
}
