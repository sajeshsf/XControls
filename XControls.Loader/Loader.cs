using System.Windows.Forms;
using XMessageBox;

namespace XControls.Loader
{
    public class Loader
    {
        private LoaderWindow Window { get; set; }
        private Loader()
        {
            Window = new LoaderWindow();
        }
        public void ShowLoader() => Window.Show();
        public void Hide()
        {
            Window.Close();
            Cursor.Current = Cursors.Default;// Set cursor as default 
        }
        public static Loader Show()
        {
            Cursor.Current = Cursors.WaitCursor;// Set cursor as hourglass
            var loader = new Loader();
            if (System.Windows.Application.Current?.MainWindow?.IsLoaded == true)
            {
                loader.Window.Owner = System.Windows.Application.Current.MainWindow;
            }
            loader.ShowLoader();
            return loader;
        }
    }
}
