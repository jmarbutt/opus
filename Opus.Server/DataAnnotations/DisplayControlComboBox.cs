
namespace Opus.DataAnnotations
{
    public class DisplayControlComboBox : DisplayControlBase
    {
        public DisplayControlComboBox()
        {
            DisplayType = DisplayTypes.ComboBox;
        }

        public string ItemSourcePath { get; set; }
        public string ValueMemberPath { get; set; }
        public string ViewModelResourceName { get; set; }

        public override object GetInputControl(object binding)
        {
            return null;
        }
    }
}