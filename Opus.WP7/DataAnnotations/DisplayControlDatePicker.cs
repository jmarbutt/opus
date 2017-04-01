using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Opus.DataAnnotations
{
    public class DisplayControlDatePicker : DisplayControlBase
    {
        public DisplayControlDatePicker()
        {
            DisplayType = DisplayTypes.Date;
        }

        public override FrameworkElement GetInputControl(Binding binding)
        {
            var textBox = new TextBox {Width = Width};
            textBox.SetBinding(TextBox.TextProperty, binding);            
            return textBox;
        }
    }
}