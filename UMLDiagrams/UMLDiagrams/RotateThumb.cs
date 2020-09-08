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
            //ContentControl designerItem = this.DataContext as ContentControl;
            Control designerItem = this.DataContext as ContentControl;
            var model = designerItem.DataContext as BaseShapeViewModel;           
            model.Angle += 1;        
        }
    }
}
