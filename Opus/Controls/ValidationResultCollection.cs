using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Opus.Controls
{
    public class ValidationResultCollection : List<ValidationResult>
    {
        public ValidationResultCollection()
        {
        }

        public ValidationResultCollection(IEnumerable<ValidationResult> results)
            : base(results)
        {
        }

        public override string ToString()
        {
            if (Count == 0)
                return null;

            var sb = new StringBuilder();
            for (var i = 0; i < Count; i++)
            {
                sb.Append(this[i]);
                if (i < Count - 1)
                    sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}