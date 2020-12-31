using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    public class CoursePageViewModel
    {
        public ObservableCollection<CategoryItemModel> CategoryCourses { get; set; }
        public ObservableCollection<CategoryItemModel> CategoryTechnology { get; set; }
        public ObservableCollection<CategoryItemModel> CategoryTeacher { get; set; }

        public CoursePageViewModel()
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
            this.CategoryTeacher.Add(new CategoryItemModel("EDIS"));
            this.CategoryTeacher.Add(new CategoryItemModel("HILL"));
            this.CategoryTeacher.Add(new CategoryItemModel("ROGER"));
        }
   
    }
}
