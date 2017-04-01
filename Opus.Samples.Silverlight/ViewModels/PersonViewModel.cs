using System.Windows;
using GalaSoft.MvvmLight.Command;
using Opus.DataAnnotations;
using Opus.Samples.Models;
using Opus.Samples.Silverlight.Models;

namespace Opus.Samples.Silverlight.ViewModels
{

    [FieldGrouping(Name = "Contact", Order = 0)]
    public class PersonViewModel
    {

        public PersonViewModel()
        {
            //Set some sample properties
            MyViewModelProperty = "Test 1 2 3";
            Person = new PersonModel();
            
        }

        [DisplayControlTextbox(Name = "View Model Property", Group = "Contact")]
        public string MyViewModelProperty { get; set; }
         
         [ImportChild(MetadataType = typeof(PersonMetadata))]
        public PersonModel Person { get; set; }

        [DisplayCommand(Name = "Save", ValidateBeforeExecuting = true)]
        public RelayCommand SaveCommand
        {
            get { return new RelayCommand(Save); }
        }

        private static void Save()
        {
            MessageBox.Show("Hello From Save");
        }

        [DisplayCommand(Name = "Close", ValidateBeforeExecuting = false)]
        public RelayCommand CloseCommand
        {
            get { return new RelayCommand(Close); }
        }

        private static void Close()
        {
            MessageBox.Show("Hello From Close - VALIDATION WAS NOT CALLED");
        }
    }
}