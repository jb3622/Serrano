/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 * Idle event timer.  Returns the number of hours, mins or seconds the system has been idle.
 * 
 * Idle is defined by no user interaction.
 *
 */
using System;
using System.Runtime.InteropServices;

namespace Disney.iDash.Framework
{
    internal class IdleTime
    {
        internal struct LASTINPUTINFO
        {
            public int cbSize;
            public uint dwTime;
        }

        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        internal int Ticks()
        {
            var uptime = Environment.TickCount;
            var lastInput = new LASTINPUTINFO();
            lastInput.cbSize = Marshal.SizeOf(lastInput);
            var idleTicks = 0;
            if (GetLastInputInfo(ref lastInput))
                idleTicks = uptime - (int)lastInput.dwTime;
            return idleTicks;
        }

        internal int Seconds()
        {
            return Ticks() / 1000;
        }

        internal int Minutes()
        {
            return Seconds() / 60;
        }

        internal int Hours()
        {
            return Minutes() / 60;
        }

    }
}
