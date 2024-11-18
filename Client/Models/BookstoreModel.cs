namespace Client.Models
{
    public class BookstoreModel: TransactionModel
    {
        public List<string> Books { get; set; } // Lista dostupnih knjiga
        public string SelectedBook { get; set; } // Odabrana knjiga
        public uint Quantity { get; set; } // Količina za kupovinu
        public double TotalPrice { get; set; } // Ukupna cena
    }
}
