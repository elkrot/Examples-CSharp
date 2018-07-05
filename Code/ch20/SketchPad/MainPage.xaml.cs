using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Printing;

namespace SketchPad
{
    public partial class MainPage : UserControl
    {
        private bool _isDrawing = false;
        private Point _prevPt;

        public MainPage()
        {
            InitializeComponent();

            this.MouseLeftButtonDown += new MouseButtonEventHandler(MainPage_MouseLeftButtonDown);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(MainPage_MouseLeftButtonUp);
            this.MouseMove += new MouseEventHandler(MainPage_MouseMove);
        }

        void MainPage_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDrawing)
            {
                Point pt = e.GetPosition(canvas);
                if (pt != _prevPt)
                {
                    Line line = new Line();
                    line.StrokeThickness = 2;
                    line.Stroke = new SolidColorBrush(Colors.Black);
                    line.X1 = _prevPt.X;
                    line.Y1 = _prevPt.Y;
                    line.X2 = pt.X;
                    line.Y2 = pt.Y;
                    canvas.Children.Add(line);
                    _prevPt = pt;
                }
            }
        }

        void MainPage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isDrawing = false;
        }

        void MainPage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isDrawing = true;
            _prevPt = e.GetPosition(canvas);
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
        }

        private void buttonPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += new EventHandler<PrintPageEventArgs>(doc_PrintPage);
            doc.Print();
        }

        void doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            //simply set the PageVisual property to the UIElement
            //that you want to print
            e.PageVisual = canvas;
            e.HasMorePages = false;
        }
    }
}
