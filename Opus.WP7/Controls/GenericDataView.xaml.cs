using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Opus.DataAnnotations;
using Opus.Interfaces;

namespace Opus.Controls
{
    public partial class GenericDataView : IViewModelView
    {
        public GenericDataView()
        {
            InitializeComponent();
            
        }

        #region IViewModelView Members

        public void LoadFieldGroup(FieldGrouping fieldGroup)
        {
            var pivotItem = new PivotItem();
            pivotItem.Header = fieldGroup.Name;
            var scroller = new ScrollViewer();
            var stackpanel = new StackPanel();
            foreach (var displayControlBase in fieldGroup.Fields)
            {
                var lbl = new Label {Content = displayControlBase.Name};

                stackpanel.Children.Add(lbl);

                var binding = new Binding(displayControlBase.PropertyPath)
                                  {Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.Default};

                var control = displayControlBase.GetInputControl(binding);
                if (control != null)
                {
                    control.Width = 480;
                    stackpanel.Children.Add(control);
                }
            }
            scroller.Content = stackpanel;
            pivotItem.Content = scroller;
            pivot.Items.Add(pivotItem);
        }

        public void LoadCommands(IEnumerable<DisplayCommand> commands)
        {
            foreach (var displayCommand in commands)
            {
                var btn = new ApplicationBarMenuItem {Text = displayCommand.Name};

                ApplicationBar.MenuItems.Add(btn);
            }
        }

        #endregion
    }
}