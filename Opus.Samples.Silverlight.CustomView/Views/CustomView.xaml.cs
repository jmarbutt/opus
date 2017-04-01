using System.Collections.Generic;
using System.Windows.Controls;
using Opus.Controls;
using Opus.DataAnnotations;
using Opus.Interfaces;

namespace Opus.Samples.Silverlight.CustomView.Views
{
    public partial class CustomView : IViewModelView
    {
        public CustomView()
        {
            InitializeComponent();
        }

        #region IViewModelView Members

        public void LoadFieldGroup(FieldGrouping fieldGroup)
        {
            var pnl = ControlHelper.LoadFieldGroupPanel(fieldGroup);
            var accordian = new AccordionItem {Header = fieldGroup.Name, Content = pnl};

            FieldsPanel.Items.Add(accordian);
        }

        public void LoadCommands(IEnumerable<DisplayCommand> commands)
        {
            foreach (var displayCommand in commands)
            {
                var btn = new Button
                              {
                                  Content = displayCommand.Name,
                                  Command = displayCommand.Command,
                                  CommandParameter = displayCommand.CommandParameter
                              };

                CommandsPanel.Children.Add(btn);
            }
        }

        #endregion
    }
}