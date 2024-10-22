namespace Cwiczenia2Domek
{
    class Program
    {
        static void Main()
        {
            // Osoba osoba1 = new Osoba { Imie= "Jan", Nazwisko = "Kowalski", Pesel = "45392986709"};
            // Console.WriteLine(osoba1);
            // Wyświetlamy dane osoby w jednym poleceniu Console.WriteLine
            //Console.WriteLine($"Imię: {osoba1.Imie}, Nazwisko: {osoba1.Nazwisko}, PESEL: {osoba1.Pesel}");

            Osoba os1 = new("Joanna", "Brodzik", EnumPlec.K);
            Osoba os2 = new("Kazimierz", "Jabłoński", EnumPlec.M) { Pesel = "01123088332" };
            Osoba os3 = new("Beata", "Nowak", "Szeroka 21", 30345, "Kraków", "1992-10-22", "99041398765", EnumPlec.K);
            Osoba os4 = new("Damian", "Wolski", EnumPlec.M) { Pesel = "90120399002" };
            Osoba os5 = new("Jan", "Janowski", "Wąska 102", 22011, "Warszawa", "1993-03-15", "92031507772", EnumPlec.M); 
            Console.WriteLine(os1);
            Console.WriteLine(os2); 
            Console.WriteLine(os3); 
            Console.WriteLine(os4);
            Console.WriteLine(os5);
       

        }
    }
}             
