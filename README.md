# Zadaća 2 - Ključna riječ static, operatori i korisne klase

## Zadatak

U sustav za rad s informacijama o vremenskim prilikama trebate dodati klasu koja predstavlja vremensku prognozu za određeni dan. Vremenska prognoza sadržava informacije o datumu na koji se odnosi (struktura DateTime) te referencu na objekt tipa Weather iz Zadaće 1. Također je potrebno dodati klasu koja predstavlja tjednu prognozu sa stanjem za čuvanje sedam dnevnih vremenskih prognoza u obliku polja. Implementirajte sve potrebne klase, njihove metode, kao i pomoćne statičke metode unutar klase "ForecastUtilities" kako bi se testni program u nastavku mogao ispravno izvesti (u klasu ForecastUtilities premjestite i metodu za pronalazak vremena s najvećim osjetom hladnoće vjetra iz zadaće 1). 

## Dodatne upute i materijali

* Kod usporedbe pri traženju vremena s najvećom temperaturom koristiti odgovarajući preopterećeni relacijski operator (uspoređuje se prema temperaturi).
* Kod bilo kakvog ispisa osloniti se na metodu "GetAsString()". Nikako ne trebate ispisivati bilo što iz klasa koje predstavljaju dio rješenja problema.
* Konzola se smije rabiti isključivo unutar testnog programa, unutar ConsoleWeatherUI projekta.
* Možete pretpostaviti da će ulazna datoteka biti ispravno formatirana i sadržavati sedam unosa.

## Pravila

* Koristiti programski jezik je C#.
* Svaka klasa ide u zasebnu datoteku imena jednakog kao i klasa
* Kreirati dva projekta unutar solutiona, jedan koji će biti definiran kao *class library* i u kojem će biti logika rješenja, a drugi koji će biti konzolna aplikacija i koji će predstavljati UI.
* Koristiti .NET Core projekte u VS-u.
* Uploadati rješenje na Github, na privatni repozitorij.
* Zalijepiti link na repozitorij na odgovarajuće mjesto na Merlinu za predaju zadaće.
* Nakon isteka roka za zadaću učiniti repozitorij javnim kako bi mogao biti ocijenjen.
* Prepisivanje je strogo zabranjeno i bit će kažnjavano (i za izvor i za prepisivača!).

## Testni program

```c#
private static void RunDemoForHW2()
{
    DateTime monday = new DateTime(2021, 11, 8);
    Weather mondayWeather = new Weather(6.17, 56.13, 4.9);
    DailyForecast mondayForecast = new DailyForecast(monday, mondayWeather);
    Console.WriteLine(monday.ToString());
    Console.WriteLine(mondayWeather.GetAsString());
    Console.WriteLine(mondayForecast.GetAsString());

    // Assume a valid input file (correct format).
    // Assume that the number of rows in the text file is always 7. 
    string fileName = "weather.forecast";
    if (File.Exists(fileName) == false)
    {
        Console.WriteLine("The required file does not exist. Please create it, or change the path.");
        return;
    }

    string[] dailyWeatherInputs = File.ReadAllLines(fileName);
    DailyForecast[] dailyForecasts = new DailyForecast[dailyWeatherInputs.Length];
    for (int i = 0; i < dailyForecasts.Length; i++)
    {
        dailyForecasts[i] = ForecastUtilities.Parse(dailyWeatherInputs[i]);
    }
    WeeklyForecast weeklyForecast = new WeeklyForecast(dailyForecasts);
    Console.WriteLine(weeklyForecast.GetAsString());
    Console.WriteLine("Maximal weekly temperature:");
    Console.WriteLine(weeklyForecast.GetMaxTemperature());
    Console.WriteLine(weeklyForecast[0].GetAsString());
}	
```

## Primjer ulazne datoteke
```
08/11/2021 00:00:00,6.17,46.50,4.9
09/11/2021 00:00:00,4.37,58.00,4.1
10/11/2021 00:00:00,8.45,56.03,1.9
11/11/2021 00:00:00,9.03,53.55,0.8
12/11/2021 00:00:00,3.14,42.33,6.1
13/11/2021 00:00:00,3.78,43.11,0.2
14/11/2021 00:00:00,5.22,42.22,0.3
```

## Primjer izlaza
```
08/11/2021 00:00:00
T=6.17°C, w=4.9km/h, h=56.13%
08/11/2021 00:00:00: T=6.17°C, w=4.9km/h, h=56.13%
08/11/2021 00:00:00: T=6.17°C, w=46.5km/h, h=4.9%
09/11/2021 00:00:00: T=4.37°C, w=58km/h, h=4.1%
10/11/2021 00:00:00: T=8.45°C, w=56.03km/h, h=1.9%
11/11/2021 00:00:00: T=9.03°C, w=53.55km/h, h=0.8%
12/11/2021 00:00:00: T=3.14°C, w=42.33km/h, h=6.1%
13/11/2021 00:00:00: T=3.78°C, w=43.11km/h, h=0.2%
14/11/2021 00:00:00: T=5.22°C, w=42.22km/h, h=0.3%

Maximal weekly temperature:
9.03
08/11/2021 00:00:00: T=6.17°C, w=46.5km/h, h=4.9%
```


# Zadaća 1 - Osnove OOP

## Zadatak

Radite sustav koji omogućuje rad s informacijama o wremenskim prilikama. Vremenske prilike predstavljene su klasom *Weather* sa stanjem za trenutnu temperaturu u stupnjevima Celzijusa, relativnu vlažnost zraka u postotcima te jačinu vjetra u km/h. Implementirajte sve potrebne klase definirajući njihova stanja i metode kako bi se testni program u nastavku mogao ispravno izvesti. 

## Dodatne upute i materijali

* Više o osjetu hladnoće vjetra (engl. *wind chill*) moguće je pronaći na [Wind chill - Wiki](https://en.wikipedia.org/wiki/Wind_chill).
	* Paziti na to u kojim se slučajevima računa osjet hladnoće vjetra, ako ga nije moguće odrediti uzima se da je osjet = 0.
* Više o osjetu topline (engl. Heat index, feels like) moguće je pronaći na [Heat index - Wiki](https://en.wikipedia.org/wiki/Heat_index).
	* Paziti na to da se rabi ispravna jednadžba, namijenjena odgovarajućoj mjernoj jedinici temperature.
* Niti jedna od ovih stvari ne predstavlja stanje.

## Pravila

* Koristiti programski jezik C#.
* Svaka klasa ide u zasebnu datoteku, imena jednakog kao i klasa
* Kreirati dva projekta unutar solutiona, jedan koji će biti definiran kao *class library* i u kojem će biti logika rješenja, a drugi koji će biti konzolna aplikacija i koji će predstavljati UI. Referencirati projekt s rješenjem u projektu koji predstavlja UI i na taj način rabiti njegove elemente.
* Koristiti .NET Core projekte u VS-u.
* Uploadati rješenje na Github, na privatni repozitorij.
* Zalijepiti link na repozitorij na odgovarajuće mjesto na Merlinu za predaju zadaće.
* Nakon isteka roka za zadaću učiniti repozitorij javnim kako bi mogao biti ocijenjen.
* Prepisivanje je strogo zabranjeno i bit će kažnjavano (i za izvor i za prepisivača!).

## Testni program

```c#	
private static void RunDemoForHW1()
{
    Weather current = new Weather();
    current.SetTemperature(24.12);
    current.SetWindSpeed(3.5);
    current.SetHumidity(0.56);
    Console.WriteLine("Weather info:\n"
        + "\ttemperature: " + current.GetTemperature() + "\n"
        + "\thumidity: " + current.GetHumidity() + "\n"
        + "\twind speed: " + current.GetWindSpeed() + "\n");
    Console.WriteLine("Feels like: " + current.CalculateFeelsLikeTemperature());

    Console.WriteLine("Finding weather info with largest windchill!");
    const int Count = 5;
    double[] temperatures = new double[Count] { 8.33, -1.45, 5.00, 12.37, 7.67 };
    double[] humidities = new double[Count] { 0.3790, 0.4555, 0.743, 0.3750, 0.6612 };
    double[] windSpeeds = new double[Count] { 4.81, 1.5, 5.7, 4.9, 1.2 };

    Weather[] weathers = new Weather[Count];
    for (int i = 0; i < weathers.Length; i++)
    {
        weathers[i] = new Weather(temperatures[i], humidities[i], windSpeeds[i]);
        Console.WriteLine("Windchill for weathers[" + i + "] is: " + weathers[i].CalculateWindChill());
    }
    Weather largestWindchill = FindWeatherWithLargestWindchill(weathers);
    Console.WriteLine(
        "Weather info:" + largestWindchill.GetTemperature() + ", " +
        largestWindchill.GetHumidity() + ", " + largestWindchill.GetWindSpeed()
    );
}
```

## Primjer izlaza
```
Weather info:
        temperature: 24.12
        humidity: 0.56
        wind speed: 3.5
Feels like: 22.97781714756796
Finding weather info with largest windchill!
Windchill for weathers[0] is: 7.925068596643065
Windchill for weathers[1] is: 0
Windchill for weathers[2] is: 3.8255514044838046
Windchill for weathers[3] is: 0
Windchill for weathers[4] is: 0
Weather info:8.33, 0.379, 4.8
```
