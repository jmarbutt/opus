using System.Windows;
using System.Windows.Input;

namespace Opus.Controls
{
    public partial class DataGrid
    {
        public DataGrid()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty AddCommandProperty = DependencyProperty.Register("AddCommand", typeof(ICommand), typeof(DataGrid),
                                        new PropertyMetadata(null));

        public static readonly DependencyProperty EditCommandProperty = DependencyProperty.Register("EditCommand", typeof(ICommand), typeof(DataGrid),
                                        new PropertyMetadata(null));

        public static readonly DependencyProperty DeleteCommandProperty = DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(DataGrid),
                                        new PropertyMetadata(null));


        public ICommand AddCommand
        {
            get { return GetValue(AddCommandProperty) as ICommand; }
            set
            {
                SetValue(AddCommandProperty, value);
                btnAdd.Command = value;
            }
        }

        public ICommand EditCommand
        {
            get { return GetValue(EditCommandProperty) as ICommand; }
            set
            {
                SetValue(EditCommandProperty, value);
                btnEdit.Command = value;
            }
        }

        public ICommand DeleteCommand
        {
            get { return GetValue(DeleteCommandProperty) as ICommand; }
            set
            {
                SetValue(DeleteCommandProperty, value);
                btnDelete.Command = value;
            }
        }
    }
}
