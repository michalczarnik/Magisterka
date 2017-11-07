using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CatiaPlugin2
{
    static class CatiaService
    {
        static private INFITF.Application Catia = null;
        public static bool InitializeCatia()
        {
            try
            {
                Catia = (INFITF.Application)Marshal.GetActiveObject("Catia.Application");
            }catch(Exception e)
            {
                MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        static public void RunMacro(string libraryPath, string macroPath, List <Object> parameters)
        {
            if (Catia != null)
            {
                Catia.SystemService.ExecuteScript(libraryPath, INFITF.CatScriptLibraryType.catScriptLibraryTypeDirectory, macroPath,
                "CATMain", parameters.ToArray());
            }
            else
            {
                MessageBox.Show("Catia nie jest uruchomiona", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
