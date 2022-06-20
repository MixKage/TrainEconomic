﻿namespace TrainEconomic;

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

    private static void Main()
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

    private static void ConsoleUpdate()
    {
        Console.Clear();
        Console.WriteLine($"Шаг: {stepsCount}");
        Console.WriteLine("Ваш баланс: ");
        // При отрицательных числах скрывать пробел

        for (int i = 0; i < valutes.Count; i++)
        {
            string space = valutes[i].value < 0 ? "" : " "; ;
            Console.WriteLine($"Название: {valutes[i].name} | Цена:{space}{valutes[i].value} | Кол-во: {person.GetValuteTmpCount(i)}");
        }
        Console.WriteLine($"Кредиты: {creadit}");

        Console.WriteLine("\nБиржа: ");
        for (int i = 0; i < valutes.Count; i++)
        {
            string space = valutes[i].value < 0 ? "" : " ";
            Console.WriteLine($"Название: {valutes[i].name} | Цена:{space}{valutes[i].value} | Кол-во: {place.GetValuteTmpCount(i)}");
        }

        Console.WriteLine($"\nПотребности: {requirement} | Осталось дней до закрытия: {stepsCount % 3}");

        Console.WriteLine($"\n(0 - следующий шаг/отмена)");
    }

    private static void InputEngiene()
    {
        while (true)
        {

            Console.Write("Покупка: ");
            string store = SelectValute(false);
            int indexValute = int.Parse(store.Split('|')[0]);
            int newCount = int.Parse(store.Split('|')[1]);
            if (indexValute == 0) { return; }

            int indexValuteWallet = -1;
            int newCountWallet = 0;
            int tmpMyMoney = 0;

            while (indexValuteWallet != 0)
            {
                ConsoleUpdate();
                Console.WriteLine($"Количетсво полученных единиц: {tmpMyMoney}");
                Console.Write($"Вам необходимо {valutes[indexValute - 1].value * newCount} ед. \nВыберите собственную валюту на продажу (1-3) (0-завершить продажу):\n::");
                string wallet = SelectValute(true);
                indexValuteWallet = int.Parse(wallet.Split('|')[0]);
                newCountWallet = int.Parse(wallet.Split('|')[1]);

                if (indexValuteWallet == 0)
                {
                    string desryption = String.Empty;
                    if (valutes[indexValute - 1].value * newCount > tmpMyMoney)
                    {
                        desryption = $"Вам не хватает {valutes[indexValute - 1].value * newCount - tmpMyMoney} ед. Взять кредит? (0-назад, 1-да, 2-отменить)\n::";
                    }
                    else if (valutes[indexValute - 1].value * newCount < tmpMyMoney)
                    {
                        desryption = $"У вас переизбыток {tmpMyMoney - valutes[indexValute - 1].value * newCount} ед. Погасить кредит? (0-назад, 1-да, 2-отменить)\n::";
                    }

                    if (desryption == String.Empty)
                    {
                        person.SetValutesCount(indexValute - 1, person.GetValuteTmpCount(indexValute - 1) + newCount);
                        break;
                    }

                    int ans = -1;
                    while (ans != 0 && ans != 1 && ans != 2)
                        ans = SuperConsole.ReadLine(desryption, "Введите 0-назад, 1-да, 2-отменить:");
                    if (ans == 0)
                    {
                        person.ReturnValue();
                        tmpMyMoney = 0;
                        indexValuteWallet = -1;
                        break;
                    }
                    else if (ans == 1)
                    {
                        if (valutes[indexValute - 1].value * newCount > tmpMyMoney)
                        {
                            creadit -= valutes[indexValute - 1].value * newCount - tmpMyMoney;
                        }
                        else if (valutes[indexValute - 1].value * newCount < tmpMyMoney)
                        {
                            creadit += tmpMyMoney - valutes[indexValute - 1].value * newCount;
                        }
                        break;
                    }
                    else
                    {
                        person.ReturnValue();
                        place.ReturnValue();
                        tmpMyMoney = 0;
                        indexValuteWallet = -1;
                    }
                }
                if (indexValuteWallet == -1) break;
                tmpMyMoney += valutes[indexValuteWallet - 1].value * newCountWallet;
            }
            if(indexValuteWallet==-1) { continue; }
            if (tmpMyMoney == 0) { return; }

            person.SaveValutes();
            place.SaveValutes();
            break;
            //if()
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
                    countCoint = person.GetValuteTmpCount(int.Parse(ans) - 1);
                else
                    countCoint = place.GetValuteTmpCount(int.Parse(ans) - 1);

                Console.Write($"{valutes[int.Parse(ans) - 1].name} ({countCoint}): ");

                newCount = SuperConsole.ReadLine("", "Введите число!");
                if (newCount == 0) { ans = "4"; continue; }
                while (newCount > countCoint)
                {
                    Console.WriteLine("Введено больше, чем имеется на бирже!");
                    Console.Write($"{valutes[int.Parse(ans) - 1].name} ({countCoint}): ");
                    newCount = SuperConsole.ReadLine("", "Введите число");
                }
                if (mode)
                {
                    person.SetValutesCount(int.Parse(ans) - 1, person.GetValuteTmpCount(int.Parse(ans) - 1) - newCount);
                }
                else
                {
                    place.SetValutesCount(int.Parse(ans) - 1, place.GetValuteTmpCount(int.Parse(ans) - 1) - newCount);
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