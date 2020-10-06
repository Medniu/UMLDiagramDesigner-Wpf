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
using System.Windows.Shapes;
using UMLDiagrams.ViewModel;
using UMLDiagrams.View;
using UMLDiagrams.Resources;
using UMLDiagrams;

namespace UMLDiagrams
{
    public class MoveThumb : Thumb
    {        
        public MoveThumb()
        {
            DragDelta += new DragDeltaEventHandler(this.MoveThumb_DragDelta);            
            MouseRightButtonDown += new MouseButtonEventHandler(this.TextBlock_MouseEnter);                                  
            PreviewMouseLeftButtonDown += new MouseButtonEventHandler(this.Select_MouseEnter);            
        }

        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ContentControl designerItem = DataContext as ContentControl;
            var model = designerItem.DataContext as BaseShapeViewModel;

            if (designerItem != null)
            {
                Point dragDelta = new Point(e.HorizontalChange, e.VerticalChange);

                RotateTransform rotateTransform = designerItem.RenderTransform as RotateTransform;
                
                if (rotateTransform != null)
                {
                    dragDelta = rotateTransform.Transform(dragDelta);
                }
               
                Move(model, dragDelta);                                                 
            }            
        }
        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            ContentControl designerItem = DataContext as ContentControl;           
            var model = designerItem.DataContext as TextViewModel;
            
            if (model is TextViewModel)
            {
                TextSetting textSetting = new TextSetting
                {
                    Owner = Window.GetWindow(this)
                };

                if (textSetting.ShowDialog() == true)

                    if (model != null)
                    {
                        model.Text = textSetting.Text; ;
                    }
            }
            
        }
        private void Select_MouseEnter(object sender, MouseEventArgs e)
        {
            ContentControl designerItem = DataContext as ContentControl;
            var model = designerItem.DataContext as BaseShapeViewModel;                    
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (model.IsSelected != true)
                {
                    model.ZIndex += 1;
                    if (model is SystemViewModel || model is DecisionNodeViewModel || model is ClassViewModel)
                    {
                        model.ZIndex = 0;
                    }

                    model.IsSelected = true;
                    if (model is ConnectorViewModel || model is LinePointViewModel)
                    {
                        model.Fill = "Green";
                    }
                }
                else
                {
                    model.IsSelected = false;
                    if (model is ConnectorViewModel || model is LinePointViewModel)
                    {
                        model.Fill = "Red";
                    }
                }
            }                      
        }
        private void CheckLeftBorder(BaseShapeViewModel model)
        {
            if (model.Left < 0)
            {
                model.Left = 0.1;
                if (model is LinePointViewModel)
                {
                    model.Center = new Point(model.Left + model.Width / 2, model.Center.Y);                   
                }

                if (!(model is SystemViewModel) && !(model is TextViewModel) && !(model is LinePointViewModel))
                {
                    model.ConnectorLeft.Left = model.Left - 15;
                    model.ConnectorRight.Left = model.Left + model.Width + 5;
                    model.ConnectorTop.Left = model.Left + model.Width / 2 - 5;
                    model.ConnectorBottom.Left = model.Left + model.Width / 2 - 5;

                    CheckConnectionCenter(model);                   
                }
            }
        }
        private void CheckRightBorder(BaseShapeViewModel model)
        {
            if (model.Left + model.Width > 3000)
            {
                model.Left = 2999.9 - model.Width;

                if (model is LinePointViewModel)
                {
                    model.Center = new Point(model.Left + model.Width / 2, model.Center.Y);
                }

                if (!(model is SystemViewModel) && !(model is TextViewModel) && !(model is LinePointViewModel))
                {                    
                    model.ConnectorLeft.Left = 2999.9 - model.Width - 15;
                    model.ConnectorRight.Left = 2999.9 + 5;
                    model.ConnectorTop.Left = 2999.9 - model.Width / 2 - 5;
                    model.ConnectorBottom.Left = 2999.9 - model.Width / 2 - 5;

                    CheckConnectionCenter(model);                   
                }
            }
        }
        private void CheckTopBorder(BaseShapeViewModel model)
        {
            if (model.Top < 0)
            {
                model.Top = 0.1;
                if (model is LinePointViewModel)
                {
                    model.Center = new Point(model.Center.X , model.Top + model.Height/2);
                }
                if (!(model is SystemViewModel) && !(model is TextViewModel) && !(model is LinePointViewModel))
                {              
                    model.ConnectorLeft.Top = model.Height / 2 - 5;
                    model.ConnectorRight.Top = model.Height / 2 - 5;
                    model.ConnectorTop.Top = model.Top - 15;
                    model.ConnectorBottom.Top = model.Height + 5;

                    CheckConnectionCenter(model);                   
                }
            }
        }
        private void CheckBottomBorder(BaseShapeViewModel model)
        {
            if (model.Top + model.Height > 2000)
            {
                model.Top = 1999.9 - model.Height;

                if (model is LinePointViewModel)
                {
                    model.Center = new Point(model.Center.X, model.Top + model.Height / 2);
                }

                if (!(model is SystemViewModel) && !(model is TextViewModel) && !(model is LinePointViewModel))
                {
                    model.ConnectorLeft.Top = 1999.9 - model.Height / 2 - 5;
                    model.ConnectorRight.Top = 1999.9 - model.Height / 2 - 5;
                    model.ConnectorTop.Top = 1999.9 - model.Height - 15;
                    model.ConnectorBottom.Top = 1999.9 + 5;

                    CheckConnectionCenter(model);                    
                }
            }
        }
        private void Move(BaseShapeViewModel model, Point dragDelta)
        {
            if (model.Left >= 0 && model.Top >= 0 && model.Left + model.Width <= 3000 &&
                        model.Top + model.Height <= 2000 && !(model is ConnectorViewModel))
            {
                model.Left += dragDelta.X;
                model.Top += dragDelta.Y;
               
                if(!(model is SystemViewModel) && !(model is LinePointViewModel) && !(model is TextViewModel))
                {

                    model.ConnectorLeft.Left += dragDelta.X;
                    model.ConnectorLeft.Top += dragDelta.Y;
                    model.ConnectorRight.Left += dragDelta.X;
                    model.ConnectorRight.Top += dragDelta.Y;
                    model.ConnectorBottom.Left += dragDelta.X;
                    model.ConnectorBottom.Top += dragDelta.Y;
                    model.ConnectorTop.Left += dragDelta.X;
                    model.ConnectorTop.Top += dragDelta.Y;

                    model.ConnectorLeft.Center = new Point(model.ConnectorLeft.Center.X + dragDelta.X, model.ConnectorLeft.Center.Y);
                    model.ConnectorLeft.Center = new Point(model.ConnectorLeft.Center.X, model.ConnectorLeft.Center.Y + dragDelta.Y);

                    model.ConnectorTop.Center = new Point(model.ConnectorTop.Center.X + dragDelta.X, model.ConnectorTop.Center.Y);
                    model.ConnectorTop.Center = new Point(model.ConnectorTop.Center.X, model.ConnectorTop.Center.Y + dragDelta.Y);

                    model.ConnectorRight.Center = new Point(model.ConnectorRight.Center.X + dragDelta.X, model.ConnectorRight.Center.Y);
                    model.ConnectorRight.Center = new Point(model.ConnectorRight.Center.X, model.ConnectorRight.Center.Y + dragDelta.Y);

                    model.ConnectorBottom.Center = new Point(model.ConnectorBottom.Center.X + dragDelta.X, model.ConnectorBottom.Center.Y);
                    model.ConnectorBottom.Center = new Point(model.ConnectorBottom.Center.X, model.ConnectorBottom.Center.Y + dragDelta.Y);
                }

                if(model is LinePointViewModel)
                {
                    model.Center = new Point(model.Center.X + dragDelta.X, model.Center.Y);
                    model.Center = new Point(model.Center.X, model.Center.Y + dragDelta.Y);
                }

                CheckLeftBorder(model);
                CheckRightBorder(model);
                CheckTopBorder(model);
                CheckBottomBorder(model);
            }
        }        
        private void CheckConnectionCenter(BaseShapeViewModel model)
        {
            model.ConnectorLeft.Center = new Point(model.Left - 10, model.Top + model.Height / 2);
            model.ConnectorRight.Center = new Point(model.Left + model.Width + 10, model.Top + model.Height / 2);
            model.ConnectorTop.Center = new Point(model.Left + model.Width / 2, model.Top - 10);
            model.ConnectorBottom.Center = new Point(model.Left + model.Width / 2, model.Top + model.Height + 10);
        }
    }
}

