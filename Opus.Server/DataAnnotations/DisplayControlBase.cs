using System;


namespace Opus.DataAnnotations
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public abstract class DisplayControlBase : Attribute
    {
        protected DisplayControlBase()
        {
            Width = 100;
        }

        public int Order { get; set; }
        public int Row { get; set; }
        public bool AutoGenerateField { get; set; }

        public string PropertyPath { get; set; }

        public object Margin { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public DisplayTypes DisplayType { get; protected set; }
        public string StringFormat { get; set; }

        public string Group { get; set; }

        public string Name { get; set; }

        public abstract object GetInputControl(object binding);

        public LabelPosition LabelPosition { get; set; }
    }
}