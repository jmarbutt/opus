using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;


namespace Opus.DataAnnotations
{
    public class DisplayControlGrid : DisplayControlBase
    {
        public DisplayControlGrid()
        {
            DisplayType = DisplayTypes.Grid;
            Width = 550;
            Height = 300;
            LabelPosition = LabelPosition.Top;
        }

        public Type EntityType { get; set; }
        public bool IsEditable { get; set; }

        public override object GetInputControl(object binding)
        {
            return null;
        }

       
    }
}