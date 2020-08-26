using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLDiagrams.Model;

namespace UMLDiagrams.ViewModel
{
    abstract public class BaseShapeViewModel : BaseViewModel
    {

        public BaseShapeViewModel()
        {

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

        private int width;
        public int Width
        {
            get => width;
            set => SetField(ref width, value, nameof(Width));
        }

        private int height;
        public int Height
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

        private string text;
        public string Text
        {
            get => text;
            set => SetField(ref text, value, nameof(Text));
        }

    }

}
