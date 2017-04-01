using System.Windows;
using GalaSoft.MvvmLight.Command;
using Opus.DataAnnotations;

namespace Opus.Samples.Silverlight.CustomView.ViewModels
{

    [FieldGrouping(Name = "Contact", Order = 0)]
    [FieldGrouping(Name = "Phone Numbers", Order = 1)]
    [FieldGrouping(Name = "Address", Order = 2)]
    [ViewSettings(EntityViewType = typeof(Views.CustomView))]

    public class PersonViewModel
    {
        [DisplayControlTextbox(Name = "First Name", Group = "Contact")]       
        public string FirstName { get; set; }

        [DisplayControlTextbox(Name = "Last Name", Group = "Contact")]        
        public string LastName { get; set; }

        [DisplayControlTextbox(Name = "Phone 1", Group = "Phone Numbers")]
        public string Phone1 { get; set; }

        [DisplayControlTextbox(Name = "Phone 2", Group = "Phone Numbers")]
        public string Phone2 { get; set; }


        [DisplayControlTextbox(Name = "Address 1", Group = "Address")]
        public string Address1 { get; set; }

        [DisplayControlTextbox(Name = "Addres 2", Row = 1, Group = "Address")]
        public string Address2 { get; set; }

        [DisplayControlTextbox(Name = "City", Row = 1, Group = "Address")]
        public string City { get; set; }

        [DisplayControlTextbox(Name = "State", Row = 1, Group = "Address")]
        public string State { get; set; }

        [DisplayControlTextbox(Name = "Zip", Row = 1, Group = "Address")]
        public string Zip { get; set; }

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