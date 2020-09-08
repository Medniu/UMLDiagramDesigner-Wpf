using GalaSoft.MvvmLight.Command;
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
using UMLDiagrams;
using UMLDiagrams.ViewModel;

namespace UMLDiagrams
{   
    public partial class MainWindow : Window
    {       
        MainViewModel MainViewModel { get; set; }
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
    }
}
