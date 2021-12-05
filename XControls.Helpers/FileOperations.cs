using Microsoft.VisualBasic.FileIO;
using System.IO;

namespace XControls.Helpers
{
    public static class FileOperations
    {
        public static DirectoryInfo CreateDirectory(string path) => !Directory.Exists(path) ? Directory.CreateDirectory(path) : new DirectoryInfo(path);
        public static void CopyFile(string src, string dst)
        {
            if (File.Exists(src))
            {
                CreateDirectory(Path.GetDirectoryName(dst));
                File.Copy(src, dst, true);
            }
        }
        /// <summary>  
        /// Delete File To Recycle Bin  
        /// WARMING: NETWORK FILES DON'T GO TO RECYCLE BIN  
        /// </summary>  
        /// <param name="file"></param>  
        public static void FileRecycle(string file)
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
        public static void DirectoryRecycle(string path)
        {
            if (Directory.Exists(path))
            {
                FileSystem.DeleteDirectory(path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
            }
        }
        public static void FileCopy(string src, string dst)
        {
            if (File.Exists(src))
            {
                CreateDirectory(Path.GetDirectoryName(dst));
                FileSystem.CopyFile(src, dst, UIOption.AllDialogs, UICancelOption.ThrowException);
            }
        }
        public static void DirectoryCopy(string src, string dst)
        {
            if (Directory.Exists(src))
            {
                CreateDirectory(dst);
                FileSystem.CopyFile(src, dst, UIOption.AllDialogs, UICancelOption.ThrowException);
            }
        }
        public static DriveInfo GetDriveInfo(string path) => FileSystem.GetDriveInfo(Path.GetPathRoot(path));
        public static (long, long, long) GetDriveSpaceInfo(string path)
        {
            var t = GetDriveInfo(path);
            return (t.TotalSize, t.TotalSize - t.AvailableFreeSpace, t.AvailableFreeSpace);
        }
        public static string ChangeExtention(this string filePath, string extention) => $"{Path.GetDirectoryName(filePath)}\\{Path.GetFileNameWithoutExtension(filePath)}{extention}";
        public static string InsertDirectory(this string filePath, string newFolder) => Path.Combine(Path.GetDirectoryName(filePath), newFolder, Path.GetFileName(filePath));
    }
}
