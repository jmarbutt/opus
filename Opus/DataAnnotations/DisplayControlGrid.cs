using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using DataGrid = Opus.Controls.DataGrid;

namespace Opus.DataAnnotations
{
    public class DisplayControlGrid : DisplayControlBase
    {

        public DisplayControlGrid()
        {
            DisplayType = DisplayTypes.Grid;
            PlaceInDataField = false;
            LabelPosition = LabelPosition.Top;
        }

        public Type EntityType { get; set; }
        public bool IsEditable { get; set; }
        public string SelectedRowPropertyPath { get; set; }

        #region "Command Properties"

        public string AddCommandPath { get; set; }
        public string EditCommandPath { get; set; }
        public string DeleteCommandPath { get; set; }

        #endregion

        public override FrameworkElement GetInputControl(Binding binding)
        {
            var dataGrid = new DataGrid();
            dataGrid.dataGrid.SetBinding(System.Windows.Controls.DataGrid.ItemsSourceProperty, binding);
            dataGrid.dataGrid.AutoGenerateColumns = false;
            if (!string.IsNullOrEmpty(SelectedRowPropertyPath))
            {
                var selectedBinding = new Binding(SelectedRowPropertyPath) {Mode = BindingMode.TwoWay};
                dataGrid.dataGrid.SetBinding(System.Windows.Controls.DataGrid.SelectedItemProperty, selectedBinding);
            }

            ////////////////////////////////
            // Command Binding            
            SetCommandBinding(dataGrid, AddCommandPath, DataGrid.AddCommandProperty, dataGrid.btnAdd);
            SetCommandBinding(dataGrid, EditCommandPath, DataGrid.EditCommandProperty, dataGrid.btnEdit);
            SetCommandBinding(dataGrid, DeleteCommandPath, DataGrid.DeleteCommandProperty, dataGrid.btnDelete);
            // End Command Binding 
            ////////////////////////////////
            
            var metadataType = GetMetadataType(EntityType);

            IEnumerable<DisplayControlBase> displayControlBases;
            if (metadataType != null)
            {
                //When using MetaData defined types, pull just from the meta data
                var members = metadataType.MetaDataType.GetMembers();
                displayControlBases = GetDisplayControlAttributes(members);
            }
            else
            {
                var members = EntityType.GetMembers();
                displayControlBases = GetDisplayControlAttributes(members);
            }

            foreach (var displayControlBase in displayControlBases.OrderBy(o => o.GridOrder))
            {
                if (!displayControlBase.IsVisibleInGrid) continue;
                var nDataGridCol = new DataGridTextColumn
                                       {
                                           Binding = new Binding
                                                         {
                                                             Path =
                                                                 new PropertyPath(displayControlBase.PropertyPath),
                                                             StringFormat = displayControlBase.StringFormat,
                                                             Mode = BindingMode.TwoWay
                                                         },
                                           Header = displayControlBase.Name
                                       };

                if (displayControlBase.DisplayType == DisplayTypes.ComboBox)
                {
                    var cmbProp = displayControlBase as DisplayControlComboBox;
                    if (cmbProp != null && cmbProp.LookupPath != null)
                        nDataGridCol.Binding.Path = new PropertyPath(cmbProp.LookupPath);
                }

                dataGrid.dataGrid.Columns.Add(nDataGridCol);
            }

            return dataGrid;
        }

        private void SetCommandBinding(DataGrid dataGrid, string commandPath, DependencyProperty dependencyProperty, Button button)
        {
            if (!string.IsNullOrEmpty(commandPath))
            {
                var cmdBinding = new Binding(commandPath) { Mode = BindingMode.TwoWay };
                //dataGrid.SetBinding(dependencyProperty, cmdBinding);                
                button.SetBinding(ButtonBase.CommandProperty, cmdBinding);
            }
            else
            {
                button.Visibility = Visibility.Collapsed;
            }
        }

        private static IEnumerable<DisplayControlBase> GetDisplayControlAttributes(IEnumerable<MemberInfo> members)
        {
            var displayAttributes = new ObservableCollection<DisplayControlBase>();
            foreach (var member in members)
            {
                var displayAttribute = GetDisplayControlAttribute(member);
                if (displayAttribute == null) continue;
                displayAttribute.PropertyPath = member.Name;
                displayAttributes.Add(displayAttribute);
            }
            return displayAttributes;
        }

        private static DisplayControlBase GetDisplayControlAttribute(MemberInfo type)
        {
            var attributes = type.GetCustomAttributes(true);
            return attributes.OfType<DisplayControlBase>().FirstOrDefault();
        }

        public MetadataTypeAttribute GetMetadataType(Type objectType)
        {
            var attributes = objectType.GetCustomAttributes(true);

            if (attributes.OfType<MetadataTypeAttribute>().Count() > 0)
            {
                var metadataTypeAttribute = attributes.OfType<MetadataTypeAttribute>().First();
                return metadataTypeAttribute;
            }
            return null;
        }
    }
}