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
                    case VerticalAlignment.Bottom:
                        deltaVertical = Math.Min(-e.VerticalChange, designerItem.ActualHeight - designerItem.MinHeight);
                        ResizeBottom(model, deltaVertical);
                        break;                        

                    case VerticalAlignment.Top:
                        deltaVertical = Math.Min(e.VerticalChange, designerItem.ActualHeight - designerItem.MinHeight);
                        ResizeTop(model, deltaVertical);                     
                        break;

                    default:
                        break;
                }

                switch (HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        deltaHorizontal = Math.Min(e.HorizontalChange, designerItem.ActualWidth - designerItem.MinWidth);
                        ResizeLeft(model, deltaHorizontal);                        
                        break;

                    case HorizontalAlignment.Right:
                        deltaHorizontal = Math.Min(-e.HorizontalChange, designerItem.ActualWidth - designerItem.MinWidth);                       
                        ResizeRight(model, deltaHorizontal);
                        break;

                    default:
                        break;
                }
            }
            e.Handled = true;
            
        }
        private void ResizeBottom(BaseShapeViewModel model, double deltaVertical)
        {
            model.Height -= deltaVertical;
            if (!(model is SystemViewModel))
            {
                model.ConnectorBottom.Top -= deltaVertical;
                model.ConnectorLeft.Top -= (deltaVertical / 2);
                model.ConnectorRight.Top -= (deltaVertical / 2);

                model.ConnectorBottom.Center = new Point(model.ConnectorBottom.Center.X, model.ConnectorBottom.Center.Y - deltaVertical);
                model.ConnectorLeft.Center = new Point(model.ConnectorLeft.Center.X, model.ConnectorLeft.Center.Y - deltaVertical / 2);
                model.ConnectorRight.Center = new Point(model.ConnectorRight.Center.X, model.ConnectorRight.Center.Y - deltaVertical / 2);
            }
        
        }
        private void ResizeTop(BaseShapeViewModel model, double deltaVertical)
        {
            model.Top += deltaVertical;
            model.Height -= deltaVertical;
            if (!(model is SystemViewModel))
            {
                model.ConnectorTop.Top += deltaVertical;
                model.ConnectorLeft.Top += (deltaVertical / 2);
                model.ConnectorRight.Top += (deltaVertical / 2);

                model.ConnectorTop.Center = new Point(model.ConnectorTop.Center.X, model.ConnectorTop.Center.Y + deltaVertical);
                model.ConnectorLeft.Center = new Point(model.ConnectorLeft.Center.X, model.ConnectorLeft.Center.Y + deltaVertical / 2);
                model.ConnectorRight.Center = new Point(model.ConnectorRight.Center.X, model.ConnectorRight.Center.Y + deltaVertical / 2);
            }

        }
        private void ResizeLeft(BaseShapeViewModel model, double deltaHorizontal)
        {
            model.Left += deltaHorizontal;
            model.Width -= deltaHorizontal;
            if (!(model is SystemViewModel))
            {
                model.ConnectorLeft.Left += deltaHorizontal;
                model.ConnectorTop.Left += (deltaHorizontal / 2);
                model.ConnectorBottom.Left += (deltaHorizontal / 2);

                model.ConnectorTop.Center = new Point(model.ConnectorTop.Center.X + deltaHorizontal / 2, model.ConnectorTop.Center.Y);
                model.ConnectorLeft.Center = new Point(model.ConnectorLeft.Center.X + deltaHorizontal, model.ConnectorLeft.Center.Y);
                model.ConnectorBottom.Center = new Point(model.ConnectorBottom.Center.X + deltaHorizontal / 2, model.ConnectorBottom.Center.Y);
            }
        }
        private void ResizeRight(BaseShapeViewModel model, double deltaHorizontal)
        {
            model.Width -= deltaHorizontal;
            if (!(model is SystemViewModel))
            {
                model.ConnectorRight.Left -= deltaHorizontal;
                model.ConnectorTop.Left -= (deltaHorizontal / 2);
                model.ConnectorBottom.Left -= (deltaHorizontal / 2);

                model.ConnectorTop.Center = new Point(model.ConnectorTop.Center.X - deltaHorizontal / 2, model.ConnectorTop.Center.Y);
                model.ConnectorRight.Center = new Point(model.ConnectorRight.Center.X - deltaHorizontal, model.ConnectorRight.Center.Y);
                model.ConnectorBottom.Center = new Point(model.ConnectorBottom.Center.X - deltaHorizontal / 2, model.ConnectorBottom.Center.Y);
            }
        }
    }
}
