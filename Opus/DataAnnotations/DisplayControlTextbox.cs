using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Opus.DataAnnotations
{
    public class DisplayControlTextbox : DisplayControlBase
    {
        public DisplayControlTextbox()
        {
            DisplayType = DisplayTypes.Textbox;
        }

        public override FrameworkElement GetInputControl(Binding binding)
        {
            var textBox = new TextBox { Width = Width };
            textBox.SetBinding(TextBox.TextProperty, binding);       

            return textBox;
        }

        public InputScopeNameValue InputScope { get; set; }
    }
}