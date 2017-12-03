using Battery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Battery
{
    public class BatteryCondition
    {
        public string PlugType { get; private set; }

        public string ChargeStatus { get; private set; }

        public string EstimatedRunTime { get; private set; }

        private static readonly int MAX_BATTERY_STATUS = 100;
        private static readonly int MIN_BATTERY_STATUS = 0;
        private static readonly string BATTERY_IS_CHARGING_MESSAGE = "battery is charging";
        private const int ESTIMATED_RUN_TIME_OF_CHARGING_BATTERY = 71582788;

        public void SetPlugType(int plugTypeId)
        {
            PlugType = ((PlugType) plugTypeId).ToString();
        }

        public void SetChargeStatus(int chargeStatus)
        {
            if(IsChargeStatusValid(chargeStatus))
            {
                ChargeStatus = $"{chargeStatus} %";
            }
        }
        
        public void SetEstimatedRunTime(int estimatedRunTime)
        {
            if(IsEstimatedRunTimeValid(estimatedRunTime))
            {
                EstimatedRunTime = (ESTIMATED_RUN_TIME_OF_CHARGING_BATTERY == estimatedRunTime) ? 
                    BATTERY_IS_CHARGING_MESSAGE : EstimatedRunTime = $"{estimatedRunTime} minutes";
            }
        }

        private bool IsChargeStatusValid(int chargeStatus)
        {
            return chargeStatus > MIN_BATTERY_STATUS && chargeStatus <= MAX_BATTERY_STATUS;
        }

        private bool IsEstimatedRunTimeValid(int estimatedRunTime)
        {
            return estimatedRunTime > 0;
        }
    }
}
