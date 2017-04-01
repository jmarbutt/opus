
namespace Opus.DataAnnotations
{
    public class DisplayControlHyperLinkButton : DisplayControlBase
    {
        public string CommandPath { get; set; }
        public override object GetInputControl(object binding)
        {
            return null;
        }
    }
}