using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace UMLDiagrams.ViewModel
{
    public class ConnectionViewModel : BaseShapeViewModel
    {        
        private BaseShapeViewModel shape1;
        private BaseShapeViewModel shape2;      
        public BaseShapeViewModel Shape1
        {
            get => shape1;
            set => SetField(ref shape1, value, nameof(Shape1));
        }
        public BaseShapeViewModel Shape2
        {
            get => shape2;
            set => SetField(ref shape2, value, nameof(Shape2));
        }        
    }
}
