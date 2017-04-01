using System;
using System.Windows;
using System.Windows.Data;

namespace Opus.DataAnnotations
{
    public class DisplayControlHyperLinkButton : DisplayControlBase
    {
        public string CommandPath { get; set; }
        public override FrameworkElement GetInputControl(Binding binding)
        {
            return null;
        }
    }
}