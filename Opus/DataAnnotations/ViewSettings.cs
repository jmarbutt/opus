using System;
using Opus.Controls;

namespace Opus.DataAnnotations
{
    public class ViewSettings : Attribute
    {
        public ViewSettings()
        {
            EntityViewType = typeof (GenericDataView);
        }

        public string Name { get; set; }
        public Type EntityViewType { get; set; }

        public double Width { get; set; }
        public double Height { get; set; }
    }
}