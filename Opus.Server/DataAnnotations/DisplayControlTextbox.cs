

namespace Opus.DataAnnotations
{
    public class DisplayControlTextbox : DisplayControlBase
    {
        public DisplayControlTextbox()
        {
            DisplayType = DisplayTypes.Textbox;
        }

        public override object GetInputControl(object binding)
        {
            return null;
        }

        public object InputScope { get; set; }
    }
}