using System;
using System.Numerics;
using System.Xml.Linq;

namespace Csharp.oop.Bankapp
{
	public class Customer
	{
        // Customer's information
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public decimal Balance { get; set; }


        // Method that works when creating a customer
        public Customer(int id, string name, string phone, decimal balance)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Balance = balance;
        }

        // Print customer information on the screen
        public void PrintInfo()
        {
            Console.WriteLine($"Müşteri No: {Id}");
            Console.WriteLine($"Ad Soyad: {Name}");
            Console.WriteLine($"Telefon: {Phone}");
            Console.WriteLine($"Bakiye: {Balance:C}");
            Console.WriteLine("──────────────────────");
        }
    }

}

