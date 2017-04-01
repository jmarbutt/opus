using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Opus.DataAnnotations
{
    public class DisplayCommand : DisplayControlBase
    {
        public bool ValidateBeforeExecuting { get; set; }
        public ICommand Command { get; set; }
        public object CommandParameter { get; set; }

        public DisplayCommand()
        {
            DisplayType = DisplayTypes.Command;
        }

        public override FrameworkElement GetInputControl(Binding binding)
        {
            return null; 
        }
    }
}