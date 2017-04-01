

namespace Opus.DataAnnotations
{
    public class DisplayControlCheckbox : DisplayControlBase
    {
        public DisplayControlCheckbox()
        {
            DisplayType = DisplayTypes.Checkbox;
        }

        public string Name { get; set; }
        public override object GetInputControl(object binding)
        {
            
            return null;
        }
    }
}