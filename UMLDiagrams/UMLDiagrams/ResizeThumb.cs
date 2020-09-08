using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using UMLDiagrams.ViewModel;

namespace UMLDiagrams
{
    public class ResizeThumb : Thumb
    {
        public ResizeThumb()
        {
            DragDelta += new DragDeltaEventHandler(this.ResizeThumb_DragDelta);
        }

        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Control designerItem = this.DataContext as Control;
            var model = designerItem.DataContext as BaseShapeViewModel;

            if (designerItem != null)
            {
                double deltaVertical, deltaHorizontal;

                switch (VerticalAlignment)
                {
                    //case VerticalAlignment.Bottom:
                    //    deltaVertical = Math.Min(-e.VerticalChange, model.Height );
                    //    model.Height -= deltaVertical;
                    //    break;

                    //case VerticalAlignment.Top:
                    //    deltaVertical = Math.Min(e.VerticalChange, model.Height);
                    //    model.Top += deltaVertical;
                    //    model.Height -= deltaVertical;
                    //    break;
                    //default:
                    //    break;
                    case VerticalAlignment.Bottom:
                        deltaVertical = Math.Min(-e.VerticalChange, designerItem.ActualHeight - designerItem.MinHeight);
                        model.Height -= deltaVertical;
                        break;

                    case VerticalAlignment.Top:
                        deltaVertical = Math.Min(e.VerticalChange, designerItem.ActualHeight - designerItem.MinHeight);
                        model.Top += deltaVertical;
                        model.Height -= deltaVertical;
                        break;
                    default:
                        break;
                }

                switch (HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        deltaHorizontal = Math.Min(e.HorizontalChange, designerItem.ActualWidth - designerItem.MinWidth);                      
                        model.Left += deltaHorizontal;                        
                        model.Width -= deltaHorizontal;
                        break;

                    case HorizontalAlignment.Right:
                        deltaHorizontal = Math.Min(-e.HorizontalChange, designerItem.ActualWidth - designerItem.MinWidth);
                        model.Width -= deltaHorizontal;
                        break;
                    default:
                        break;
                }
            }

            e.Handled = true;
        }
    }
}
