using System;
using Opus.DataAnnotations;

namespace Opus.Samples.Models
{
    public static class OrderMetadata
    {
        [DisplayControlDatePicker(Name = "Order Date", StringFormat = "{0:d}")]
        public static DateTime OrderDate;

        [DisplayControlTextbox(Name = "Qty")]
        public static int? Qty;

        [DisplayControlTextbox(Name = "Description")]
        public static string Description;

        [DisplayControlTextbox(Name = "Amount", StringFormat = "{0:c}")]
        public static double? Amount;
    }
}