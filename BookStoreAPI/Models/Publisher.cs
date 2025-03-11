using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ReactAppTest.Server.Models
{
    public class Publisher
    {
        [Key]
        public int PublisherId { get; set; }
        public string PublisherName { get; set; } = string.Empty;
        public string PublisherAddress { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;

        // Một nhà xuất bản có nhiều sách
        [JsonIgnore]
        public ICollection<Book>? Books { get; set; }
    }
}