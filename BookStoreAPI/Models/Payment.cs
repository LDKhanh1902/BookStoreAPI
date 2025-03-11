using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReactAppTest.Server.Models
{
    public class Payment
    {
        [Key]
        public string Id { get; set; } = string.Empty;
        public int Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        // Một thanh toán có thể có nhiều chi tiết thanh toán
        [JsonIgnore]
        public ICollection<PaymentDetail>? PaymentDetails { get; set; }
    }
}