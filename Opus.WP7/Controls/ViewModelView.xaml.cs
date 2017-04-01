using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Opus.Controls;
using Opus.DataAnnotations;
using Opus.Interfaces;

namespace Opus.WP7.Controls
{
    public partial class ViewModelView
    {
        public ViewModelView()
        {
            InitializeComponent();
            Loaded += ViewModelView_Loaded;
        }

        private ObservableCollection<FieldGrouping> _fieldGroups;

        private IViewModelView _viewModelView;

        void ViewModelView_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataContext();
        }

        private void LoadDataContext()
        {


            HorizontalAlignment = HorizontalAlignment.Stretch;
            VerticalAlignment = VerticalAlignment.Stretch;



            LoadViewControl();
            if (DataContext == null) return;


            LoadFieldGroups(DataContext.GetType(), "");

            AddFieldGroups();

            LoadCommands(DataContext);


        }

        private void LoadFieldGroups(Type objectType, string parentPropertyName)
        {
            if (_fieldGroups == null) _fieldGroups = new ObservableCollection<FieldGrouping>();

            //Get Metadata Type class if available
            var metadataClass = GetMetadataType(objectType);
            if (metadataClass != null) LoadFieldGroups(metadataClass.MetaDataType, parentPropertyName);

            var blankGroup = new FieldGrouping
            {
                Name = "",
                Order = -99,
                Fields = LoadFields("", objectType, parentPropertyName)
            };

            if (blankGroup.Fields.Count() > 0)
                _fieldGroups.Add(blankGroup);

            var attributes = objectType.GetCustomAttributes(true);
            foreach (var groupattribute in attributes.OfType<FieldGrouping>().OrderBy(fg => fg.Order))
            {
                var currentgroup = GetFieldGroup(groupattribute);
                if (currentgroup.Fields == null)
                    currentgroup.Fields = new ObservableCollection<DisplayControlBase>();
                foreach (var field in LoadFields(currentgroup.Name, objectType, parentPropertyName))
                    currentgroup.Fields.Add(field);
            }

            //Load Children Groups
            var members = objectType.GetMembers();
            var importChildren = GetImportedChildren(members);

            foreach (var importChild in importChildren)
            {
                var childtype = importChild.MetadataType ?? objectType.GetProperty(importChild.PropertyPath).PropertyType;
                LoadFieldGroups(childtype, parentPropertyName + objectType.GetProperty(importChild.PropertyPath).Name + ".");
            }
        }

        /// <summary>
        /// This will return with an existing field group if the field group is already in the current collection
        /// </summary>
        /// <param name="currentFieldGroup"></param>
        /// <returns></returns>
        private FieldGrouping GetFieldGroup(FieldGrouping currentFieldGroup)
        {
            var existingGroup = from g in _fieldGroups
                                where g.Name == currentFieldGroup.Name
                                select g;
            if (existingGroup.Count() == 0)
            {
                _fieldGroups.Add(currentFieldGroup);
                return currentFieldGroup;
            }
            return existingGroup.ToList()[0];
        }

        private void AddFieldGroups()
        {
            var groups = from g in _fieldGroups
                         orderby g.Order
                         select g;

            foreach (var fieldGrouping in groups)
                _viewModelView.LoadFieldGroup(fieldGrouping);
        }


        private void LoadCommands(object currentObject)
        {
            var members = currentObject.GetType().GetMembers();
            var attributes = new ObservableCollection<DisplayCommand>();
            foreach (var member in members)
            {
                var attribute = GetViewModelCommand(member);
                if (attribute == null) continue;
                attribute.PropertyPath = member.Name;


                if (attribute.ValidateBeforeExecuting)
                {
                    // When you need to call validate before you call a command, you must do it this way
                    var validateCommand = new RelayCommand<object>(ValidateThenCallCommand);
                    attribute.Command = validateCommand;
                    attribute.CommandParameter =
                        currentObject.GetType().GetProperty(attribute.PropertyPath).GetValue(currentObject, null);
                }
                else
                {
                    attribute.Command =
                        (ICommand)
                        currentObject.GetType().GetProperty(attribute.PropertyPath).GetValue(currentObject, null);
                }

                attributes.Add(attribute);
            }
            ((Control)_viewModelView).HorizontalAlignment = HorizontalAlignment.Stretch;
            ((Control)_viewModelView).VerticalAlignment = VerticalAlignment.Stretch;

            _viewModelView.LoadCommands(attributes);
        }


        private void ValidateThenCallCommand(object obj)
        {
            //if (!EditForm.Validate()) return;
            var cmd = obj as ICommand;
            if (cmd != null) cmd.Execute(null);
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

        private static ObservableCollection<DisplayControlBase> LoadFields(string groupName, Type objectType, string parentPropertyName)
        {
            var members = objectType.GetMembers();
            var displayControlBases = GetDisplayControlAttributes(members);
            var importChildren = GetImportedChildren(members);

            foreach (var importChild in importChildren)
            {

                LoadFields(groupName,
                           objectType.GetProperty(importChild.PropertyPath).PropertyType, parentPropertyName + objectType.GetProperty(importChild.PropertyPath).Name + ".");

            }

            var fields = new ObservableCollection<DisplayControlBase>();
            var displayAttributesOrdered = from d in displayControlBases
                                           where (d.Group == groupName) || (d.Group == null && groupName == "")
                                                 && d.DisplayType != DisplayTypes.Command
                                           orderby d.Row, d.Order
                                           select d;

            foreach (var displayAttribute in displayAttributesOrdered)
            {
                displayAttribute.PropertyPath = parentPropertyName + displayAttribute.PropertyPath;
                fields.Add(displayAttribute);
            }
            return fields;
        }

        private static ObservableCollection<ImportChild> GetImportedChildren(IEnumerable<MemberInfo> members)
        {
            var importedChildren = new ObservableCollection<ImportChild>();
            foreach (var member in members)
            {
                var importChild = GetImportedChildAttribute(member);
                if (importChild == null) continue;
                importChild.PropertyPath = member.Name;
                importedChildren.Add(importChild);
            }
            return importedChildren;
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

        private void LoadViewControl()
        {


            // Get View Setting
            var attributes = DataContext.GetType().GetCustomAttributes(true);

            if (attributes.OfType<ViewSettings>().Count() > 0)
            {
                var viewsetting = attributes.OfType<ViewSettings>().First();
                _viewModelView = Activator.CreateInstance(viewsetting.EntityViewType) as IViewModelView;

                if (_viewModelView == null)
                    throw new Exception("Custom Views must support the IViewModelView interface");



            }
            else
                _viewModelView = new GenericDataView();
            LayoutRoot.Children.Add((UIElement)_viewModelView);

        }

        private static DisplayControlBase GetDisplayControlAttribute(MemberInfo type)
        {
            var attributes = type.GetCustomAttributes(true);
            return attributes.OfType<DisplayControlBase>().FirstOrDefault();
        }

        private static ImportChild GetImportedChildAttribute(MemberInfo type)
        {
            var attributes = type.GetCustomAttributes(true);
            return attributes.OfType<ImportChild>().FirstOrDefault();
        }

        private static DisplayCommand GetViewModelCommand(MemberInfo type)
        {
            var attributes = type.GetCustomAttributes(true);
            return attributes.OfType<DisplayCommand>().FirstOrDefault();
        }
    }
}
