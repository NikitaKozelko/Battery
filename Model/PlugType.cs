using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battery.Model
{
    public enum PlugType
    {
        Battery = 1,
        AC = 2,
        FullyCharged = 3,
        Low = 4,
        Critical =5,
        Charging =6,
        ChargingAndHigh = 7,
        ChargingAndLow = 8,
        ChargingAndCritical = 9,
        Undefined = 10, 
        PartiallyCharged = 11
    }
}
