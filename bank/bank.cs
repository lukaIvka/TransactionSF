using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Interfaces;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace bank
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class Bank : StatelessService,IBank
    {
        public Bank(StatelessServiceContext context)
            : base(context)
        { }
        private readonly Dictionary<string, double> _clientBalances = new Dictionary<string, double>
        {
            { "User1", 5000.00 },
            { "User2", 2500.50 },
            { "User3", 300.75 }
        };
        public void Commit()
        {
            Console.WriteLine("Transaction committed in BankService.");
        }

        public void EnlistMoneyTransfer(string userID, double amount)
        {
            if (!_clientBalances.ContainsKey(userID))
            {
                throw new Exception($"Client '{userID}' does not exist.");
            }

            if (_clientBalances[userID] < amount)
            {
                throw new Exception($"Insufficient balance for client '{userID}'.");
            }

            _clientBalances[userID] -= amount;
            Console.WriteLine($"Successfully transferred {amount:C} from client '{userID}'. New balance: {_clientBalances[userID]:C}");
        }

        public void ListClients()
        {
            Console.WriteLine("Available clients:");
            foreach (var client in _clientBalances)
            {
                Console.WriteLine($"Client: {client.Key}, Balance: {client.Value:C}");
            }
        }

        public bool Prepare()
        {
            Console.WriteLine("Preparing transaction in BankService...");
            return true; // Simulacija pripreme transakcije
        }

        public void Rollback()
        {
            Console.WriteLine("Transaction rolled back in BankService.");

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
