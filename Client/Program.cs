using System;
using Common;
using Common.Interfaces;

namespace Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Klijent aplikacija ===");
            Console.WriteLine("1. Bankovna transakcija");
            Console.WriteLine("2. Rezervacija knjige");
            Console.WriteLine("0. Izlaz");

            bool exit = false;

            while (!exit)
            {
                Console.Write("Unesite opciju: ");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                       // await HandleBankTransaction();
                        break;

                    case "2":
                        //await HandleBookReservation();
                        break;

                    case "0":
                        Console.WriteLine("Izlaz iz aplikacije...");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Nevažeća opcija. Pokušajte ponovo.");
                        break;
                }
            }
        }
        /*
        /// <summary>
        /// Obrada bankovne transakcije
        /// </summary>
        private static async Task HandleBankTransaction()
        {
            Console.WriteLine("=== Obrada bankovne transakcije ===");

            // Simulacija slanja zahteva servisu za validaciju
            IBank bankService = new BankService(); // Pretpostavljamo da postoji implementacija IBank u Common
            Console.Write("Unesite iznos transakcije: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                bool isValid = await bankService.ValidateTransaction(amount);
                if (isValid)
                {
                    Console.WriteLine("Transakcija je uspešno validirana!");
                }
                else
                {
                    Console.WriteLine("Transakcija nije validna.");
                }
            }
            else
            {
                Console.WriteLine("Nevažeći iznos. Pokušajte ponovo.");
            }
        }

        /// <summary>
        /// Obrada rezervacije knjige
        /// </summary>
        private static async Task HandleBookReservation()
        {
            Console.WriteLine("=== Rezervacija knjige ===");

            // Simulacija slanja zahteva servisu za rezervaciju knjiga
            IBookstore bookstoreService = new IBookstore();
            Console.Write("Unesite naslov knjige: ");
            string? title = Console.ReadLine();

            if (!string.IsNullOrEmpty(title))
            {
                bool isReserved = await bookstoreService.ReserveBook(title);
                if (isReserved)
                {
                    Console.WriteLine($"Knjiga '{title}' je uspešno rezervisana.");
                }
                else
                {
                    Console.WriteLine($"Rezervacija knjige '{title}' nije uspela.");
                }
            }
            else
            {
                Console.WriteLine("Naslov knjige ne može biti prazan.");
            }
        }
        */
    }
}