

namespace Opus.DataAnnotations
{
    public class DisplayControlLabel : DisplayControlBase
    {
        public DisplayControlLabel()
        {
            DisplayType = DisplayTypes.Label;
        }

        public override object GetInputControl(object binding)
        {
            return null;

        }
    }
}