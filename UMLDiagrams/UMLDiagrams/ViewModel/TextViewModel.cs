using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLDiagrams.ViewModel
{
    public class TextViewModel : BaseShapeViewModel
    {
        private string text;
        public string Text
        {
            get => text;
            set => SetField(ref text, value, nameof(Text));
        }
    }
}
