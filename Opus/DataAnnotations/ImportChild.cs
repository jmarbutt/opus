using System;

namespace Opus.DataAnnotations
{
    public class ImportChild : Attribute
    {
        public string PropertyPath { get; set; }

        public Type MetadataType { get; set; }
    }
}