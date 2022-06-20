using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainEconomic
{
    static class SuperConsole
    {
        public static int ReadLine(string descryption = "::", string errorText = "Ошибка! Введено не число!")
        {
            int answer;
            Console.Write(descryption);
            while (!int.TryParse(Console.ReadLine(), out answer))
            {
                Console.WriteLine(errorText);
            }
            return answer;
        }
    }
}
