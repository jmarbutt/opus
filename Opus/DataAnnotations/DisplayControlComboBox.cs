using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Opus.DataAnnotations
{
    public class DisplayControlComboBox : DisplayControlBase
    {
        public DisplayControlComboBox()
        {
            DisplayType = DisplayTypes.ComboBox;
        }

        public string ItemSourcePath { get; set; }
        public string ValueMemberPath { get; set; }
        public string ViewModelResourceName { get; set; }

        public string LookupPath { get; set; }

     

// This is used to enable lazy loading in IdeaBlade

        public override FrameworkElement GetInputControl(Binding binding)
        {
            var cmb = new ComboBox
                          {
                              Width = Width
                          };
            //cmb.SetBinding(AutoCompleteBox.SelectedItemProperty, binding);

            
            if (ItemSourcePath != null)
            {
                if (!string.IsNullOrEmpty(ViewModelResourceName))
                {
                    //Bind to the common resource 
                    var itemSourcePath = new Binding(ItemSourcePath)
                                             {
                                                 Mode = BindingMode.TwoWay,
                                                 Source =
                                                     Application.Current.Resources[
                                                         ViewModelResourceName]
                                             };
                    cmb.SetBinding(ItemsControl.ItemsSourceProperty, itemSourcePath);
                }
                else
                {
                    var itemSourcePath = new Binding(ItemSourcePath) { Mode = BindingMode.TwoWay };
                    cmb.SetBinding(ItemsControl.ItemsSourceProperty, itemSourcePath);
                }
            }


            return cmb;
        }
    }
}