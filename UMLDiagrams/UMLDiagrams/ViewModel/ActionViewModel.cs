using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLDiagrams.ViewModel
{
    public class ActionViewModel : BaseShapeViewModel
    {
        

        private double radiusX;
        public double RadiusX
        {
            get => radiusX;
            set => SetField(ref radiusX, value, nameof(RadiusX));
        }

        private double radiusY;
        public double RadiusY
        {
            get => radiusY;
            set => SetField(ref radiusY, value, nameof(RadiusY));
        }
    }
}
