# Zadaća 4 - Iznimke, Generičke klase

## Zadatak

Definirajte klasu koja omogućuje pohranu dnevnih vremenskih prognoza za proizvoljan broj dana (klasa *DailyForecastRepository*). Omogućite:
* dodavanje novih prognoza za pojedinačne dane
* dodavanje cijele liste prognoza
* uklanjanje prognoza po datumu
* izravno iteriranje po prognozi uporabom petlje *foreach*
* duboko kopiranje uporabom odgovarajućeg konstruktora

Implementirajte sve potrebne klase i njihove metode kako bi se testni program u nastavku mogao ispravno izvesti.

## Dodatne upute i materijali

* Dnevne prognoze unutar objekta u svakom su trenutku sortirane uzlazno prema datumu.
* U slučaju da se pokuša dodati prognoza za datum (uzeti u obzir samo datume, ne gledati vrijeme) koji je već prisutan, potrebno je postojeću zamijeniti novom prognozom.
* U slučaju da kod brisanja ne postoji niti jedna prognoza, potrebno je podići iznimku vlastitog tipa izvedenog iz klase *System.Exception* koja će čuvati i omogućiti pristup datumu koji je doveo do iznimke.

## Pravila

* Koristiti programski jezik C#.
* Svaka klasa ide u zasebnu datoteku imena jednakog kao i klasa.
* Kreirati dva projekta unutar solutiona, jedan koji će biti definiran kao *class library* i u kojem će biti logika rješenja, a drugi koji će biti konzolna aplikacija i koji će predstavljati UI.
* Koristiti .NET Core projekte u VS-u.
* Uploadati rješenje na Github, na privatni repozitorij.
* Zalijepiti link na repozitorij na odgovarajuće mjesto na Merlinu za predaju zadaće.
* Nakon isteka roka za zadaću učiniti repozitorij javnim kako bi mogao biti ocijenjen.
* Prepisivanje je strogo zabranjeno i bit će kažnjavano (i za izvor i za prepisivača!).

## Testni program

```c#
private static void RunDemoForHW4()
{
    double minTemperature = -25.00, maxTemperature = 43.00;
    double minHumidity = 0.0, maxHumidity = 100.00;
    double minWindSpeed = 0.00, maxWindSpeed = 10.00;
    IRandomGenerator randomGenerator = new UniformGenerator(new Random());
    WeatherGenerator weatherGenerator = new WeatherGenerator(
        minTemperature, maxTemperature,
        minHumidity, maxHumidity,
        minWindSpeed, maxWindSpeed,
        randomGenerator
    );

    // Creating the repository and adding items.
    DailyForecastRepository repository = new DailyForecastRepository();
    repository.Add(new DailyForecast(DateTime.Now, weatherGenerator.Generate()));
    repository.Add(new DailyForecast(DateTime.Now.AddDays(1), weatherGenerator.Generate()));
    repository.Add(new DailyForecast(DateTime.Now.AddDays(2), weatherGenerator.Generate()));
    Console.WriteLine($"Current state of repository:{Environment.NewLine}{repository}");

    // Adding a new forecast for the same day should replace the old one:
    repository.Add(new DailyForecast(DateTime.Now.AddHours(2), weatherGenerator.Generate()));
    Console.WriteLine($"Current state of repository:{Environment.NewLine}{repository}");
    
    // Adding multiple forecasts, the ones for existing days should replace the old ones:
    List<DailyForecast> forecasts = new List<DailyForecast>() {
        new DailyForecast(DateTime.Now.AddDays(2), weatherGenerator.Generate()),
        new DailyForecast(DateTime.Now.AddDays(3), weatherGenerator.Generate()),
        new DailyForecast(DateTime.Now.AddDays(4), weatherGenerator.Generate()),
    };
    repository.Add(forecasts);
    Console.WriteLine($"Current state of repository:{Environment.NewLine}{repository}");

    // Removing forecasts based on date:
    try
    {
        repository.Remove(DateTime.Now);
        repository.Remove(DateTime.Now.AddDays(100));
    }
    catch (NoSuchDailyWeatherException exception) 
    {
        Console.WriteLine(exception.Message);
    }
    Console.WriteLine($"Current state of repository:{Environment.NewLine}{repository}");

    // Iterating over a custom object:
    Console.WriteLine("Temperatures:");
    foreach (DailyForecast dailyForecast in repository)
    {
        Console.WriteLine($"-> {dailyForecast.Weather.GetTemperature()}");
    }

    // Deep clone:
    DailyForecastRepository copy = new DailyForecastRepository(repository);
    Console.WriteLine($"Original repository:{Environment.NewLine}{repository}");
    Console.WriteLine($"Cloned repository:{Environment.NewLine}{copy}");
    
    DailyForecast forecastToAdd = new DailyForecast(DateTime.Now, new Weather(-2.0, 47.12, 2.1));
    copy.Add(forecastToAdd);
    
    Console.WriteLine($"Original repository:{Environment.NewLine}{repository}");
    Console.WriteLine($"Cloned repository:{Environment.NewLine}{copy}");
}
```

## Primjer izlaza

```
Current state of repository:
10.12.2021. 12:02:41: T=-2.449213920835973°C, w=2.967170687842728km/h, h=93.82930434952922%
11.12.2021. 12:02:41: T=35.455357905689326°C, w=6.927796735860313km/h, h=13.351009792346048%
12.12.2021. 12:02:41: T=-22.406719079896213°C, w=9.561407258529872km/h, h=73.79925664225559%
Current state of repository:
10.12.2021. 14:02:41: T=-19.968777814399814°C, w=5.392248679600772km/h, h=1.1538550728717143%
11.12.2021. 12:02:41: T=35.455357905689326°C, w=6.927796735860313km/h, h=13.351009792346048%
12.12.2021. 12:02:41: T=-22.406719079896213°C, w=9.561407258529872km/h, h=73.79925664225559%
Current state of repository:
10.12.2021. 14:02:41: T=-19.968777814399814°C, w=5.392248679600772km/h, h=1.1538550728717143%
11.12.2021. 12:02:41: T=35.455357905689326°C, w=6.927796735860313km/h, h=13.351009792346048%
12.12.2021. 12:02:41: T=18.107616548942218°C, w=1.087208972818781km/h, h=93.31162436553818%
13.12.2021. 12:02:41: T=36.478731279018675°C, w=6.7183022372044165km/h, h=45.56707131935613%
14.12.2021. 12:02:41: T=-3.4348463259799615°C, w=0.6839242254774665km/h, h=41.59626864902501%
No daily forecast for 20.3.2022. 0:00:00
Current state of repository:
11.12.2021. 12:02:41: T=35.455357905689326°C, w=6.927796735860313km/h, h=13.351009792346048%
12.12.2021. 12:02:41: T=18.107616548942218°C, w=1.087208972818781km/h, h=93.31162436553818%
13.12.2021. 12:02:41: T=36.478731279018675°C, w=6.7183022372044165km/h, h=45.56707131935613%
14.12.2021. 12:02:41: T=-3.4348463259799615°C, w=0.6839242254774665km/h, h=41.59626864902501%
Temperatures:
-> 35.455357905689326
-> 18.107616548942218
-> 36.478731279018675
-> -3.4348463259799615
Original repository:
11.12.2021. 12:02:41: T=35.455357905689326°C, w=6.927796735860313km/h, h=13.351009792346048%
12.12.2021. 12:02:41: T=18.107616548942218°C, w=1.087208972818781km/h, h=93.31162436553818%
13.12.2021. 12:02:41: T=36.478731279018675°C, w=6.7183022372044165km/h, h=45.56707131935613%
14.12.2021. 12:02:41: T=-3.4348463259799615°C, w=0.6839242254774665km/h, h=41.59626864902501%
Cloned repository:
11.12.2021. 12:02:41: T=35.455357905689326°C, w=6.927796735860313km/h, h=13.351009792346048%
12.12.2021. 12:02:41: T=18.107616548942218°C, w=1.087208972818781km/h, h=93.31162436553818%
13.12.2021. 12:02:41: T=36.478731279018675°C, w=6.7183022372044165km/h, h=45.56707131935613%
14.12.2021. 12:02:41: T=-3.4348463259799615°C, w=0.6839242254774665km/h, h=41.59626864902501%
Original repository:
11.12.2021. 12:02:41: T=35.455357905689326°C, w=6.927796735860313km/h, h=13.351009792346048%
12.12.2021. 12:02:41: T=18.107616548942218°C, w=1.087208972818781km/h, h=93.31162436553818%
13.12.2021. 12:02:41: T=36.478731279018675°C, w=6.7183022372044165km/h, h=45.56707131935613%
14.12.2021. 12:02:41: T=-3.4348463259799615°C, w=0.6839242254774665km/h, h=41.59626864902501%
Cloned repository:
10.12.2021. 12:02:41: T=-2°C, w=2.1km/h, h=47.12%
11.12.2021. 12:02:41: T=35.455357905689326°C, w=6.927796735860313km/h, h=13.351009792346048%
12.12.2021. 12:02:41: T=18.107616548942218°C, w=1.087208972818781km/h, h=93.31162436553818%
13.12.2021. 12:02:41: T=36.478731279018675°C, w=6.7183022372044165km/h, h=45.56707131935613%
14.12.2021. 12:02:41: T=-3.4348463259799615°C, w=0.6839242254774665km/h, h=41.59626864902501%
```

# Zadaća 3 - Odnosi i polimorfizam

## Zadatak

U sustav za rad s informacijama o vremenskim prilikama trebate dodati klasu za generiranje vremenskih prilika (generiranje instanci klase *Weather*). Ta nova klasa definira raspone vrijednosti unutar kojih će biti postavljane vrijednosti atributa novostvorenih (generiranih) vremenskih prilika, generator pseudoslučajnih vrijednosti te metodu za generiranje vremenskih prilika.

Generator pseudoslučajnih vrijednosti treba biti predstavljen sučeljem *IRandomGenerator* koje ćete sami definirati, a ono sadrži metodu koja omogućuje generiranje realne vrijednosti unutar raspona zadanog predanim argumentima. Definirajte dva različita konkretna tipa generatora pseudoslučajnih vrijednosti. Prvi treba generirati vrijednosti unutar zadanog raspona prema uniformnoj razdiobi. Drugi predstavlja pristrani generator. Kod pristranog generatora, dvostruko je veća vjerojatnost generiranja vrijednosti u donjoj polovini raspona vrijednosti u odnosu na one iz gornje polovine (vidjeti dodatne upute). 
 
Također,  potrebno je omogućiti ispis sadržaja u aplikaciji korištenjem sučelja *IPrinter* s dvije konkretne implementacije, konzolnom i datotečnom. Konzolnoj se inačici može zadati i naknadno izmijeniti boja teksta, dok se datotečnoj može zadati i naknadno izmijeniti datoteka u koju će zapisivati. 

Implementirajte sve potrebne klase i njihove metode kako bi se testni program u nastavku mogao ispravno izvesti. 

## Dodatne upute i materijali

* [Uniformna razdioba](https://mathworld.wolfram.com/UniformDistribution.html).
* [Kompozicija i labave veze](https://martinfowler.com/ieeeSoftware/coupling.pdf).
* Sve pojave metoda GetAsString() zamijenite pozivima odgovarajućim preopterećenim metodama ToString().
* Unutar vlastitih konkretnih generatora možete koristiti instancu Random klase.
* Gornja granica ne uključuje se kod generiranja. Ovo je samo da Vam olakša život ako koristite klasu *Random* čija metoda *NextDouble()* omogućuje generiranje pseudoslučajne vrijednosti u rasponu *0.0&le;r<1.0*.
* Ako je *Tmin=0*, a *Tmax=10*, tada će vjerojatnost generiranja vrijednosti *t<5* biti dvostruko veća u odnosu na *5&le;t<10*.
* Izlaz je formatiran tako da prikazuje očekivani izlaz s konzole i treba promatrati prikaz izlaza, a ne izvornu .md datoteku koja sadržava i html oznake.
* Metoda *PrintWeathers* ispisuje sve vremenske prilike na sve dostupne pisače.

## Pravila

* Koristiti programski jezik C#.
* Svaka klasa ide u zasebnu datoteku imena jednakog kao i klasa.
* Kreirati dva projekta unutar solutiona, jedan koji će biti definiran kao *class library* i u kojem će biti logika rješenja, a drugi koji će biti konzolna aplikacija i koji će predstavljati UI.
* Koristiti .NET Core projekte u VS-u.
* Uploadati rješenje na Github, na privatni repozitorij.
* Zalijepiti link na repozitorij na odgovarajuće mjesto na Merlinu za predaju zadaće.
* Nakon isteka roka za zadaću učiniti repozitorij javnim kako bi mogao biti ocijenjen.
* Prepisivanje je strogo zabranjeno i bit će kažnjavano (i za izvor i za prepisivača!).

## Testni program

```c#
private static void RunDemoForHW3()
{
    const int weatherCount = 10;
    double minTemperature = -25.00, maxTemperature = 43.00;
    double minHumidity = 0.00, maxHumidity = 100.00;
    double minWindSpeed = 0.00, maxWindSpeed = 10.00;
    Random generator = new Random();

    IRandomGenerator randomGenerator = new UniformGenerator(generator);
    WeatherGenerator weatherGenerator = new WeatherGenerator(
        minTemperature, maxTemperature,
        minHumidity, maxHumidity,
        minWindSpeed, maxWindSpeed,
        randomGenerator
    );

    Weather[] uniformWeathers = new Weather[weatherCount];
    for (int i = 0; i < uniformWeathers.Length; i++)
    {
        uniformWeathers[i] = weatherGenerator.Generate();
    }

    randomGenerator = new BiasedGenerator(generator);
    weatherGenerator.SetGenerator(randomGenerator);
    Weather[] winterWeathers = new Weather[weatherCount];
    for (int i = 0; i < winterWeathers.Length; i++)
    {
        winterWeathers[i] = weatherGenerator.Generate();
    }            

    IPrinter[] uniformPrinters = new IPrinter[]
    {
        new ConsolePrinter(ConsoleColor.DarkYellow),
        new FilePrinter(@"uniformWeathers.txt"),
    };	
    ForecastUtilities.PrintWeathers(uniformPrinters, uniformWeathers);

    IPrinter[] winterPrinters = new IPrinter[]
    {
        new ConsolePrinter(ConsoleColor.Green),
        new FilePrinter(@"winterWeathers.txt"),
    };
    ForecastUtilities.PrintWeathers(winterPrinters, winterWeathers);
}	
```

## Primjer izlaza

<span style="color:gold">
T=34.407072087473736°C, w=0.7242330027391357km/h, h=21.260308903297553%<br/>
T=8.348959385114235°C, w=5.741071643187232km/h, h=5.8444830150550615%<br/>
T=34.36940613359651°C, w=0.0366215501151148km/h, h=60.5831262472007%<br/>
T=-23.121934087072468°C, w=4.2098772778221765km/h, h=9.09385509281133%<br/>
T=22.721952622626887°C, w=2.075417783146453km/h, h=83.3377609417484%<br/>
T=-15.066170620762822°C, w=9.6834317779557km/h, h=19.34217727712457%<br/>
T=-12.24434554914215°C, w=6.475176804920276km/h, h=59.52898085095406%<br/>
T=26.33234551704132°C, w=4.058234097463188km/h, h=94.19013024037244%<br/>
T=-11.689332447335744°C, w=0.6188617463311468km/h, h=0.9697876875148097<br/>
T=37.487946042086904°C, w=5.903676932632774km/h, h=9.749426231602872%<br/>
</span><span style="color:green">
T=-2.5841465804652053°C, w=1.8678344748298796km/h, h=87.79237875146437%<br/>
T=39.16203646183109°C, w=2.610613826015319km/h, h=65.4507674348777%<br/>
T=15.793718503226396°C, w=2.65027532011749km/h, h=94.9463532282721%<br/>
T=32.91354864366052°C, w=7.542470103382352km/h, h=87.83233602895976%<br/>
T=-23.977097658895467°C, w=1.4734038112095573km/h, h=13.535089331462554<br/>
T=26.415513649310693°C, w=1.8416440495483783km/h, h=25.41685000779892%<br/>
T=31.61476519359963°C, w=1.294835033498162km/h, h=72.9905357225754%<br/>
T=-2.5828538106674586°C, w=2.7753609198962153km/h, h=3.6583521653238464<br/>
T=-7.7655242638501925°C, w=1.0780035849092546km/h, h=73.98214064258251%<br/>
T=37.34651307125879°C, w=3.4500459271716166km/h, h=76.45463593139064%<br/>
</span>

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
