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
            StringFormat = "{0:d}";
        }

        public override FrameworkElement GetInputControl(Binding binding)
        {
            var datePicker = new DatePicker();
            datePicker.SetBinding(DatePicker.SelectedDateProperty, binding);

            return datePicker;
        }
    }
}