using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UMLDiagrams;
using UMLDiagrams.Resources;
using UMLDiagrams.ViewModel;

namespace UMLDiagrams
{   
    public partial class MainWindow : Window
    {               
        MainViewModel MainViewModel { get; set; }
        Point? point1; 
        Point? point2; 
        public MainWindow()
        {                       
            InitializeComponent();
            MainViewModel = new MainViewModel();
            DataContext = MainViewModel;         
        }

        private void My_Canvas_Loaded(object sender, RoutedEventArgs e)
        {            
            MainViewModel.Canvas = sender as Canvas;            
        }

        private void ScrollViewer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                var pos = e.GetPosition(MainViewModel.Canvas);
                ScrollViewer scv = (ScrollViewer)sender;

                if (e.Delta > 0)
                {
                    ZoomSlider.Value += 0.1;
                    scv.ScrollToVerticalOffset(scv.VerticalOffset + e.Delta);
                    scv.ScrollToHorizontalOffset(scv.HorizontalOffset + e.Delta);
                }
                if (e.Delta < 0)
                {
                    ZoomSlider.Value -= 0.1;
                    scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
                    scv.ScrollToHorizontalOffset(scv.HorizontalOffset - e.Delta);
                }
                e.Handled = true;
            }
        }

        
        private void ScrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                point1 = e.GetPosition(MainViewModel.Canvas);
        }
        private void ScrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            { 
                point2 = e.GetPosition(MainViewModel.Canvas);
                var x = point2.Value.X - point1.Value.X;
                var y = point2.Value.Y - point1.Value.Y;
                scv.ScrollToVerticalOffset(scv.VerticalOffset - y);
                scv.ScrollToHorizontalOffset(scv.HorizontalOffset - x);
            }
           
        }
    }
}
