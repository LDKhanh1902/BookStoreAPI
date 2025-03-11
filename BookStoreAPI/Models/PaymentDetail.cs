using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactAppTest.Server.Models
{
    public class PaymentDetail
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book? Book { get; set; }

        [ForeignKey("Payment")]
        public string PaymentId { get; set; } = string.Empty;
        public Payment? Payment { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
