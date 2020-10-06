using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UMLDiagrams.Model;

namespace UMLDiagrams.ViewModel
{
    abstract public class BaseShapeViewModel : BaseViewModel
    {

        private ConnectorViewModel connectorLeft;
        public ConnectorViewModel ConnectorLeft
        {
            get => connectorLeft;
            set => SetField(ref connectorLeft, value, nameof(ConnectorLeft));
        }

        private ConnectorViewModel connectorRight;
        public ConnectorViewModel ConnectorRight
        {
            get => connectorRight;
            set => SetField(ref connectorRight, value, nameof(ConnectorRight));
        }

        private ConnectorViewModel connectorTop;
        public ConnectorViewModel ConnectorTop
        {
            get => connectorTop;
            set => SetField(ref connectorTop, value, nameof(ConnectorTop));
        }

        private ConnectorViewModel connectorBottom;
        public ConnectorViewModel ConnectorBottom
        {
            get => connectorBottom;
            set => SetField(ref connectorBottom, value, nameof(ConnectorBottom));
        }
               

        private Point center;
        public Point Center
        {
            get => center;
            set => SetField(ref center, value, nameof(Center));
        }
        
        //private double centerX;
        //public double CenterX
        //{
        //    get => centerX;
        //    set => SetField(ref centerX, value, nameof(CenterX));
        //}
       
        
        //private double centerY;
        //public double CenterY
        //{
        //    get => centerY;
        //    set => SetField(ref centerY, value, nameof(CenterY));
        //}        


        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set => SetField(ref isSelected, value, nameof(IsSelected));
        }


        private double zindex;
        public double ZIndex
        {
            get => zindex;
            set => SetField(ref zindex, value, nameof(ZIndex));
        }


        private double angle;
        public double Angle
        {
            get => angle;
            set => SetField(ref angle, value, nameof(Angle));
        }


        private double left;
        public double Left
        {
            get => left;
            set => SetField(ref left, value, nameof(Left));
        }


        private double top;
        public double Top
        {
            get => top;
            set => SetField(ref top, value, nameof(Top));
        }


        private double width;
        public double Width
        {
            get => width;
            set => SetField(ref width, value, nameof(Width));
        }


        private double height;
        public double Height
        {
            get => height;
            set => SetField(ref height, value, nameof(Height));
        }


        private string fill;
        public string Fill
        {
            get => fill;
            set => SetField(ref fill, value, nameof(Fill));
        }
        

        private string stroke;
        public string Stroke
        {
            get => stroke;
            set => SetField(ref stroke, value, nameof(Stroke));
        }

    }

}
