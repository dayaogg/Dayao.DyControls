using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dayao.DyControls
{
    /// <summary>
    /// Thermometer.xaml 的交互逻辑
    /// </summary>
    public partial class Thermometer : UserControl
    {
        public Thermometer()
        {
            InitializeComponent();
            this.SizeChanged += Thermometer_SizeChanged;
        }

        private void Thermometer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Refresh();
        }



        public int value
        {
            get { return (int)GetValue(valueProperty); }
            set { SetValue(valueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty valueProperty =
            DependencyProperty.Register("value", typeof(int), typeof(Thermometer), new PropertyMetadata(0,new PropertyChangedCallback(OnRangeChanged)));


        public int Minvalue
        {
            get { return (int)GetValue(MinvalueProperty); }
            set { SetValue(MinvalueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Minvalue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinvalueProperty =
            DependencyProperty.Register("Minvalue", typeof(int), typeof(Thermometer), new PropertyMetadata(0,new PropertyChangedCallback(OnRangeChanged)));

        private static void OnRangeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Thermometer).Refresh();
        }

        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(int), typeof(Thermometer), new PropertyMetadata(100, new PropertyChangedCallback(OnRangeChanged)));

        
        private void Refresh() {

            var h = mainCanvas.ActualHeight;
            if (h <= 0) return;

            double w = 100;

            double setpCount = MaxValue - Minvalue;
            if (setpCount ==0) {

                return;
            
            }
            double setp = h / setpCount;

            this.mainCanvas.Children.Clear();
            for (int i = 0;i <=setpCount;i++) {

                Line line = new Line();

                line.Y1 = i*setp;
                line.Y2 = i * setp;
                mainCanvas.Children.Add(line);

                if (i % 10 == 0)
                {
                    line.X1 = 22;
                    line.X2 = w - 22;


                    TextBlock text = new TextBlock();
                    text.Text = MaxValue - i + "";
                   
                    mainCanvas.Children.Add(text);
                    Canvas.SetTop(text, i * setp-8);
                    Canvas.SetLeft(text, 0);

                    TextBlock text2 = new TextBlock();
                    text2.Text = ( MaxValue-i) + "";
                    Canvas.SetTop(text2, i * setp-8);
                    Canvas.SetLeft(text2, w - 18);
                    mainCanvas.Children.Add(text2);
                }
                else if (i % 5 == 0)
                {
                    line.X1 = 27;
                    line.X2 = w - 27;
                    TextBlock text = new TextBlock();
                    text.Text = MaxValue - i + "";



                    mainCanvas.Children.Add(text);
                    Canvas.SetTop(text, i * setp - 8);
                    Canvas.SetLeft(text, 0);

                    TextBlock text2 = new TextBlock();
                    text2.Text = (MaxValue - i) + "";
                    Canvas.SetTop(text2, i * setp - 8);
                    Canvas.SetLeft(text2, w - 18);
                    mainCanvas.Children.Add(text2);
                }
                else 
                {
                    line.X1 = 32;
                    line.X2 = w - 32;
                }
                
                line.Stroke = Brushes.Black;
                line.StrokeThickness = 1;



                try
                {
                    liquidColumn.Height = 20 + (value - Minvalue) * setp;
                }
                catch (Exception)
                {

                    liquidColumn.Height = 20;
                }



            }


        }

    }
}
