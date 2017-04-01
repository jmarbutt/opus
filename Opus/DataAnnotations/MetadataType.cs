using System;

namespace Opus.DataAnnotations
{
    public class MetadataTypeAttribute : Attribute
    {
        public MetadataTypeAttribute(Type t)
        {
            MetaDataType = t;
        }

        public Type MetaDataType
        {
            get;
            set;
        }
    }
}