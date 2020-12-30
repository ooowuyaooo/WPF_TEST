using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controls
{
    /// <summary>
    /// Interaction logic for Instrument.xaml
    /// </summary>
    /// 依赖属性
    public partial class Instrument : UserControl
    {
        //依赖属性

        public int Value
        {
            get { return (int)this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty,value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(Instrument),
                new PropertyMetadata(default(int),new PropertyChangedCallback(OnPropertyChanged)));

        public int Mininum
        {
            get { return (int)GetValue(MininumProperty); }
            set { SetValue(MininumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Mininum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MininumProperty =
            DependencyProperty.Register("Mininum", typeof(int), typeof(Instrument), 
                new PropertyMetadata(default(int), new PropertyChangedCallback(OnPropertyChanged)));



        public int Maxinum
        {
            get { return (int)GetValue(MaxinumProperty); }
            set { SetValue(MaxinumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Maxinum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxinumProperty =
            DependencyProperty.Register("Maxinum", typeof(int), typeof(Instrument), 
                new PropertyMetadata(default(int), new PropertyChangedCallback(OnPropertyChanged)));



        public int Interval
        {
            get { return (int)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Interval.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof(int), typeof(Instrument), 
                new PropertyMetadata(default(int), new PropertyChangedCallback(OnPropertyChanged)));






        public static void OnPropertyChanged(DependencyObject d,DependencyPropertyChangedEventArgs e)
        {
            (d as Instrument).Refresh();
        }
        public Instrument()
        {
            InitializeComponent();
            this.SizeChanged += Instrument_SizeChanged;
        }

        private void Instrument_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double minSize = Math.Min(this.RenderSize.Width, this.RenderSize.Height);
            this.backEllipse.Width = minSize;
            this.backEllipse.Height = minSize;
        }

        private void Refresh()
        {
            double radius = this.backEllipse.Width / 2;
            if (double.IsNaN(radius)) return;

            this.mainCanvas.Children.Clear();

            //double min = 0, max = 100;
            //double scaleCountArea = 10;
            double step = 270.0 / (this.Maxinum - this.Mininum);

            for(int i=0; i < this.Maxinum - this.Mininum; i++)
            {
                Line lineScale = new Line();
                lineScale.X1 =radius-(radius-13)*Math.Cos((i*step-45)*Math.PI/180);
                lineScale.Y1 = radius - (radius-13) * Math.Sin((i * step - 45) * Math.PI / 180);
                lineScale.X2 = radius - (radius-8) * Math.Cos((i * step - 45) * Math.PI / 180);
                lineScale.Y2 = radius - (radius-8) * Math.Sin((i * step - 45) * Math.PI / 180);

                lineScale.Stroke = Brushes.White;
                lineScale.StrokeThickness = 1; 

                this.mainCanvas.Children.Add(lineScale);


            }

            step = 270.0 / Interval;
            int scaleText = this.Mininum;
            for(int i = 0; i <= Interval; i++)
            {
                Line lineScale = new Line();
                lineScale.X1 = radius - (radius - 20) * Math.Cos((i * step - 45) * Math.PI / 180);
                lineScale.Y1 = radius - (radius - 20) * Math.Sin((i * step - 45) * Math.PI / 180);
                lineScale.X2 = radius - (radius - 8) * Math.Cos((i * step - 45) * Math.PI / 180);
                lineScale.Y2 = radius - (radius - 8) * Math.Sin((i * step - 45) * Math.PI / 180);

                lineScale.Stroke = Brushes.White;
                lineScale.StrokeThickness = 1;

                this.mainCanvas.Children.Add(lineScale);

                TextBlock textScale = new TextBlock();
                textScale.Width = 34;
                textScale.TextAlignment = TextAlignment.Center;
                textScale.FontSize = 14;
                textScale.Text = (scaleText +(this.Maxinum - this.Mininum) / Interval * i).ToString();

                textScale.Foreground = Brushes.White;
                Canvas.SetLeft(textScale, radius - (radius - 36) * Math.Cos((i * step - 45) * Math.PI / 180)-17);
                Canvas.SetTop(textScale, radius - (radius - 36) * Math.Sin((i * step - 45) * Math.PI / 180)-10);

                this.mainCanvas.Children.Add(textScale);
            }

            string sData = "M{0} {1} A{0} {0} 0 1 1 {1} {2}";
            sData = string.Format(sData, radius / 2,radius,radius*1.5);
            var converter = TypeDescriptor.GetConverter(typeof(Geometry));
            this.circle.Data = (Geometry)converter.ConvertFrom(sData);

            step = 270.0 / (this.Maxinum - this.Mininum);
            //this.rtPointer.Angle=this.Value * step - 45;
            //double value = double.IsNaN(this.Value) ? 0 : this.Value;

            DoubleAnimation da = new DoubleAnimation((this.Value-this.Mininum) * step - 45, new Duration(TimeSpan.FromMilliseconds(200)));
            this.rtPointer.BeginAnimation(RotateTransform.AngleProperty, da);

            sData = "M{0} {1},{1} {2},{1} {3}";
            sData = string.Format(sData,radius*0.3, radius,radius-5,radius+5);
            this.pointer.Data = (Geometry)converter.ConvertFrom(sData);


        }
    }
}
