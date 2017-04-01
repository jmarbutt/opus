using System;
using Opus.DataAnnotations;

namespace Opus.Samples.Models
{
    [MetadataType(typeof (OrderMetadata))]
    public class OrderModel
    {
        public DateTime OrderDate { get; set; }

        public int? Qty { get; set; }

        public string Description { get; set; }

        public double? Amount { get; set; }
    }
}