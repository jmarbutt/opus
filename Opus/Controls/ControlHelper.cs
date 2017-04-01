using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Opus.DataAnnotations;

namespace Opus.Controls
{
    public static class ControlHelper
    {
        public static DockPanel LoadFieldGroupPanel(FieldGrouping fieldGroup)
        {
            var returnPanel = new DockPanel {LastChildFill = fieldGroup.LastChildFill};
            returnPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
            returnPanel.VerticalAlignment = VerticalAlignment.Stretch;
            
            var lastRow = -1;
            var currentPanel = new DockPanel();

            if (fieldGroup.Fields != null)
            {
                foreach (var displayAttribute in
                    fieldGroup.Fields.Where(displayAttribute => displayAttribute.DisplayType != DisplayTypes.Command))
                {
                    if (lastRow != displayAttribute.Row)
                    {
                        lastRow = displayAttribute.Row;
                        currentPanel = new DockPanel ();
                        returnPanel.Children.Add(currentPanel);
                        currentPanel.LastChildFill = fieldGroup.LastChildFill;
                        DockPanel.SetDock(currentPanel, Dock.Top);
                    }


                    var dataField = new DataField
                                        {
                                            PropertyPath = displayAttribute.PropertyPath
                                        };

                    dataField.LabelVisibility = displayAttribute.LabelVisible ? Visibility.Visible : Visibility.Collapsed;

                    switch (displayAttribute.LabelPosition)
                    {
                        case LabelPosition.Left:
                            dataField.LabelPosition = DataFieldLabelPosition.Left;
                            break;
                        case LabelPosition.Top:
                            dataField.LabelPosition = DataFieldLabelPosition.Top;
                            break;
                        default:
                            dataField.LabelPosition = DataFieldLabelPosition.Auto;
                            break;
                    }

                    if (displayAttribute.Name != null)
                        dataField.Label = displayAttribute.Name;

                    if (displayAttribute.DisplayType == DisplayTypes.RadioButton)
                        dataField.DescriptionViewerVisibility = Visibility.Collapsed;

                    var binding = new Binding(displayAttribute.PropertyPath) { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.Default, StringFormat = displayAttribute.StringFormat, NotifyOnValidationError = true, ValidatesOnExceptions = true };

                    

                    if (displayAttribute.PlaceInDataField)
                    {
                        dataField.Content = displayAttribute.GetInputControl(binding);

                        DockPanel.SetDock(dataField, Dock.Left);

                        if (displayAttribute.DisplayType == DisplayTypes.Checkbox)
                            dataField.LabelVisibility = Visibility.Collapsed;

                        dataField.HorizontalAlignment = HorizontalAlignment.Stretch;
                        dataField.VerticalAlignment = VerticalAlignment.Stretch;

                        currentPanel.Children.Add(dataField);
                    }
                    else
                    {
                        var ctrl = displayAttribute.GetInputControl(binding);
                        DockPanel.SetDock(ctrl, Dock.Left);
                        currentPanel.Children.Add(ctrl);
                        
                    }
                }
            }

            
            return returnPanel;
        }
    }
}