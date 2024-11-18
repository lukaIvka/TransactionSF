using System.Fabric;
using Common.Interfaces;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace booksotre
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class Booksotre : StatelessService, IBookstore
    {
        public Booksotre(StatelessServiceContext context)
            : base(context)
        { }

        private readonly Dictionary<string, (double Price, uint Stock)> _inventory = new Dictionary<string, (double, uint)>
    {
        { "20000 milja pod morem", (10.99, 50) },
        { "Na Drini Cuprija", (12.99, 30) },
        { "Kad su cvetale tikve", (8.99, 20) }
    };

        public void ListAvailableItems()
        {
            Console.WriteLine("Available books in the bookstore:");
            foreach (var item in _inventory)
            {
                Console.WriteLine($"Book: {item.Key}, Price: {item.Value.Price:C}, Stock: {item.Value.Stock}");
            }
        }

        public void EnlistPurchase(string bookID, uint count)
        {
            if (!_inventory.ContainsKey(bookID))
            {
                throw new Exception($"Book '{bookID}' does not exist.");
            }

            if (_inventory[bookID].Stock < count)
            {
                throw new Exception($"Insufficient stock for book '{bookID}'. Available: {_inventory[bookID].Stock}");
            }

            var updatedStock = _inventory[bookID].Stock - count;
            _inventory[bookID] = (_inventory[bookID].Price, updatedStock);
            Console.WriteLine($"Successfully purchased {count} copies of '{bookID}'. Remaining stock: {updatedStock}");
        }

        public double GetItemPrice(string bookID)
        {
            if (!_inventory.ContainsKey(bookID))
            {
                throw new Exception($"Book '{bookID}' does not exist.");
            }

            return _inventory[bookID].Price;
        }

        public bool Prepare()
        {
            Console.WriteLine("Preparing transaction in BookstoreService...");
            return true; // Simulacija pripreme transakcije
        }

        public void Commit()
        {
            Console.WriteLine("Transaction committed in BookstoreService.");
        }

        public void Rollback()
        {
            Console.WriteLine("Transaction rolled back in BookstoreService.");
        }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[0];
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following sample code with your own logic 
            //       or remove this RunAsync override if it's not needed in your service.

            long iterations = 0;

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                ServiceEventSource.Current.ServiceMessage(this.Context, "Working-{0}", ++iterations);

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
    }
}
