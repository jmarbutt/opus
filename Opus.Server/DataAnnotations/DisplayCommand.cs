
namespace Opus.DataAnnotations
{
    public class DisplayCommand : DisplayControlBase
    {
        public bool ValidateBeforeExecuting { get; set; }
        public object Command { get; set; }
        public object CommandParameter { get; set; }

        public DisplayCommand()
        {
            DisplayType = DisplayTypes.Command;
        }

        public override object GetInputControl(object binding)
        {
            return null; 
        }
    }
}