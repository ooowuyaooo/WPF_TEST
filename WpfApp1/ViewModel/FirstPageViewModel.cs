using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using WpfApp1.Common;
using WpfApp1.DataAccess;
using WpfApp1.Model;

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

        public ObservableCollection<CourseSeriesModel> CourseSeriesList { get; set; } = new ObservableCollection<CourseSeriesModel>();

        Random random = new Random();
        bool taskSwitch = true;
        List<Task> taskList = new List<Task>();
        public FirstPageViewModel()
        {
            this.RefreshInstrumentValue();
            this.InitCourseSeries();
        }

        private void InitCourseSeries()
        {
            //CourseSeriesList.Add(new CourseSeriesModel
            //{
            //    CourseName = "C#",
            //    SeriesCollection = new LiveCharts.SeriesCollection { new PieSeries { Title="eeeedddiiisss",
            //        Values=new ChartValues<ObservableValue>{ new ObservableValue(123)},DataLabels=false, 

            //    },new PieSeries { Title="eeeedddiiisss",
            //        Values=new ChartValues<ObservableValue>{ new ObservableValue(123)},DataLabels=false,

            //    } },
            //    SeriesList=new ObservableCollection<SeriesModel>
            //    {
            //        new SeriesModel{ SeriesName = "Cloudd",CurrentValue=161,IsGrowing=false,ChangeRate=75},
            //        new SeriesModel{ SeriesName = "Cloudd",CurrentValue=161,IsGrowing=false,ChangeRate=75},
            //        new SeriesModel{ SeriesName = "Cloudd",CurrentValue=161,IsGrowing=false,ChangeRate=75},
            //        new SeriesModel{ SeriesName = "Cloudd",CurrentValue=161,IsGrowing=false,ChangeRate=75},
            //        new SeriesModel{ SeriesName = "Cloudd",CurrentValue=161,IsGrowing=false,ChangeRate=75}
            //    }
            //}) ;
            //CourseSeriesList.Add(new CourseSeriesModel
            //{
            //    CourseName = "C#",
            //    SeriesCollection = new LiveCharts.SeriesCollection { new PieSeries { Title="eeeedddiiisss",
            //        Values=new ChartValues<ObservableValue>{ new ObservableValue(123)},DataLabels=false,

            //    },new PieSeries { Title="eeeedddiiisss",
            //        Values=new ChartValues<ObservableValue>{ new ObservableValue(123)},DataLabels=false,

            //    } },
            //    SeriesList = new ObservableCollection<SeriesModel>
            //    {
            //        new SeriesModel{ SeriesName = "Cloudd",CurrentValue=161,IsGrowing=false,ChangeRate=75},
            //        new SeriesModel{ SeriesName = "Cloudd",CurrentValue=161,IsGrowing=false,ChangeRate=75},
            //        new SeriesModel{ SeriesName = "Cloudd",CurrentValue=161,IsGrowing=false,ChangeRate=75},
            //        new SeriesModel{ SeriesName = "Cloudd",CurrentValue=161,IsGrowing=false,ChangeRate=75},
            //        new SeriesModel{ SeriesName = "Cloudd",CurrentValue=161,IsGrowing=false,ChangeRate=75}
            //    }
            //});
            //CourseSeriesList.Add(new CourseSeriesModel
            //{
            //    CourseName = "C#",
            //    SeriesCollection = new LiveCharts.SeriesCollection { new PieSeries { Title="eeeedddiiisss",
            //        Values=new ChartValues<ObservableValue>{ new ObservableValue(123)},DataLabels=false,

            //    },new PieSeries { Title="eeeedddiiisss",
            //        Values=new ChartValues<ObservableValue>{ new ObservableValue(123)},DataLabels=false,

            //    } },
            //    SeriesList = new ObservableCollection<SeriesModel>
            //    {
            //        new SeriesModel{ SeriesName = "Cloudd",CurrentValue=161,IsGrowing=false,ChangeRate=75},
            //        new SeriesModel{ SeriesName = "Cloudd",CurrentValue=161,IsGrowing=false,ChangeRate=75},
            //        new SeriesModel{ SeriesName = "Cloudd",CurrentValue=161,IsGrowing=false,ChangeRate=75},
            //        new SeriesModel{ SeriesName = "Cloudd",CurrentValue=161,IsGrowing=false,ChangeRate=75},
            //        new SeriesModel{ SeriesName = "Cloudd",CurrentValue=161,IsGrowing=false,ChangeRate=75}
            //    }
            //});

            var cList = LocalDataAccess.GetInstance().GetCoursePlayRecord();
            foreach (var item in cList)
                this.CourseSeriesList.Add(item);
            
        }

        private void RefreshInstrumentValue()
        {
            var task = Task.Factory.StartNew(new Action(async() =>
            {
                while (taskSwitch)
                {
                    InstrumentValue = random.Next(Math.Max(this.InstrumentValue - 20, -10), Math.Min(this.InstrumentValue + 20, 90));

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
