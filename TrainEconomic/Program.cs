namespace TrainEconomic;

enum LVL
{
    easy = 0,
    medium = 1,
    hard = 2
}

public static class Program
{
    private static int creadit = 0;
    private static int stepsCount = 0;
    private static int requirement;
    private static List<valuta> valutes = new List<valuta>();
    private static Person person;
    private static Place place;

    static Program()
    {
        StartGame();

        while (true)
        {

            Step();
            ConsoleUpdate();
            InputEngiene();
            if (creadit >= 30)
            {
                Console.WriteLine("You lose");
            }
        }
    }

    private static void Main()
    {

    }

    private static void ConsoleUpdate()
    {
        Console.Clear();
        Console.WriteLine("Ваш баланс: ");
        // При отрицательных числах скрывать пробел

        for (int i = 0; i < valutes.Count; i++)
        {
            string space = valutes[i].value < 0 ? "" : " "; ;
            Console.WriteLine($"Название: {valutes[i].name} | Цена:{space}{valutes[i].value} | Кол-во: {person.valutesCount[i]}");
        }
        Console.WriteLine($"Кредиты: {creadit}");

        Console.WriteLine("\nБиржа: ");
        for (int i = 0; i < valutes.Count; i++)
        {
            string space = valutes[i].value < 0 ? "" : " ";
            Console.WriteLine($"Название: {valutes[i].name} | Цена:{space}{valutes[i].value} | Кол-во: {place.valutesCount[i]}");
        }

        Console.WriteLine($"\nПотребности: {requirement} | Осталось дней до закрытия: {stepsCount % 3}");

        Console.Write($"\n(0 - следующий шаг) Покупка: ");
    }

    private static void InputEngiene()
    {
        while (true)
        {

            string store = SelectValute();
            int indexValute = int.Parse(store.Split('|')[0]);
            int newCount = int.Parse(store.Split('|')[1]);
            if (indexValute == 0) { return; }
            Console.WriteLine($"Вам необходимо {valutes[indexValute - 1].value * newCount} ед. \nВыберите собственную валюту на продажу (1-3):");
            string wallet = SelectValute();

            Console.WriteLine($"Вам необходимо {valutes[indexValute - 1].value * newCount} ед. \nВыберите собственную валюту на продажу (1-3):");
            Console.ReadLine();

        }
    }

    private static string SelectValute()
    {
        string ans = Console.ReadLine();
        int newCount = 0;
        while (ans != "0")
        {
            if (ans == "1" || ans == "2" || ans == "3")
            {
                Console.Write($"{valutes[int.Parse(ans) - 1].name} ({place.valutesCount[int.Parse(ans) - 1]}): ");

                while (!int.TryParse(Console.ReadLine(), out newCount))
                {
                    Console.WriteLine("Введите количество!");
                }
                if (newCount == 0) { ans = "4"; continue; }
                while (newCount > place.valutesCount[int.Parse(ans) - 1])
                {
                    Console.WriteLine("Введено больше, чем имеется на бирже!");
                    Console.Write($"{valutes[int.Parse(ans) - 1].name} ({place.valutesCount[int.Parse(ans) - 1]}): ");
                    while (!int.TryParse(Console.ReadLine(), out newCount))
                    {
                        Console.WriteLine("Введите количество!");
                        Console.Write($"{valutes[int.Parse(ans) - 1].name} ({place.valutesCount[int.Parse(ans) - 1]}): ");
                    }
                }
                return ans + "|" + newCount.ToString();
            }
            else
            {
                Console.WriteLine("Введите 1-3, для выбора вылюты и 0 - для следующего хода");
                ans = Console.ReadLine();
            }            
        }
        return ans + "|" + newCount.ToString();
    }
    private static void Step()
    {
        Random random = new Random();

        foreach (var val in valutes) { val.value = random.Next(-5, 5); }
        stepsCount++;

        if (requirement != 0)
        {
            creadit += requirement;
        }

        if (stepsCount % 3 == 0)
        {
            //Подумать над сложностью игры
            requirement = random.Next(5, 10);
        }
    }

    private static void StartGame()
    {
        valutes.Add(new valuta("Coin 1"));
        valutes.Add(new valuta("Coin 2"));
        valutes.Add(new valuta("Coin 3"));

        person = new Person(new List<int>() { 0, 0, 0 });
        place = new Place(new List<int>() { 5, 5, 5 });
    }
}