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

        private const int MaxBatteryStatus = 100;
        private const int MinBatteryStatus = 0;
        private const string BatteryIsChargingMessage = "battery is charging";
        private const int EstimatedRunTimeOfChargingBattery = 71582788;

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
                 EstimatedRunTime = (EstimatedRunTimeOfChargingBattery == estimatedRunTime) ? 
                     BatteryIsChargingMessage : EstimatedRunTime = $"{estimatedRunTime} minutes";
            }
        }

        private bool IsChargeStatusValid(int chargeStatus)
        {
            return chargeStatus > MinBatteryStatus && chargeStatus <= MaxBatteryStatus;
        }

        private bool IsEstimatedRunTimeValid(int estimatedRunTime)
        {
            return estimatedRunTime > 0;
        }
    }
}
