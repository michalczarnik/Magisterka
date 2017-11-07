using System;
using System.Windows.Forms;

namespace CatiaPlugin2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new folderSelection());
            }
            catch(Exception e)
            {
                MessageBox.Show("Please turn on CATIA.", "Error");
            }
        }
    }
}
