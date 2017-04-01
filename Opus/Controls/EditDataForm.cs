using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight.Command;

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

        private Grid _layoutroot;

        private ValidationSummary _validationSummary;

        public EditDataForm()
        {
            var dictionary = new ResourceDictionary();
            if (DesignerProperties.IsInDesignTool)
            {
            }
            else
            {
                dictionary.Source = new Uri("/Opus;component/Themes/Generic.xaml", UriKind.RelativeOrAbsolute);
            }
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
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
                    DataContext = value;
                }
            }
        }

        public ICommand SaveCommand
        {
            get { return (ICommand) GetValue(SaveCommandProperty); }
            set { SetValue(SaveCommandProperty, value); }
        }

        public RelayCommand SaveAfterValidateCommand
        {
            get
            {
                return SaveCommand != null
                           ? new RelayCommand(ValidateThenSave, () => SaveCommand.CanExecute(null))
                           : null;
            }
        }

        private static void CurrentItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(CurrentItemProperty, e.NewValue);
            var editingControl = d as EditDataForm;
            if (editingControl != null) editingControl.CurrentItem = e.NewValue;
        }

        private void ValidateThenSave()
        {
            if (Validate())
                SaveCommand.Execute(null);
        }

        public override void OnApplyTemplate()
        {
            if (DesignerProperties.IsInDesignTool) return; // Do not perform changing content at design time.


            _validationSummary = (ValidationSummary) GetTemplateChild("validationSummary");

            _layoutroot = GetTemplateChild("LayoutRoot") as Grid;
        }


        public bool Validate()
        {
            var results = new ValidationResultCollection();


            Validator.TryValidateObject(CurrentItem, new ValidationContext(CurrentItem, null, null), results, true);
            _validationSummary.Errors.Clear();

            SetErrorsForControls(_layoutroot, results);

            return results.Count == 0;
        }

        private void SetErrorsForControls(DependencyObject parentControl, IEnumerable<ValidationResult> errorResults)
        {
            var childControlCount = VisualTreeHelper.GetChildrenCount(parentControl);
            for (var i = 0; i < childControlCount; i++)
            {
                var childControl = VisualTreeHelper.GetChild(parentControl, i);
                var binding = GetBinding(childControl);
                if (binding == null)
                {
                }
                else
                {
                    var filteredErrorResults = from e in errorResults
                                               where
                                                   e.MemberNames.Contains(
                                                       binding.ParentBinding.Path.Path)
                                               select e;

                    if (filteredErrorResults.ToList().Count > 0)
                    {
                        var errorErrorResult = filteredErrorResults.ToList()[0];
                        SetControlError((Control) childControl, errorErrorResult.ErrorMessage);
                    }
                    else
                    {
                        ClearControlError((Control) childControl);
                    }
                }

                SetErrorsForControls(childControl, errorResults);
            }
        }

        private static BindingExpression GetBinding(DependencyObject childControl)
        {
            // ReSharper disable PossibleNullReferenceException
            BindingExpression binding = null;
            if (childControl.GetType() == typeof (TextBox))
            {
                var txt = childControl as TextBox;
                binding = txt.GetBindingExpression(TextBox.TextProperty);
            }
            else if (childControl.GetType() == typeof (ComboBox))
            {
                var cmb = childControl as ComboBox;
                binding = cmb.GetBindingExpression(Selector.SelectedValueProperty);
            }
            else if (childControl.GetType() == typeof (CheckBox))
            {
                var chk = childControl as CheckBox;
                binding = chk.GetBindingExpression(ToggleButton.IsCheckedProperty);
            }
            else if (childControl.GetType() == typeof (ListBox))
            {
                var lst = childControl as ListBox;
                binding = lst.GetBindingExpression(Selector.SelectedValueProperty);
            }
            // ReSharper restore PossibleNullReferenceException
            return binding;
        }


        public void SetControlError(Control control, string errorMessage)
        {
            if (control == null) return;

            var validationHelper =
                new ControlValidationHelper(errorMessage);


            control.SetBinding(TagProperty, new Binding("ValidationError")
                                                {
                                                    Mode = BindingMode.TwoWay,
                                                    NotifyOnValidationError = true,
                                                    ValidatesOnExceptions = true,
                                                    UpdateSourceTrigger = UpdateSourceTrigger.Explicit,
                                                    Source = validationHelper
                                                });

            // this line throws a ValidationException with your custom error message;
            // the control will catch this exception and switch to its "invalid" state
            // ReSharper disable PossibleNullReferenceException
            control.GetBindingExpression(TagProperty).UpdateSource();
            // ReSharper restore PossibleNullReferenceException
        }

        public void ClearControlError(Control control)
        {
            var b = control.GetBindingExpression(TagProperty);

            if (b != null)
            {
                ((ControlValidationHelper) b.DataItem).ThrowValidationError = false;
                b.UpdateSource();
            }
        }
    }
}