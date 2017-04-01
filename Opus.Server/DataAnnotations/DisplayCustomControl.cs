using System;


namespace Opus.DataAnnotations
{
    public class DisplayCustomControl : DisplayControlBase
    {
        public Type ControlType { get; set; }



        public override object GetInputControl(object binding)
        {
        
            return null;
        }
    }
}