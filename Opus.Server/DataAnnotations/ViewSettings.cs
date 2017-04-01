using System;

namespace Opus.DataAnnotations
{
    public class ViewSettings : Attribute
    {
        public string Name { get; set; }
        public Type EntityViewType { get; set; }

        public ViewSettings()
        {
            
        }
    }
}