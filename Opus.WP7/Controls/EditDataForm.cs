using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Opus.Controls
{
    public class EditDataForm : ContentControl
    {
        public static readonly DependencyProperty CurrentItemProperty =
            DependencyProperty.Register("CurrentItem", typeof (object), typeof (EditDataForm),
                                        new PropertyMetadata(CurrentItemChanged));


        public static readonly DependencyProperty SaveCommandProperty =
            DependencyProperty.Register("SaveCommand", typeof (ICommand), typeof (EditDataForm),
                                        null);


        public readonly StackPanel EditingPanel = new StackPanel();

        private static void CurrentItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(CurrentItemProperty, e.NewValue);
            var editingControl = d as EditDataForm;
            if (editingControl != null) editingControl.EditingPanel.DataContext = e.NewValue;
        }

        public object CurrentItem
        {
            get { return GetValue(CurrentItemProperty); }
            set
            {
                if (DesignerProperties.IsInDesignTool)
                    DataContext = value;
                else
                {
                    SetValue(CurrentItemProperty, value);
                    EditingPanel.DataContext = value;
                }
            }
        }


        public bool Validate()
        {
            //TODO Complete
            return true;
        }
    }
}