using System.Data;

namespace Csharp.oop.Bankapp;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome");
        Console.WriteLine("Welcome to Our Simple Bank Application");

        // Show Main Menu
        ShowMainMenu();
        
    }
     static List<Customer> customers = new List<Customer>(); // We will keep all customers here
     static int nextCustomerId= 1; // We will give a number to each new customer
     static void RegisterNewCustomer()
    {
        Console.WriteLine("\n════════ YENİ MÜŞTERİ KAYDI ════════");

        // We receive information from the user
        Console.Write("Ad Soyad: ");
        string name = Console.ReadLine();

        Console.Write("Telefon:");
        string phone = Console.ReadLine();

        Console.Write("Başlangıç Bakiyesi:");
        decimal balance ;

        // We check if the correct number has been entered with TryParse
        while(!decimal.TryParse(Console.ReadLine(), out balance) || balance < 0)
        {
            Console.Write("Geçersiz bakiye! Lütfen pozitif bir sayı girin:");
        }

        // We create a new customer object
        Customer newCustomer = new Customer(nextCustomerId,name,phone,balance);

        // We add the new customer to the list
        customers.Add(newCustomer);

        nextCustomerId++; // We increase the customer number for the next customer
    Console.WriteLine($"\n✅ Müşteri başarıyla kaydedildi!");
        Console.WriteLine("Customer ID: {newCustomer.Id}");

        // Wait 2 seconds and return to the menu
        System.Threading.Thread.Sleep(2000);
    }
    static void ListCustomers()
    {
            Console.WriteLine("\n════════ MÜŞTERİ LİSTESİ ════════");

        // We are checking if there are any customers
        if (customers.Count == 0)
        {
            Console.WriteLine("Henüz Kayıtlı Müşteri Yok.");
            return; // finsh the method
        }
        // We list all customers
        Console.WriteLine($"Toplam {customers.Count} müşteri bulunuyor:\n");

        foreach(Customer customer in customers)
        {
            customer.PrintInfo(); //We call the PrintInfo method for each customer
        }
            Console.WriteLine("\nDevam etmek için bir tuşa basın...");
            Console.ReadKey();
    }
   static void ShowMainMenu()
    {
        Console.WriteLine("\n════════════ ANA MENÜ ════════════");
        Console.WriteLine("1. Yeni Müşteri Kaydı");
        Console.WriteLine("2. Müşteri Listesi");
        Console.WriteLine("3. Hesap İşlemleri");
        Console.WriteLine("4. Çıkış");
        Console.WriteLine("═══════════════════════════════════");

        Console.Write("\nSeçiminiz (1-4): ");
        string choice = Console.ReadLine();

        // Act according to the user's choice

        switch (choice)
        {
            case "1";
                RegisterNewCustomer();
                break;
                case "2";
                ListCustoemers();
                break;
                case "3";
                ShowAccountOperations();
                break;
                case "4";
                Console.WriteLine("Logging Out...");
                return;
                default;
                Console.WriteLine("Invalid choice! Try again");
                break;
        }
        //back to menu
        ShowMainMenu();
    }
}

