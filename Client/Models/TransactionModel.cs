namespace Client.Models
{
    public class TransactionModel
    {
        public string TransactionId { get; set; }
        public enum State { Pending, Completed, Failed }
        public string Message { get; set; } // Poruka za prikaz korisniku
    }
}
