using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;

namespace Battery
{
    public class BatteryApi
    {
        private const string BatteryStatusPropertyName = "BatteryStatus";
        private const string BatteryEstimatedChargeRemainingPropertyName = "EstimatedChargeRemaining";
        private const string BatteryEstimatedRuntimePropertyName = "EstimatedRunTime";

        public static BatteryApi Instance { get; private set; }

        private BatteryApi()
        { }

        public static BatteryApi GetInstance()
        {
            if (Instance == null)
            {
                Instance = new BatteryApi();
            }
            return Instance;
        }

        public BatteryCondition GetCurrentBatteryCondition()
        {
            var query = new ObjectQuery("Select * FROM Win32_Battery");
            var searcher = new ManagementObjectSearcher(query);

            var collection = searcher.Get();

            var batteryCondition = new BatteryCondition();
            foreach (ManagementObject wmiBattery in collection)
            {
                batteryCondition
                    .SetPlugType(Convert.ToInt32(wmiBattery[BatteryStatusPropertyName]));

                batteryCondition
                    .SetChargeStatus(Convert.ToInt32(wmiBattery[BatteryEstimatedChargeRemainingPropertyName]));

                batteryCondition
                    .SetEstimatedRunTime(Convert.ToInt32(wmiBattery[BatteryEstimatedRuntimePropertyName]));
            }

            return batteryCondition;
        }

        public void SetPowerTimeout(int minutes)
        {
            var process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.Arguments = "/c powercfg /x -monitor-timeout-dc " + minutes;
            process.Start();
        }
    }
}
