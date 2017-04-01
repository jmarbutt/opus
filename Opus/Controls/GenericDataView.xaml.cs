using System.Collections.Generic;
using System.Windows.Controls;
using Opus.DataAnnotations;
using Opus.Interfaces;

namespace Opus.Controls
{
    internal partial class GenericDataView : IViewModelView
    {
        
        private int _groupCount;

        private TabControl _tabControl;

        public GenericDataView()
        {
            InitializeComponent();

            this.Loaded += new System.Windows.RoutedEventHandler(GenericDataView_Loaded);
        }

        void GenericDataView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            
        }

        #region IViewModelView Members

        public void LoadFieldGroup(FieldGrouping fieldGroup)
        {
            if (_groupCount == 0)
            {
                
                FieldsPanel.LastChildFill = true;
                var toppanel = ControlHelper.LoadFieldGroupPanel(fieldGroup);
                DockPanel.SetDock(toppanel, Dock.Top);
                FieldsPanel.Children.Add(toppanel);
            }
            else
            {
                if (_tabControl == null)
                {
                    _tabControl = new TabControl();
                    FieldsPanel.Children.Add(_tabControl);
                    DockPanel.SetDock(_tabControl, Dock.Top);
                }

                var ntab = new TabItem
                               {
                                   Header = fieldGroup.Name,
                                   Content = ControlHelper.LoadFieldGroupPanel(fieldGroup)
                               };

                _tabControl.Items.Add(ntab);
            }
            _groupCount++;
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


                CommandPanel.Children.Add(btn);
            }
        }

        #endregion
    }
}