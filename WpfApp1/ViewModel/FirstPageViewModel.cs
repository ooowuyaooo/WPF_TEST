using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Common;

namespace WpfApp1.ViewModel
{
    public class FirstPageViewModel:NotifyBase
    {
        private int _instrumentValue=0;

        public int InstrumentValue
        {
            get { return _instrumentValue; }
            set { _instrumentValue = value;this.DoNotify(); }
        }

        Random random = new Random();
        bool taskSwitch = true;
        List<Task> taskList = new List<Task>();
        public FirstPageViewModel()
        {
            this.RefreshInstrumentValue();
        }

        private void RefreshInstrumentValue()
        {
            var task = Task.Factory.StartNew(new Action(async() =>
            {
                while (taskSwitch)
                {
                    InstrumentValue = random.Next(Math.Max(this.InstrumentValue-5,-10),Math.Min(this.InstrumentValue+5,90));

                    await Task.Delay(1000);
                }
            }));
            taskList.Add(task);
        }

        public void Dispose()
        {
            try {
            taskSwitch = false;
            Task.WaitAll(this.taskList.ToArray()); }
            catch
            {

            }
        }

    }
}
