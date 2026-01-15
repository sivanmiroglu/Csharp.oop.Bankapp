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

