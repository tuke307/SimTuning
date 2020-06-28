﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SimTuning.windows.Business
{
    public static class Functions
    {
        public static string GoToSite(string url)
        {
            Process myProcess = new Process();

            try
            {
                // true is the default, but it is important not to set it to false
                myProcess.StartInfo.UseShellExecute = true;
                myProcess.StartInfo.FileName = url;
                myProcess.Start();
                //myProcess.Close();
                //myProcess.WaitForExit();
            }
            catch
            {
                return "Fehler beim Starten";
            }

            return null;
        }
    }
}