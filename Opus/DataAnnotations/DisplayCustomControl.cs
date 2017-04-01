using System;
using System.Windows;
using System.Windows.Data;

namespace Opus.DataAnnotations
{
    public class DisplayCustomControl : DisplayControlBase
    {
        public Type ControlType { get; set; }

        protected DependencyProperty ControlProperty { get; set; }

        public override FrameworkElement GetInputControl(Binding binding)
        {
            if (ControlType == null)
            {
                return null;
            }

            var control = Activator.CreateInstance(ControlType) as FrameworkElement;
            if (control != null)
            {
                if (ControlProperty != null) control.SetBinding(ControlProperty, binding);

                if (Height != 0) control.Height = Height;
                if (Width != 0) control.Width = Width;
                return control;
            }
            return null;
        }
    }
}