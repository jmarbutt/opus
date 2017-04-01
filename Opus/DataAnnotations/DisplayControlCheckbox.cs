using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Opus.DataAnnotations
{
    public class DisplayControlCheckbox : DisplayControlBase
    {
        public DisplayControlCheckbox()
        {
            DisplayType = DisplayTypes.Checkbox;
        }

        public string Name { get; set; }
        public override FrameworkElement GetInputControl(Binding binding)
        {
            var checkbox = new CheckBox();
            checkbox.SetBinding(ToggleButton.IsCheckedProperty, binding);

            checkbox.Content = Name;
            return checkbox;
        }
    }
}