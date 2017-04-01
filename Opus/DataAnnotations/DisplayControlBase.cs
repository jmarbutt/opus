using System;
using System.Windows;
using System.Windows.Data;

namespace Opus.DataAnnotations
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public abstract class DisplayControlBase : Attribute
    {
        protected DisplayControlBase()
        {
            Width = 100;
            PlaceInDataField = true;
            LabelVisible = true;
            GridOrder = 999999;
        }

        public int Order { get; set; }
        public int Row { get; set; }
        public bool AutoGenerateField { get; set; }

        public string PropertyPath { get; set; }

        public int GridOrder { get; set; }
        public Thickness Margin { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public DisplayTypes DisplayType { get; protected set; }
        public string StringFormat { get; set; }

        public string Group { get; set; }

        public string Name { get; set; }

        public LabelPosition LabelPosition { get; set; }
        public bool LabelVisible { get; set; }

        public string UniqueId { get; set; }
        public abstract FrameworkElement GetInputControl(Binding binding);

        public bool IsVisibleInGrid;

        public bool PlaceInDataField { get; set; }
    }
}