using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UMLDiagrams
{
    public class Timeline : ItemsControl
    {
        protected override System.Windows.DependencyObject GetContainerForItemOverride()
        {            
            return new ContentControl();
        }

        protected override void PrepareContainerForItemOverride(System.Windows.DependencyObject element, object item)
        {
            var control = element as Control;
            if (control != null)
            {
                var myUri = new Uri("/Resources/DesignerItem.xaml", UriKind.Relative);
                var dictionary = Application.LoadComponent(myUri) as ResourceDictionary;

                if (dictionary != null)
                {
                    //control.Template = (ControlTemplate)dictionary["MoveThumbTemplate"];
                    //control.Style = (Style)dictionary["DesignerItemStyle"];
                }

                control.DataContext = item;
            }
            
            base.PrepareContainerForItemOverride(element, item);
        }
    }
}
