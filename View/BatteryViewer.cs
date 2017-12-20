using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Threading;

namespace Battery
{
    public partial class BatteryViewer : Form
    {
        private BatteryApi batteryApi;

        private Thread batteryConditionUpdater;

        private bool isBatteryConditionUpdaterContinueWork;

        private int batteryConditionUpdatingFrequency = 200;

        private const int DEFAULT_POWER_TIMEOUT_IN_MINUTES = 60;

        public BatteryViewer()
        {
            InitializeComponent();
        }

        private void BatteryViewer_Load(object sender, EventArgs e)
        {
            batteryApi = BatteryApi.GetInstance();
            
            batteryApi.SetPowerTimeout(60);
            isBatteryConditionUpdaterContinueWork = true;
            batteryConditionUpdater = new Thread(UpdateBatteryConditIonProcedure);
            batteryConditionUpdater.Start();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            TurnOffBatteryConditionUpdater();
            SetDefaultPowerTimeout();
            System.Windows.Forms.Application.Exit();
        }

        private void UpdateBatteryConditIonProcedure()
        {
            while (isBatteryConditionUpdaterContinueWork)
            {
                var batteryCondition = batteryApi.GetCurrentBatteryCondition();
                UpdateBatteryCondition(batteryCondition);
                Thread.Sleep(batteryConditionUpdatingFrequency);
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
            isBatteryConditionUpdaterContinueWork = false;
            batteryConditionUpdater.Abort();
        }

        private void SetDefaultPowerTimeout()
        {
            batteryApi.SetPowerTimeout(DEFAULT_POWER_TIMEOUT_IN_MINUTES); 
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
            batteryApi.SetPowerTimeout(selectedValue);
        }

        private void batteryListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
