using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Text.RegularExpressions;
using System.Threading;

namespace Battery
{
    public partial class BatteryViewer : Form
    {
        private BatteryApi _batteryApi;

        private Thread _batteryConditionUpdater;

        private bool _isBatteryConditionUpdaterContinueWork;

        private int _batteryConditionUpdatingFrequency = 100;

        private int _defaultPowerTimeoutInMinutes = 60;

        private const int TimeLen = 10;

        public BatteryViewer()
        {
            _defaultPowerTimeoutInMinutes = GetCurrentTimeout();
            InitializeComponent();
        }

        private void BatteryViewer_Load(object sender, EventArgs e)
        {
            _batteryApi = BatteryApi.GetInstance();
            
            _batteryApi.SetPowerTimeout(60);
            _isBatteryConditionUpdaterContinueWork = true;
            _batteryConditionUpdater = new Thread(UpdateBatteryConditIonProcedure);
            _batteryConditionUpdater.Start();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            TurnOffBatteryConditionUpdater();
            SetDefaultPowerTimeout();
            System.Windows.Forms.Application.Exit();
        }

        private void UpdateBatteryConditIonProcedure()
        {
            while (_isBatteryConditionUpdaterContinueWork)
            {
                var batteryCondition = _batteryApi.GetCurrentBatteryCondition();
                UpdateBatteryCondition(batteryCondition);
                Thread.Sleep(_batteryConditionUpdatingFrequency);
            }
        }

        // This delegate enables asynchronous calls for setting
        // updated battery condition on BatteryListBox.
        delegate void UpdateBatteryConditionCallBack(BatteryCondition batteryCondition);

        private void UpdateBatteryCondition(BatteryCondition batteryCondition)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.batteryListBox.InvokeRequired)
            {
                UpdateBatteryConditionCallBack callBack
                    = new UpdateBatteryConditionCallBack(UpdateBatteryCondition);
                this.Invoke(callBack, new object[] { batteryCondition});
            }
            else
            {
                SetUpdatedBatteryCondition(batteryCondition);
            }
        }

        private void SetUpdatedBatteryCondition(BatteryCondition batteryCondition) 
        {
            batteryListBox.Items.Clear();
            batteryListBox.Items.Add($"Battery plug type: \t\t{batteryCondition.PlugType}");
            batteryListBox.Items.Add($"Battery charge status: \t{batteryCondition.ChargeStatus}");
            batteryListBox.Items.Add($"Battery estimated runtime: \t{batteryCondition.EstimatedRunTime}");
        }

        private void TurnOffBatteryConditionUpdater()
        {
            _isBatteryConditionUpdaterContinueWork = false;
            _batteryConditionUpdater.Abort();
        }

        private void SetDefaultPowerTimeout()
        {
            _batteryApi.SetPowerTimeout(_defaultPowerTimeoutInMinutes); 
        }

        private void BatteryViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            TurnOffBatteryConditionUpdater();
            SetDefaultPowerTimeout();
            System.Windows.Forms.Application.Exit();
        }

        private void TimeDisplaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedValue = Int32.Parse(TimeDisplaComboBox.SelectedItem.ToString());
            _batteryApi.SetPowerTimeout(selectedValue);
        }

        private void batteryListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public int GetCurrentTimeout()
        {
            var p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c powercfg /q";
            p.Start();
            var powercfgOut = p.StandardOutput.ReadToEnd();
            var reg = new Regex("VIDEOIDLE.*\\n.*\\n.*\\n.*\\n.*\\n.*\\n.*");
            var videoidle = reg.Match(powercfgOut).Value;
            var batteryIdle = videoidle.Substring(videoidle.Length - 1 - TimeLen).TrimEnd();
            return Convert.ToInt32(batteryIdle, 16) / 60;
        }
    }
}
