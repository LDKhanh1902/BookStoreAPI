using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ReactAppTest.Server.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        [JsonIgnore] // Ngăn vòng lặp khi serialize
        public Author? Author { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [JsonIgnore] // Ngăn vòng lặp khi serialize
        public Category? Category { get; set; }

        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }

        [JsonIgnore] // Ngăn vòng lặp khi serialize
        public Publisher? Publisher { get; set; }

        public DateTime PublishedDate { get; set; }
        public decimal Price { get; set; }
        public DateTime EntryDate { get; set; }
        public int PurchasePrice { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore] // Ngăn vòng lặp khi serialize
        public ICollection<PaymentDetail>? PaymentDetails { get; set; }
    }

}
