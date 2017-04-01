using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Opus.DataAnnotations
{
    public class DisplayControlLabel : DisplayControlBase
    {
        public DisplayControlLabel()
        {
            DisplayType = DisplayTypes.Label;
        }

        public override FrameworkElement GetInputControl(Binding binding)
        {
            var label = new Label() { Width = Width };
            label.SetBinding(ContentControl.ContentProperty, binding);

            return label;
            
        }
    }
}