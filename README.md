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
