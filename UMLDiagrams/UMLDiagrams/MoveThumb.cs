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
            //MouseLeftButtonDown += new MouseButtonEventHandler(Select_MouseEnter);
            PreviewMouseLeftButtonDown += new MouseButtonEventHandler(this.Select_MouseEnter);
        }

        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ContentControl designerItem = DataContext as ContentControl;                 
            
            if (designerItem != null)
            {
                Point dragDelta = new Point(e.HorizontalChange, e.VerticalChange);

                RotateTransform rotateTransform = designerItem.RenderTransform as RotateTransform;
                if (rotateTransform != null)
                {
                    dragDelta = rotateTransform.Transform(dragDelta);
                }
                var model = designerItem.DataContext as BaseShapeViewModel;
                if (model.Left >= 0 && model.Top >= 0 && model.Left+model.Width <= 3000 && model.Top+model.Height <=2000 )
                {
                    model.Left += dragDelta.X;
                    model.Top += dragDelta.Y;
                }
                if (model.Left < 0)
                {
                    model.Left = 0.1;                    
                }
                if (model.Left + model.Width > 3000)
                {
                    model.Left = 2999.9 - model.Width;
                }
                if (model.Top < 0)
                {
                    model.Top = 0.1 ;
                }
                if (model.Top + model.Height > 2000)
                {
                    model.Top = 1999.9 - model.Height;
                }
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
            model.ZIndex += 1;
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (model.IsSelected != true)
                {
                    model.IsSelected = true;                  
                }
                else
                {
                    model.IsSelected = false;
                }
            }
        }

    }
}

