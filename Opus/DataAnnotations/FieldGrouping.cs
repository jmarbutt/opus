using System;

namespace Opus.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class FieldGrouping : Attribute
    {
        public FieldGrouping()
        {
            LastChildFill = false;
        }

        public string Name { get; set; }
        public int Order { get; set; }
        public System.Collections.ObjectModel.ObservableCollection<DisplayControlBase> Fields { get; set; }

        public bool LastChildFill { get; set; }
    }
}