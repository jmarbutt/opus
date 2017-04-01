using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Shell;
using Opus.DataAnnotations;

namespace Opus.Controls
{
    public partial class ViewModelView
    {
        public ObservableCollection<ApplicationBarMenuItem> MenuItems { get; set; }
        private void LoadMenuItems(IEnumerable<DisplayCommand> commands)
        {            
            foreach (var displayCommand in commands)
            {
                var btn = new ApplicationBarMenuItem { Text = displayCommand.Name };
                MenuItems.Add(btn);
            }
        }
    }
}
