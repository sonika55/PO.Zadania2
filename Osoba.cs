using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cwiczenia2Domek

{
    enum EnumPlec { K, M }
    internal class Osoba
    {
        private string imie;
        private string nazwisko;
        private string adres;
        private DateTime dataUrodzenia; //uwaga
        EnumPlec plec;
        private string pesel; //to som obiekty

        public string Imie { get => imie; set => imie = value; }
        public string Nazwisko { get => nazwisko; set => nazwisko = value; }
        public string Adres { get => adres; set => adres = value; }
        public DateTime DataUrodzenia { get => dataUrodzenia; set => dataUrodzenia = value; }
        public string Pesel
        {
            get => pesel;

            init
            {

                if //(value.Length != 11)
                    (!Regex.IsMatch(value, @"\d{11}")) //wyrazenie regularne, klasa Regex to dodatkowo sprawdzenie czy składa się z cyfr
                { throw new ArgumentException("Pesel musi miec 11 znaków"); }

                pesel = value;
            }
        }
        // init - służy do definiowania właściwości,
        // które można ustawić tylko w trakcie inicjalizacji
        // obiektu, a nie zmieniać później
        //pesel zwykle nie zmienia się w trakcie życia. Z tego względu naturalne jest,
        //aby tę właściwość można było ustawić tylko raz — przy tworzeniu obiektu
        // a później nie mogła być modyfikowana. Stąd użycie init zamiast set

        public Osoba() //konstruktory dodajemy w danej klasie !!!
                       //bezparametrowy domyślny konstruktor
        {
            Imie = string.Empty;
            Nazwisko = string.Empty; //tu te z dużej litery, z właściwości
            Pesel = new string('0', 11); //tutaj tworze uniwersalny Pesel ktory sie wyswietla

        }

        public Osoba(string imie, string nazwisko, EnumPlec plec)
            : this() //wywołanie konstruktora domyślnego
        {
            Imie = imie;
            Nazwisko = nazwisko;
            this.plec = plec; // Tutaj używamy "this", aby wskazać na pole klasy plec,
                              // zeby kompliator mogl odroznic pole klasy od parametru metody (tym w nawiasie)
        }

        public Osoba(string imie, string nazwisko, string dataUrodzenia, string pesel, EnumPlec plec)
            : this(imie, nazwisko, plec)
        {
            // Imie = imie; tu juz nie przywolujemy tego co w poprzednim konsktruktorze, mamy odwolanie w this()
            Pesel = pesel;
            //Parsowanie to proces konwersji jednego formatu danych na inny
            if (DateTime.TryParseExact(dataUrodzenia, new[] { "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "ddMMM-yy" },
                null, System.Globalization.DateTimeStyles.None, out DateTime d))
            {
                DataUrodzenia = d;
            }
        }

        public Osoba(string imie, string nazwisko, string ulica, int kod, string miasto, string dataUrodzenia, string
pesel, EnumPlec plec)
            : this(imie, nazwisko, dataUrodzenia, pesel, plec)
        {
            Adres = $" {ulica}, {kod}, {miasto}";
            string sformatowanyKodPocztowy = $"{kod / 1000:00}-{kod % 1000:000}"; // Formatuje kod z myślnikiem
            Adres = $"{ulica}, {sformatowanyKodPocztowy} {miasto}";

        }

        public int Wiek()
        {

            if (DataUrodzenia == default(DateTime))
            {
                return -1; // Zwracamy -1, jeśli data urodzenia nie jest podana
            }

            int w1 = DateTime.Now.Year - DataUrodzenia.Year;
            int w2 = (int)(DateTime.Now - DataUrodzenia).TotalDays / 365;
            return w2;

        }
        //uwaga ponizej inna funkcja Wiek z tej z lekcji na inną BO dla próby
        // musialam zmienic typ DatyUrodzenia na nullable aby użyć ?? 
        //w ToString, a wtedy nie mam .TotalDays, zatem zmieniam funkcje Wiek

/*  public int Wiek()
 {
     if (!DataUrodzenia.HasValue)
     {
         return 0; // lub inna wartość, jeśli data urodzenia nie jest podana
     }

     DateTime data = DataUrodzenia.Value;
     int wiek = DateTime.Now.Year - data.Year;

     // Sprawdzamy, czy już było urodziny w tym roku, jeśli nie, odejmujemy 1 rok.
     if (DateTime.Now < data.AddYears(wiek))
     {
         wiek--;
     }

     return wiek;
 }
*/


//teraz tak: cała ta metoda ToString jest po to, żeby w konsoli potem łatwiej wyświetlało się dane osoby
// czemu słowo "przesłoń"?
//przesłonić metodę ToString() w klasie Osoba, aby dostarczała przyjazny dla użytkownika
//format wyświetlania wszystkich właściwości. Dzięki temu wystarczy jedno wywołanie Console.WriteLine(osoba)
//suuuupcio juz wiemy o co chodzi


//to $ to interpolacja ciągów - pozwala na bezpośrednie wstawienie wartości zmiennych wewnątrz nawiasów {}



//podsumowanie:
//co do DatyUrodzenia jesli chce, żeby została wyświetlona tylko wtedy, gdy jest podana to:
//1.typ DateTime mogę zmienić na typ nullable DateTime? i wtedy użyć .HasValue
/* //Sprawdzamy, czy DataUrodzenia ma wartość, jeśli nie, nie wyświetlamy jej

    // string dataUrodzeniaStr = DataUrodzenia.HasValue ? $"ur. {DataUrodzenia:yyyy-MM-dd}" : string.Empty;
    // Warunkowe wyświetlanie "ur." oraz wieku, jeśli data urodzenia istnieje
     string dataUrodzeniaStr = DataUrodzenia.HasValue
         ? $"[{Wiek()} lat] ur. {DataUrodzenia:yyyy-MM-dd}"
         : string.Empty; 

     // Jeśli dataUrodzeniaStr jest pustym ciągiem, ten fragment nie zostanie dodany
     return $"{imie} {nazwisko} ({plec}), " +
            $"{(string.IsNullOrEmpty(dataUrodzeniaStr) ? "" : dataUrodzeniaStr + " ")}" +
            $"({Pesel})" +
            $"{Adres}"; */

//2. mogę uzależnić wyświetlanie daty od tego czy adres został podany

/*     return $"{Imie} {Nazwisko} ({plec}), " +
(string.IsNullOrEmpty(Adres) ? "" : $"ur. {DataUrodzenia:yyyy-MM-dd} ") +
$"({Pesel})";*/

//3. mogę użyć .MinValue co ustawia 
/*         string dataUrodzeniaStr = DataUrodzenia == DateTime.MinValue
             ? "---"
            : DataUrodzenia.ToString("yyyy-MM-dd"); */ //date obsłuży ale trzeba dodać coś jeszcze gdzieś żeby działało poprawnie
 //4. coś z Var

public override string ToString() //to nazwa metody
{

string imie = string.IsNullOrEmpty(Imie) ? "---" : Imie;
string nazwisko = string.IsNullOrEmpty(Nazwisko) ? "---" : Nazwisko;

var ageDisplay = Wiek() >= 0 ? $"{Wiek()} lat" : "-"; // Wyświetl wiek lub "-"
var formattedDate = DataUrodzenia != DateTime.MinValue ? $"ur. {DataUrodzenia:yyyy-MM-dd} " : "";
         /*   // Formatuj datę urodzenia
            string formattedDate = DataUrodzenia.HasValue
                ? $"ur. {DataUrodzenia.Value:yyyy-MM-dd}"
                : ""; // Pusty string, jeśli data nie jest dostępna */

            // Warunkowe dodawanie przedimka "ul." do adresu
            string adresDisplay = !string.IsNullOrEmpty(Adres)
                ? $"ul. {Adres}"
                : ""; 
      // Pusty string, jeśli adres nie jest dostępny// Wyświetl datę urodzenia tylko, gdy nie jest domyślna

     //var formattedAddress = !string.IsNullOrEmpty(Ulica) ? $"ul. {Ulica}, " : ""; // Wyświetl adres, jeśli jest podany
     //var formattedLocation = Kod != 0 && !string.IsNullOrEmpty(Miasto) ? $"{Kod} {Miasto}" : ""; // Wyświetl miasto, jeśli jest podane

            return $"{Imie} {Nazwisko} [{ageDisplay}] ({plec}), " +
                      $"{formattedDate}" + // Warunkowo wyświetlamy datę urodzenia
                      $"({Pesel})" +
                      $" {adresDisplay}";
        }

        // string pesel = string.IsNullOrEmpty(Pesel) ? "-------" : Pesel; -> pesel juz obsluguje we wlasciwosciach chyba
        //string adresStr = string.IsNullOrEmpty(Adres) ? "" : $", {Adres}"; -> w polu adres moze byc puste
        // string plecStr = plec.ToString(); // Konwersja do stringa - tu niewazne, inaczej sie to obsluguje





        // string dataUrodzenia = !DataUrodzenia.HasValue ? "---" : DataUrodzenia.Value.ToString("yyyy-MM-dd"); tez nie dziala
        //   string dataUrodzenia = string.IsNullOrEmpty(DataUrodzenia) ? "---" : DataUrodzenia
        //   zle bo dataurodzenia to DataType a nie string


        //    return $"{Imie} {Nazwisko} ({plec}), " + $" {DataUrodzenia:yyyy-MM-dd} ({Pesel})"
        //+ $"{Adres}";
        //return $" {Imie} {Nazwisko} ({plec}),  ur. {DataUrodzenia}, PESEL: {Pesel}"; 


        //nie trzeba z plusem, można wszystko w jednym ciągu - kwestia zapisu i czytelności
        // można wszystko w jednym ciągu "interpolowanym"
        //co oznacza override ?
        //override oznacza przesłanianie metody w klasie bazowej
        //w C# każda metoda DZIEDZICZY po klasie bazowej "object", domyślnie ta metoda {} zwraca ciąg znaków reprezentujący obiekt,
        // - w większości przypadków jest to nazwa klasy, ale można ją przesłonić aby dostosować to, co ma być wyświetlane
        // string w nazwie metody oznacza typ zwracany przez metodę - w tym przypadku ciąg znaków
        //Gdy wywołujesz np. Console.WriteLine(osoba), to domyślnie zostanie wywołana metoda ToString() tego obiektu.
        //Bez przesłonięcia zwróci nazwę klasy, ale po przesłonięciu możesz wyświetlić np. imię i nazwisko, jak to zrobiliśmy powyżej
        //-----------------------------------------------------------------------------------------------------------

        // !!! ważne !!! aby uwzględnić brak danych, i wyswietlać np. ---- można warunkowo sprawdzić wartości
        //Jeśli interesuje nas tylko sprawdzenie, czy wartości są puste lub null,
        //i chcemy wyświetlać domyślną wartość (np. -------), można użyć operatora
        //null-coalescing (??) bez dodatkowych sprawdzeń.
        //jednak dla pustych łańcuchów tekstowych warto wykorzystać również string.IsNullOrEmpty()
    }
    }
