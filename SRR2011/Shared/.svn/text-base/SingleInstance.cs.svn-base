/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 *  Used to determine whether the application is already running and activate that instance if it is.
 *  
*/
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace Disney.iDash.Shared
{
    public class SingleInstance
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow); 

        private bool _isRunning = false;

        /// <summary>
        /// productName should be set to Application.ProductName for the entry assembly or
        /// a unique string that is not used by any other application.
        /// </summary>
        /// <param name="productName"></param>
        public SingleInstance()
        {
            var current = Process.GetCurrentProcess();
            foreach (Process process in Process.GetProcessesByName(current.ProcessName))
            {
                if (process.Id != current.Id)
                {
                    _isRunning = true;
                    break;
                }
            }
        }

        public bool IsRunning
        {
            get { return _isRunning; }
        }

        public void Activate()
        {
            var current = Process.GetCurrentProcess();
            const int SW_RESTORE = 9;

            foreach (Process process in Process.GetProcessesByName(current.ProcessName))
            {
                if (process.Id != current.Id)
                {
                    SetForegroundWindow(process.MainWindowHandle);
                    ShowWindowAsync(process.MainWindowHandle, SW_RESTORE);
                    break;
                }
            }
        }
    }
}
