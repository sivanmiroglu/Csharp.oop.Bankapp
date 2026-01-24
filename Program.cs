using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

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
    static void ShowAccountOperations()
    {
        Console.WriteLine("\n════════ HESAP İŞLEMLERİ ════════");
        if (customers.Count == 0)
        {
            Console.WriteLine("Önce müşteri kaydı yapmalısınız.");
            return;
        }
              Console.WriteLine("1. Para Yatır");
    Console.WriteLine("2. Para Çek");
    Console.WriteLine("3. Bakiye Sorgula");
    Console.WriteLine("4. Para Transferi");  
    Console.WriteLine("5. Ana Menü");       
    Console.WriteLine("══════════════════════════════════");
    
    Console.Write("\nSeçiminiz (1-5): ");
    string choice = Console.ReadLine();

      switch (choice)
    {
        case "1": DeopositMoney(); break;
        case "2": WithdrawMoney(); break;
        case "3": CheckBalance(); break;
        case "4": TransferforMoney(); break;  // ← YENİ EKLENDİ
        case "5": return;  // Ana menüye dön
        default: Console.WriteLine("Geçersiz seçim!"); break;
    }
    
    ShowAccountOperations();
        // Show this menu again when the process is finished
        ShowAccountOperations();
    }

    static void DeopositMoney()
    {
            Console.WriteLine("\n════════ PARA YATIRMA ════════");

            // We ask for the customer number
            Console.Write("Müşteri No:");
            int customerId;

            if(!int.TryParse(Console.ReadLine(),out customerId))
        {
            Console.WriteLine("Geçersiz müşteri numarası");
        }
        // We find the customer
        Customer customer = FindCustomerById(customerId);
        if(customer == null)
        {
            Console.WriteLine("Muşteri Bulunamadı");
            return;
        }
        // we ask for the amount 
        Console.Write("Yatırılacak Tutar:");
        decimal amount;

    if (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
        {
            Console.WriteLine("Geçersiz miktar");
            return;
        }

        // Update the balance
        customer.Balance += amount;
        Console.WriteLine($"\n✅ {amount} TL başarıyla yatırıldı.");
        Console.WriteLine($"Güncel Bakiye: {customer.Balance:CheckBalance}");

        System.Threading.Thread.Sleep(2000);
    }
    static Customer FindCustomerById(int id)
    {
       // We walk among all customers
       foreach(Customer customer in customers)
        {
            if (customer.Id == id)
            {
                return customer; // Does the ID match?
            }
        }
        return null; // is not found customer
        
    }

    static void WithdrawMoney()
    {
            Console.WriteLine("\n════════ PARA ÇEKME ════════");
            
            Console.Write("Müşteri No:");
            int customerId;

            if(!int.TryParse(Console.ReadLine(), out customerId))
        {
            Console.WriteLine("Geçersiz Müşteri Numarası");
            return;
        }

        Customer customer = FindCustomerById(customerId);

        if (customer == null)
        {
            Console.WriteLine("Müşteri Bulunamadı");
            return;
        }
        // Let's show the balance first
        Console.WriteLine($"Mevcut Bakiye: {customer.Balance:CheckBalance}");

        Console.Write("Çekileecek Tutar: ");
        decimal amount;

        if(!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
        {
            Console.WriteLine("Gçersiz Miktar");
            return;
        }
        // Let's check if the balance is sufficient
        if (amount > customer.Balance)
        {
             Console.WriteLine("❌ Yetersiz bakiye!");
             Console.WriteLine($"Çekmek istediğiniz:{amount:CheckBalance}");
             Console.WriteLine($"Mevcut Bakiye:{customer.Balance:CheckBalance}");
        }
        else
        {
            // Withdrawal
            customer.Balance = customer.Balance - amount;
             Console.WriteLine($"\n✅ {amount:C} çekildi.");
             Console.WriteLine($"Güncel Bakiye:{customer.Balance:CheckBalance}");
        }
        System.Threading.Thread.Sleep(2000);
    }

    static void CheckBalance()
    {
            Console.WriteLine("\n════════ BAKİYE SORGULAMA ════════");
            Console.Write("Müşteri No:");
            int customerId;

            if(!int.TryParse(Console.ReadLine(),out customerId))
        {
            Console.WriteLine("Gçersiz Müşteri Numarası");
            return;
        }

        Customer customer = FindCustomerById(customerId);
        if(customer == null)
        {
            Console.WriteLine("Müşteri Bulunamadı");
            return;
        }
         // Show customer information
             Console.WriteLine("\n════════ HESAP BİLGİLERİ ════════");
           
       customer.PrintInfo();    // We use the PrintInfo method in Customer class
           Console.WriteLine("══════════════════════════════════");
         Console.WriteLine("\nDevam etmek için bir tuşa basın...");
    Console.ReadKey();
    }
    static void TransferforMoney()
    {
            Console.WriteLine("\n════════ PARA TRANSFERİ ════════");

            // 1. Get the sender customer ID
            Console.Write("Gönderen Müşteri No:");
            int senderId;

            if(!int.TryParse(Console.ReadLine(), out senderId))
        {
            Console.WriteLine("Geçersiz Müşteri Numarası!");
            return;
        }

        // Find the sender customer
        Customer sender = FindCustomerById(senderId);
        if(sender == null)
        {
            Console.WriteLine("Gönderen müşteri bulunamadı");
            return;
        }

        // 3. Get the recipient customer ID
        Console.Write("Alıcı Müşteri No:");
        int receiverId;

        if(!int.TryParse(Console.ReadLine(),out receiverId)) ;
        {
            Console.WriteLine("Geçersiz Müşteri Numarası");
            return;
        }
        // Find the recipient
        Customer receiver = FindCustomerById(receiverId);
        if (receiver == null)
        {
            Console.WriteLine("Alıcı Müşteri Numarası Bulunamadı");
            return; 
        }
        // 5. Check if there is a transfer to the same person
        if (senderId == receiverId)
        {
                   Console.WriteLine("❌ Kendi hesabınıza transfer yapamazsınız!");
             return;
        }
        // 6. Get the transfer amount
        Console.Write("Transfer Miktarı:");
        decimal amount;

        if(!decimal.TryParse(Console.ReadLine(),out amount) || amount <= 0)
        {
            Console.WriteLine("Geçersiz Miktar");
            return;
        }
       // 7. Check if the sender's money is enough
       if(amount > sender.Balance)
        {
            Console.WriteLine("❌ Yetersiz Bakiye!");
            Console.WriteLine($"Göndermek istediğiniz:{amount:C}");
            Console.WriteLine($"Mevcut Bakiye:{sender.Balance:C}");
            return;
        }

        // 8. Perform the transfer
        sender.Balance -= amount;
        receiver.Balance += amount;

        Console.WriteLine($"\n✅ {amount:C} başarıyla transfer edildi.");
        Console.WriteLine($"Gönderen Güncel Bakiye:{sender.Balance:C}");
        Console.WriteLine($"Alıcı Güncel Bakiye:{receiver.Balance:C}");

        System.Threading.Thread.Sleep(2000);
    
    }
    static void DeleteCustomer()
    {
        Console.WriteLine("\n════════ MÜŞTERİ SİLME ════════");

        // Show customer list first
        ListCustomers();

        if(customers.Count == 0)
        {
         return;   
        }

        // Get the customer ID to be deleted
        Console.Write("Silinecek Müşteri No:");
        int customerId;

        if(!int.TryParse(Console.ReadLine(),out customerId))
        {
            Console.WriteLine("Geçersiz Müşteri Numarası");
            return;
        }
        // Found the customer
        Customer customerToDelete = FindCustomerById(customerId);
        if(customerToDelete == null)
        {
            Console.WriteLine("Müşteri Bulunamadı");
            return;
        }
    Console.WriteLine($"\nSilmek istediğiniz müşteri:");
        customerToDelete.PrintInfo();
            Console.Write("\nBu müşteriyi silmek istediğinize emin misiniz? (E/H): ");
        string confirmation = Console.ReadLine().ToUpper();

        if(confirmation == "E" || confirmation == "EVET")
        {
            // Remove customer from list
            customers.Remove(customerToDelete);
                    Console.WriteLine($"✅ Müşteri ({customerToDelete.Name}) başarıyla silindi.");
        }
        else
        {
                    Console.WriteLine("❌ Silme işlemi iptal edildi.");
        }
        System.Threading.Thread.Sleep(2000);
    }

    static void ShowBankReport()
    {
            Console.WriteLine("\n══════════ BANKA RAPORU ══════════");
        //Total number of customers
        Console.WriteLine($"Toplam Müşteri Sayısı : {customers.Count}");

        if(customers.Count == 0)
        {
            Console.WriteLine("Henüz müşteri kaydı yok.");
            return;
        }
        // Total balance 
        decimal totalBalance = 0;
        foreach(Customer customer in customers)
        {
            totalBalance += customer.Balance;
        }
        Console.WriteLine($"Toplam Bakiye: {totalBalance:C}");

        // Average Balance
        decimal averageBalance = totalBalance / customers.Count;
        Console.WriteLine($"Ortalama Bakiye: {averageBalance:C}");

       // Highest balance
       Customer richestCustomer = null;
       decimal maxBalance = 0;

       foreach (Customer customer in customers)
        {
            if(customer.Balance > maxBalance)
            {
                maxBalance = customer.Balance;
                richestCustomer = customer;
            }
        }
        if (richestCustomer != null)
    {
        Console.WriteLine($"\n🏆 En Zengin Müşteri:");
        Console.WriteLine($"   Ad: {richestCustomer.Name}");
        Console.WriteLine($"   Bakiye: {richestCustomer.Balance:C}");
    }
    // Lowest Balance
        Customer poorestCustomer = null;
    decimal minBalance = decimal.MaxValue;
    
    foreach (Customer customer in customers)
    {
        if (customer.Balance < minBalance)
        {
            minBalance = customer.Balance;
            poorestCustomer = customer;
        }
    }
    
    if (poorestCustomer != null && poorestCustomer != richestCustomer)
    {
        Console.WriteLine($"\n📉 En Düşük Bakiye:");
        Console.WriteLine($"   Ad: {poorestCustomer.Name}");
        Console.WriteLine($"   Bakiye: {poorestCustomer.Balance:C}");
    }
    
    Console.WriteLine("\n═══════════════════════════════════");
    Console.WriteLine("\nDevam etmek için bir tuşa basın...");
    Console.ReadKey();
}
    

   static void ShowMainMenu()
    {
          Console.WriteLine("\n════════════ ANA MENÜ ════════════");
    Console.WriteLine("1. Yeni Müşteri Kaydı");
    Console.WriteLine("2. Müşteri Listesi");
    Console.WriteLine("3. Hesap İşlemleri");
    Console.WriteLine("4. Müşteri Sil");     
    Console.WriteLine("5. Banka Raporu");     
    Console.WriteLine("6. Çıkış");            
    Console.WriteLine("═══════════════════════════════════");
    
    Console.Write("\nSeçiminiz (1-6): ");
    string choice = Console.ReadLine();
        // Act according to the user's choice

        switch (choice)
        {
            case "1":
                RegisterNewCustomer();
                break;
                case "2":
                ListCustomers();
                break;
                case "3":
                ShowAccountOperations();
                break;
                case "4":
                DeleteCustomer();
                break;
                case "5":
               ShowBankReport();
               break;
                  case "6": 
            Console.WriteLine("Çıkış yapılıyor...");
            return;
        default:
            Console.WriteLine("Geçersiz seçim! Tekrar deneyin.");
            break;
        }
        //back to menu
        ShowMainMenu();
    }
}

