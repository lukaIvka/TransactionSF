namespace Client.Models
{
    public class BankModel : TransactionModel
    {
        public List<string> Clients { get; set; } // Lista klijenata
        public string SelectedClient { get; set; } // Odabrani klijent
        public double TransferAmount { get; set; } // Iznos za transfer
    }
}
