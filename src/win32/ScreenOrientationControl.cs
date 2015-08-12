using System;
using System.Runtime.InteropServices;

namespace DesktopScreenControl
{
    public static class ScreenOrientationControl
    {
        private static readonly int[] OrientationValues = {NativeMethods.DMDO_DEFAULT,
                                                    NativeMethods.DMDO_90,
                                                    NativeMethods.DMDO_180,
                                                    NativeMethods.DMDO_270};

        public static int RotateClockwise()
        {
            var dm = NativeMethods.CreateDevmode();
            GetSettings(ref dm);

            var temp = dm.dmPelsHeight;
            dm.dmPelsHeight = dm.dmPelsWidth;
            dm.dmPelsWidth = temp;

            var index = Array.IndexOf(OrientationValues, (object)dm.dmDisplayOrientation);
            var newIndex = (index == 3) ? 0 : index + 1;
            dm.dmDisplayOrientation = OrientationValues[newIndex];

            return ChangeSettings(dm);
        }

        public static int RotateCounterClockwise()
        {
            var dm = NativeMethods.CreateDevmode();
            GetSettings(ref dm);

            var temp = dm.dmPelsHeight;
            dm.dmPelsHeight = dm.dmPelsWidth;
            dm.dmPelsWidth = temp;

            var index = Array.IndexOf(OrientationValues, (object)dm.dmDisplayOrientation);
            var newIndex = (index == 0) ? 3 : index - 1;
            dm.dmDisplayOrientation = OrientationValues[newIndex];

            return ChangeSettings(dm);
        }

        public static int Flip()
        {
            var dm = NativeMethods.CreateDevmode();
            GetSettings(ref dm);

            switch (dm.dmDisplayOrientation)
            {
                case NativeMethods.DMDO_DEFAULT:
                    dm.dmDisplayOrientation = NativeMethods.DMDO_180;
                    break;

                case NativeMethods.DMDO_90:
                    dm.dmDisplayOrientation = NativeMethods.DMDO_270;
                    break;

                case NativeMethods.DMDO_180:
                    dm.dmDisplayOrientation = NativeMethods.DMDO_DEFAULT;
                    break;

                case NativeMethods.DMDO_270:
                    dm.dmDisplayOrientation = NativeMethods.DMDO_90;
                    break;
            }

            return ChangeSettings(dm);
        }

        private static void GetSettings(ref DEVMODE dm)
        {
            GetSettings(ref dm, NativeMethods.ENUM_CURRENT_SETTINGS);
        }

        private static void GetSettings(ref DEVMODE dm, int iModeNum)
        {
            NativeMethods.EnumDisplaySettings(null, iModeNum, ref dm);
        }

        private static int ChangeSettings(DEVMODE dm)
        {
            return NativeMethods.ChangeDisplaySettings(ref dm, 0);
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct DEVMODE
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmDeviceName;

            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public int dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmFormName;

            public short dmLogPixels;
            public short dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        };

        private class NativeMethods
        {
            [DllImport("user32.dll", CharSet = CharSet.Ansi)]
            public static extern int EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

            [DllImport("user32.dll", CharSet = CharSet.Ansi)]
            public static extern int ChangeDisplaySettings(ref DEVMODE lpDevMode, int dwFlags);

            public static DEVMODE CreateDevmode()
            {
                var dm = new DEVMODE { dmDeviceName = new String(new char[32]), dmFormName = new String(new char[32]) };
                dm.dmSize = (short)Marshal.SizeOf(dm);
                return dm;
            }

            public const int ENUM_CURRENT_SETTINGS = -1;
            public const int DISP_CHANGE_SUCCESSFUL = 0;
            public const int DISP_CHANGE_BADDUALVIEW = -6;
            public const int DISP_CHANGE_BADFLAGS = -4;
            public const int DISP_CHANGE_BADMODE = -2;
            public const int DISP_CHANGE_BADPARAM = -5;
            public const int DISP_CHANGE_FAILED = -1;
            public const int DISP_CHANGE_NOTUPDATED = -3;
            public const int DISP_CHANGE_RESTART = 1;
            public const int DMDO_DEFAULT = 0;
            public const int DMDO_90 = 1;
            public const int DMDO_180 = 2;
            public const int DMDO_270 = 3;
        }
    }
}
