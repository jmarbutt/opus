using System;
using System.Windows;
using System.Windows.Data;
using Opus.Controls;

namespace Opus.DataAnnotations
{
    public class DisplayControlGrid : DisplayControlBase
    {
        public DisplayControlGrid()
        {
            DisplayType = DisplayTypes.Grid;
        }

        public Type EntityType { get; set; }
        public bool IsEditable { get; set; }
        public override FrameworkElement GetInputControl(Binding binding)
        {
         
            return null;
        }
    }
}