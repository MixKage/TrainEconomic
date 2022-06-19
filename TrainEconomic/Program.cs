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
        Console.WriteLine($"Шаг: {stepsCount}"); 
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

        Console.WriteLine($"\n(0 - следующий шаг/отмена)");
    }

    private static void InputEngiene()
    {
        while (true)
        {
            int tmpCredit = 0;
            Console.Write("Покупка: ");
            string store = SelectValute(false);
            int indexValute = int.Parse(store.Split('|')[0]);
            int newCount = int.Parse(store.Split('|')[1]);
            if (indexValute == 0) { return; }
            ConsoleUpdate();
            Console.WriteLine($"Вам необходимо {valutes[indexValute - 1].value * newCount} ед. \nВыберите собственную валюту на продажу (1-3):");
            string wallet = SelectValute(true);
            int indexValuteWallet = int.Parse(store.Split('|')[0]);
            int newCountWallet = int.Parse(store.Split('|')[1]);
            if (indexValuteWallet == 0) { return; }
            
            if(valutes[indexValute - 1].value * newCount > valutes[indexValuteWallet - 1].value * newCountWallet)
            {
                Console.Write($"Вам не хватает {valutes[indexValute - 1].value * newCount - valutes[indexValuteWallet - 1].value * newCountWallet} ед. Взять кредит? (0-назад, 1-да, 2-нет)\n:: ");
            }
            else if (valutes[indexValute - 1].value * newCount < valutes[indexValuteWallet - 1].value * newCountWallet)
            {
                Console.Write($"Вам не хватает {valutes[indexValuteWallet - 1].value * newCountWallet - valutes[indexValute - 1].value * newCount} ед. Погасить кредит? (0-назад, 1-да, 2-нет)\n:: ");
            }
            if()
            //Если остались свободные единицы потратить их на погашение долга или покупку валюты
            //Console.WriteLine($"Вам необходимо {valutes[indexValute - 1].value * newCount} ед. \nВыберите собственную валюту на продажу (1-3):");
            //Console.ReadLine();

        }
    }

    //mode = true - wallet, false - place
    private static string SelectValute(bool mode)
    {
        string ans = Console.ReadLine();
        int newCount = 0;

        while (ans != "0")
        {
            if (ans == "1" || ans == "2" || ans == "3")
            {
                int countCoint;
                if (mode)
                    countCoint = person.valutesCount[int.Parse(ans) - 1];
                else
                    countCoint = place.valutesCount[int.Parse(ans) - 1];

                Console.Write($"{valutes[int.Parse(ans) - 1].name} ({countCoint}): ");

                while (!int.TryParse(Console.ReadLine(), out newCount))
                {
                    Console.WriteLine("Введите количество!");
                }
                if (newCount == 0) { ans = "4"; continue; }
                while (newCount > countCoint)
                {
                    Console.WriteLine("Введено больше, чем имеется на бирже!");
                    Console.Write($"{valutes[int.Parse(ans) - 1].name} ({countCoint}): ");
                    while (!int.TryParse(Console.ReadLine(), out newCount))
                    {
                        Console.WriteLine("Введите количество!");
                        Console.Write($"{valutes[int.Parse(ans) - 1].name} ({countCoint}): ");
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

        person = new Person(new List<int>() { 5, 5, 5 });
        place = new Place(new List<int>() { 5, 5, 5 });
    }
}