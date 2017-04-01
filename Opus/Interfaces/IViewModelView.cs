using System.Collections.Generic;
using Opus.DataAnnotations;

namespace Opus.Interfaces
{
    public interface IViewModelView
    {
        void LoadFieldGroup(FieldGrouping fieldGroup);
        void LoadCommands(IEnumerable<DisplayCommand> commands);
    }
}
