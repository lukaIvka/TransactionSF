using System.Fabric.Management.ServiceModel;

namespace Client.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public Book() { }
    }
}
