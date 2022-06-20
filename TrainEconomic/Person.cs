using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainEconomic
{
    internal class Person
    {
        private List<int> valutesCount = new List<int>();
        private List<int> tmpValutesCount = new List<int>();
        public Person(List<int> input)
        {
            foreach (int valute in input)
            {
                valutesCount.Add(valute);
                tmpValutesCount.Add(valute);
            }
        }
        public int GetValuteCount(int index)
        {
            return valutesCount[index];
        }
        public int GetValuteTmpCount(int index)
        {
            return tmpValutesCount[index];
        }
        public void SetValutesCount(List<int> tmpValutesCount)
        {
            this.tmpValutesCount = tmpValutesCount;
        }
        public void SetValutesCount(int index, int newValue)
        {
            tmpValutesCount[index] = newValue;
        }
        public void ReturnValue()
        {
            tmpValutesCount.Clear();
            foreach (int valute in valutesCount)
                tmpValutesCount.Add(valute);
        }
        public void SaveValutes()
        {
            valutesCount.Clear();
            foreach (int valute in tmpValutesCount)
                valutesCount.Add(valute);
        }
    }
}
