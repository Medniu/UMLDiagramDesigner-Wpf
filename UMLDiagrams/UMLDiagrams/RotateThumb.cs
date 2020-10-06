using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using UMLDiagrams.ViewModel;

namespace UMLDiagrams
{
    public class RotateThumb : Thumb
    {
       
        public RotateThumb()
        {
            DragDelta += new DragDeltaEventHandler(this.RotateThumb_DragDelta);
        }
       

        private void RotateThumb_DragDelta(object sender, DragDeltaEventArgs e)
        { 
            double h = e.HorizontalChange;          
            Control designerItem = DataContext as ContentControl;
            var model = designerItem.DataContext as BaseShapeViewModel;
            if (h > 0) { model.Angle += 1; }
            else { model.Angle -= 1; }            
        }        
    }
}
