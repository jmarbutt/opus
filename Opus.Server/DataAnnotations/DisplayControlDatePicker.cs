

namespace Opus.DataAnnotations
{
    public class DisplayControlDatePicker : DisplayControlBase
    {
        public DisplayControlDatePicker()
        {
            DisplayType = DisplayTypes.Date;
        }

        public override object GetInputControl(object binding)
        {
            return null;
        }
    }
}