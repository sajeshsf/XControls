using Microsoft.Win32;
using System.IO;
using System.Linq;

namespace XControls.Helpers
{
    public class DialogHelper
    {
        public static bool SaveFileDialog(string filter, out string path, out int filterIndex) => SaveFileDialog(new string[] { filter }, out path, out filterIndex);
        public static bool SaveFileDialog(string[] filters, out string fileName, out int filterIndex, string initialDirectory = "")
        {
            fileName = string.Empty;
            filterIndex = 0;
            var fileDialog = new SaveFileDialog
            {
                InitialDirectory = initialDirectory == "" ? string.Empty : new DirectoryInfo(initialDirectory)?.FullName,
                Filter = FileExtensionsAndFilters.CombineFilter(filters),
                AddExtension = true,
            };
            if (fileDialog.ShowDialog() != true)
            {
                return false;
            }
            filterIndex = fileDialog.FilterIndex - 1;
            var fileName1 = fileName = Path.GetFullPath(fileDialog.FileName);
            string[] extensions = GetExtensionsFromFilter(filters[filterIndex]);
            if (extensions.Where(item => fileName1.EndsWith(item)).ToArray().Length == 0)
            {
                fileName = fileName.ChangeExtention(extensions[0]);
            }
            return true;
        }
        private static string[] GetExtensionsFromFilter(string filter)
        {
            var tmp1 = filter.Split('|');
            var tmp2 = tmp1[1].Split(';');
            var extensions = tmp2.Select(item =>
            {
                var tmp3 = item.Split('*');
                return tmp3[1].ToLower();
            }).ToArray();
            return extensions;
        }
        public static bool SaveFileDialog(string filter, string fileExtension, out string path, string initialDirectory = "")
        {
            path = string.Empty;
            var fileDialog = new SaveFileDialog
            {
                InitialDirectory = initialDirectory == "" ? string.Empty : new DirectoryInfo(initialDirectory)?.FullName,
                Filter = filter,
                AddExtension = true,
            };
            if (fileDialog.ShowDialog() != true)
            {
                return false;
            }
            path = fileDialog.FileName;
            if (!Path.GetExtension(fileDialog.FileName).Equals(fileExtension))
            {
                path = fileDialog.FileName + fileExtension;
            }
            return true;
        }
        public static bool OpenFileDialog(string filter, string extension, out string path, string initialDirectory = "") =>
            OpenFileDialog(new string[] { filter }, out path, initialDirectory) && path.EndsWith(extension);
        public static bool OpenFileDialog(string filter, out string path, string initialDirectory = "") => OpenFileDialog(new string[] { filter }, out path, initialDirectory);
        public static bool OpenFileDialog(string[] filters, out string path, string initialDirectory = "")
        {
            path = string.Empty;
            var fileDialog = new OpenFileDialog()
            {
                InitialDirectory = initialDirectory == "" ? string.Empty : new DirectoryInfo(initialDirectory)?.FullName,
                Filter = FileExtensionsAndFilters.CombineFilter(filters)
            };
            if (fileDialog.ShowDialog() != true)
            {
                return false;
            }
            path = fileDialog.FileName;
            return true;
        }
    }

}
