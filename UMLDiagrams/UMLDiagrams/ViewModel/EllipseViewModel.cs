using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLDiagrams.Model;
using System.Collections.ObjectModel;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace UMLDiagrams.ViewModel
{
    public class EllipseViewModel : BaseShapeViewModel
    {      
        public EllipseViewModel()
        {            

        }
        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            EllipseViewModel n = (EllipseViewModel)((FrameworkElement)sender).DataContext;
            n.Left += e.HorizontalChange;
            n.Top += e.VerticalChange;
        }
    }
}
