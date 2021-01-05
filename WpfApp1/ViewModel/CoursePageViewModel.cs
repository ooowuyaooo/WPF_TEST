using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Common;
using WpfApp1.DataAccess;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    public class CoursePageViewModel
    {
        public ObservableCollection<CategoryItemModel> CategoryCourses { get; set; }
        public ObservableCollection<CategoryItemModel> CategoryTechnology { get; set; }
        public ObservableCollection<CategoryItemModel> CategoryTeacher { get; set; }

        public ObservableCollection<CourseModel> CourseList { get; set; }



        private List<CourseModel> courseAll;
        public CommandBase TeacherFilterCommand { get; set; }




        public CommandBase OpenCourseUrl { get; set; }

        public CoursePageViewModel()
        {
            this.InitCategory();
            this.InitCourseList();

            this.OpenCourseUrl = new CommandBase();
            this.OpenCourseUrl.DoCanExcute = new Func<object, bool>((o) => true);
            this.OpenCourseUrl.DoExcute = new Action<object>((o) => {
                System.Diagnostics.Process.Start(o.ToString());
            });

            this.TeacherFilterCommand = new CommandBase();
            this.TeacherFilterCommand.DoCanExcute = new Func<object, bool>((o) => true);
            this.TeacherFilterCommand.DoExcute = new Action<object>(DoFilter);
        }

        private void DoFilter(object o)
        {
            string teacher = o.ToString();
            List<CourseModel> temp = courseAll;
            if(teacher!="全部")
            {
                temp = courseAll.Where(c => c.Teachers.Exists(e => e == teacher)).ToList();
            }

            CourseList.Clear();
            foreach (var item in temp)
                CourseList.Add(item);

            
        }

        private void InitCategory()
        {
            this.CategoryCourses = new ObservableCollection<CategoryItemModel>();
            this.CategoryCourses.Add(new CategoryItemModel("全部", true));
            this.CategoryCourses.Add(new CategoryItemModel("公开课"));
            this.CategoryCourses.Add(new CategoryItemModel("VIP课程"));

            this.CategoryTechnology = new ObservableCollection<CategoryItemModel>();
            this.CategoryTechnology.Add(new CategoryItemModel("全部", true));
            this.CategoryTechnology.Add(new CategoryItemModel("C#"));
            this.CategoryTechnology.Add(new CategoryItemModel("JAVA"));
            this.CategoryTechnology.Add(new CategoryItemModel("Winform"));
            this.CategoryTechnology.Add(new CategoryItemModel("wechat"));
            this.CategoryTechnology.Add(new CategoryItemModel("eplan"));
            this.CategoryTechnology.Add(new CategoryItemModel("Tia"));
            this.CategoryTechnology.Add(new CategoryItemModel("beckhoff"));
            this.CategoryTechnology.Add(new CategoryItemModel("siemens"));
            this.CategoryTechnology.Add(new CategoryItemModel("mitsbishi"));
            this.CategoryTechnology.Add(new CategoryItemModel("omron"));

            this.CategoryTeacher = new ObservableCollection<CategoryItemModel>();
            this.CategoryTeacher.Add(new CategoryItemModel("全部", true));
            foreach (var item in LocalDataAccess.GetInstance().GetTeachers())
                this.CategoryTeacher.Add(new CategoryItemModel(item));
        }
        private void InitCourseList()
        {
            courseAll = LocalDataAccess.GetInstance().GetCourses();


            CourseList =new ObservableCollection<CourseModel>(courseAll);
        }
   
    }
}
