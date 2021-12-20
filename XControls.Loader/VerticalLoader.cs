using System.Windows.Forms;
using XMessageBox;

namespace XControls.Loader
{
    public class VerticalLoader
    {
        private LoaderWindow Window { get; set; }
        private VerticalLoader()
        {
            Window = new LoaderWindow();
        }
        public void ShowLoader() => Window.Show();
        public void Hide()
        {
            Window.Close();
            Cursor.Current = Cursors.Default;// Set cursor as default 
        }
        public static VerticalLoader Show()
        {
            Cursor.Current = Cursors.WaitCursor;// Set cursor as hourglass
            var loader = new VerticalLoader();
            if (System.Windows.Application.Current?.MainWindow?.IsLoaded == true)
            {
                loader.Window.Owner = System.Windows.Application.Current.MainWindow;
            }
            loader.ShowLoader();
            return loader;
        }
    }
}
