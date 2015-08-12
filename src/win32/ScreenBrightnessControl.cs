using System;
using System.Linq;
using System.Management;

namespace DesktopScreenControl
{
    public static class ScreenBrightnessControl
    {
        public static void SetBrightness(double value)
        {
            if (value < 0 || value > 1.0)
                throw new ArgumentOutOfRangeException("value");

            var mclass = new ManagementClass("WmiMonitorBrightnessMethods")
            {
                Scope = new ManagementScope(@"\\.\root\wmi")
            };

            foreach (var instance in mclass.GetInstances().Cast<ManagementObject>())
            {
                const ulong timeout = 1;
                var brightness = (ushort)(value * 100);
                var args = new object[] { timeout, brightness };
                instance.InvokeMethod("WmiSetBrightness", args);
            }
        }

        public static double GetBrightness()
        {
            var scope = new ManagementScope("root\\WMI");
            var query = new SelectQuery("SELECT * FROM WmiMonitorBrightness");

            using (var searcher = new ManagementObjectSearcher(scope, query))
            using (var objectCollection = searcher.Get())
            {
                var value = objectCollection.Cast<ManagementObject>()
                    .SelectMany(x => x.Properties.Cast<PropertyData>())
                    .Where(x => x.Name == "CurrentBrightness")
                    .Select(x => Convert.ToInt32(x.Value))
                    .FirstOrDefault();

                return 0.01 * value;
            }
        }
    }
}
