using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;
using UMLDiagrams.ViewModel;

namespace UMLDiagrams
{
    public class MoveThumb : Thumb
    {
        public MoveThumb()
        {
            DragDelta += new DragDeltaEventHandler(this.MoveThumb_DragDelta);
        }

        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ContentControl designerItem = DataContext as ContentControl ;
           
            if (designerItem != null)
            {
                Point dragDelta = new Point(e.HorizontalChange, e.VerticalChange);
                
                RotateTransform rotateTransform = designerItem.RenderTransform as RotateTransform;
                if (rotateTransform != null)
                {
                    dragDelta = rotateTransform.Transform(dragDelta);
                }
                double left = Canvas.GetLeft(designerItem);
                double top = Canvas.GetTop(designerItem);
               
                Canvas.SetLeft(designerItem, left + dragDelta.X);
                Canvas.SetTop(designerItem, top + dragDelta.Y);
               
            }

        }
    }
}

